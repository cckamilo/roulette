using System;
using System.Collections.Generic;

namespace Masiv.OnlineGames.Models
{
    public class RouletteModel
    {

        public string id { get; set; }
        public bool isOpen { get; set; }
        public DateTime? openedAt { get; set; }
        public DateTime? closedAt { get; set; }
        public int winningNumber { get; set; }
        public string winningColor { get; set; }
        public List<BetsModel> bets { get; set; }
    }
}

