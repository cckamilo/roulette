using System;
using System.ComponentModel.DataAnnotations;

namespace Masiv.OnlineGames.Models
{
    public class BetsModel
    {
        public string id { get; set; }
        /// <summary>
        /// position 0-36, and 37=> red, 38 => black
        /// </summary>
        [Range(0, 38)]
        public int value { get; set; }
        [Range(0.5d, maximum: 10000)]
        public double amount { get; set; }
        public double payout { get; set; }

    }
}
