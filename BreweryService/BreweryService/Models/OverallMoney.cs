using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class OverallMoney
    {
        public float Outcome { get; set; }
        public float Income { get; set; }
        public int BottlesProduced { get; set; }
        public int BottlesSold { get; set; }
        public float Balance { get; set; }
    }
}
