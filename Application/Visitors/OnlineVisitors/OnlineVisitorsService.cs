using System;
using System.Linq;
using Application.Interfaces.Contexts;
using Domain.OnlineVisitors;
using MongoDB.Driver;

namespace Application.Visitors.OnlineVisitors
{
    public interface IOnlineVisitorsService
    {
        void OnConnectUser(string ClientId);
        void OndisConnectUser(string ClientId);
        int GetCount();
    }

    public class OnlineVisitorsService : IOnlineVisitorsService
    {
        private readonly IMongoDbContext<OnlineVisitor> _dbContext;
        private readonly IMongoCollection<OnlineVisitor> collection;

        public OnlineVisitorsService(IMongoDbContext<OnlineVisitor> dbContext)
        {
            _dbContext = dbContext;
            collection = _dbContext.GetCollection();
        }

        public void OnConnectUser(string ClientId)
        {
            var exist = collection.AsQueryable().FirstOrDefault(s => s.ClientId == ClientId);
            if (exist == null)
            {
                collection.InsertOne(new OnlineVisitor()
                {
                    Time = DateTime.Now,
                    ClientId = ClientId
                });
            }
       
        }

        public void OndisConnectUser(string ClientId)
        {
            collection.FindOneAndDelete(s => s.ClientId == ClientId);
        }

        public int GetCount()
        {
            return collection.AsQueryable().Count();
        }
    }
}