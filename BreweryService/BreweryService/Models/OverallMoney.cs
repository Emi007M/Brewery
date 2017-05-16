using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class OverallMoney
    {
        private BeerContext bc;

        public OverallMoney()
        { }
            public OverallMoney(BeerContext _bc)
        {
            bc = _bc;
            this.Outcome = bc.GetOverallOutcome();
            this.Income = bc.GetOverallIncome();
            this.BottlesProduced = bc.GetOverallBottlesProduced();
            this.BottlesSold = bc.GetOverallBottlesSold();
            this.Balance = bc.GetOverallBalance();
        }

        public float Outcome { get; private set; }
        public float Income { get; private set; }
        public int BottlesProduced { get; private set; }
        public int BottlesSold { get; private set; }
        public float Balance { get; private set; }
    }
}
