using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LejlekuXpress.Models
{
    public class Log
    {
        [BsonId]  
        [BsonRepresentation(BsonType.ObjectId)] 
        public ObjectId Id { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
    }

}
