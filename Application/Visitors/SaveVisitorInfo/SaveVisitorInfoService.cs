using Application.Interfaces.Contexts;
using Domain.Visitor;
using MongoDB.Driver;

namespace Application.Visitors.SaveVisitorInfo
{
    public class SaveVisitorInfoService : ISaveVisitorInfoService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _collection;
        public SaveVisitorInfoService(IMongoDbContext<Visitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _collection = _mongoDbContext.GetCollection();
        }

        public void Execute(VisitorDto request)
        {
            var Visitor = new Visitor()
            {
                Browser = new VisitorVersion
                {
                    Family = request.Browser.Family,
                    Version = request.Browser.Version,
                },
                CurrentLink = request.CurrentLink,
                Device = new Device
                {
                    Brand = request.Device.Brand,
                    Family = request.Device.Family,
                    IsSpider = request.Device.IsSpider,
                    Model = request.Device.Model
                },
                Ip = request.Ip,
                Method = request.Method,
                OperationSystem = new VisitorVersion
                {
                    Family = request.OperationSystem.Family,
                    Version = request.OperationSystem.Version
                },
                PhysicalPath = request.PhysicalPath,
                Protocol = request.Protocol,
                ReferenceLink = request.ReferenceLink,
                Time = request.Time,
                VisitorId = request.VisitorId
            };
            _collection.InsertOne(Visitor);
        }
    }
}