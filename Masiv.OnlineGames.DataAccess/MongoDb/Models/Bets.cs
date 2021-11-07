using System;
using System.ComponentModel.DataAnnotations;

namespace Masiv.OnlineGames.DataAccess.MongoDb.Models
{
    public class Bets
    {
        public int value { get; set; }
        public double amount { get; set; }
        public double payout { get; set; }
    }
}
