using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.Contexts;
using Domain.Visitor;
using MongoDB.Driver;

namespace Application.Visitors.GetTodyReport
{
    public interface IGetTodyReportService
    {
        ResultTodyReportDto Execute();
    }

    public class GetTodyReportService : IGetTodyReportService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _collection;

        public GetTodyReportService(IMongoDbContext<Visitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _collection = _mongoDbContext.GetCollection();
        }

        public ResultTodyReportDto Execute()
        {

            var start = DateTime.Now.Date;
            var end = DateTime.Now.AddDays(1);

            var GettVisitDay = GettVisitPerHour(start, end);

            var visitPerMounth = GetVisitPerDay();

            var TodayPageViewCount = _collection.AsQueryable().Where(p => p.Time >= start && p.Time < end)
                .LongCount();

            var TodayVisitorCount = _collection.AsQueryable().Where(s => s.Time >= start && s.Time < end)
                .GroupBy(s => s.VisitorId).LongCount();
            var AllPageViewCount = _collection.AsQueryable().LongCount();
            var AllVisitorCount = _collection.AsQueryable().GroupBy(s => s.VisitorId).LongCount();

            var visitor = _collection.AsQueryable()
                .OrderByDescending(s => s.Time)
                .Take(10)
                .Select(p => new VisitorsDto()
                {
                    Id = p.Id,
                    Browser = p.Browser.Family,
                    CurrentLink = p.CurrentLink,
                    Ip = p.Ip,
                    OperationSystem = p.OperationSystem.Family,
                    IsSpider = p.Device.IsSpider,
                    ReferrerLink = p.ReferenceLink,
                    Time = p.Time,
                    VisitorId = p.VisitorId
                })
                .ToList();

            return new ResultTodyReportDto()
            {
                GeneralStats = new GeneralStateDto()
                {
                    PageViewsPerVisit = getAvg(AllPageViewCount , AllVisitorCount),
                    TotalPageViews = AllPageViewCount,
                    TotalVisitors = AllVisitorCount,
                    VisitPerDay = visitPerMounth
                },
                Today = new TodayDto()
                {
                    PageViews = TodayPageViewCount,
                    Visitors = TodayVisitorCount,
                    ViewsPerVisitor = getAvg(TodayPageViewCount , TodayVisitorCount),
                    VisitPerhour = GettVisitDay
                },
                Visitors = visitor
            };
        }

        private VisitCountDto GetVisitPerDay()
        {
            DateTime MonthStart = DateTime.Now.Date.AddDays(-30);
            DateTime MonthEnds = DateTime.Now.Date.AddDays(1);
            var Month_PageViewList = _collection.AsQueryable()
                .Where(s => s.Time >= MonthStart && s.Time < MonthEnds)
                .Select(d => new {d.Time}).ToList();

            VisitCountDto visitPerDay = new VisitCountDto() {Display = new string[31], Value = new int[31]};
            for (int i = 0; i <= 30; i++)
            {
                var currentday = DateTime.Now.AddDays(i * (-1));
                visitPerDay.Display[i] = i.ToString();
                visitPerDay.Value[i] = Month_PageViewList.Where(p => p.Time.Date == currentday.Date).Count();
            }

            return visitPerDay;
        }

        private VisitCountDto GettVisitPerHour(DateTime start, DateTime end)
        {
 

            var TodaypageViewList = _collection.AsQueryable().Where(p => p.Time >= start && p.Time < end)
                .Select(s => new {s.Time}).ToList();
            var  VisitPerHour = new VisitCountDto()
            {
                Display = new string[24],
                Value = new int[24]
            };
            for (int i = 0; i <= 23; i++)
            {
                VisitPerHour.Display[i] = $"h-{i}";
                VisitPerHour.Value[i] = TodaypageViewList.Where(s => s.Time.Hour == i).Count();
            }

            return VisitPerHour;
        }

        private float getAvg(long visitPage, long visitor)
        {
            if (visitor == 0)
            {
                return 0;
            }
            else
            {
                return visitPage / visitor;
            }

        }
    }

    public class ResultTodyReportDto
    {
        public GeneralStateDto GeneralStats { get; set; }    
        public TodayDto Today { get; set; }    
        public List<VisitorsDto> Visitors { get; set; }    
    }
    public class GeneralStateDto
    {
        public long TotalPageViews { get; set; }
        public long TotalVisitors { get; set; }
        public float PageViewsPerVisit { get; set; }
        public VisitCountDto VisitPerDay { get; set; }
    }

    public class TodayDto
    {
        public long PageViews { get; set; }
        public long Visitors { get; set; }
        public float ViewsPerVisitor { get; set; }
        public VisitCountDto VisitPerhour { get; set; }
    }
    public class VisitCountDto
    {
        public string[] Display { get; set; }
        public int[] Value { get; set; }

    }
    public class VisitorsDto
    {
        public string Id { get; set; }
        public string Ip { get; set; }
        public string CurrentLink { get; set; }
        public string ReferrerLink { get; set; }
        public string Browser { get; set; }
        public string OperationSystem { get; set; }
        public bool IsSpider { get; set; }
        public DateTime Time { get; set; }
        public string VisitorId { get; set; }

    }
}