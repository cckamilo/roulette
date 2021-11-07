using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Masiv.OnlineGames.DataAccess.MongoDb.Models
{
    public class Roulette : EntityBase
    {
        public Roulette()
        {
         
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string id { get; set; }
        public bool isOpen { get; set; }
        public DateTime? openedAt { get; set; }
        public DateTime? closedAt { get; set; }  
        public int winningNumber { get; set; }
        public string winningColor { get; set; }
        public List<Bets> bets { get; set; }
    }
}
