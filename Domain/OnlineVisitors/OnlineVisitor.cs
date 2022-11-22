using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.OnlineVisitors
{
    public class OnlineVisitor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Time { get; set; }
        public string ClientId { get; set; }
    }
}