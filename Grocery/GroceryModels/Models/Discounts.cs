using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryModels.Models
{
    public class Discounts
    {
        public int Per100
        {
            get;
            set;
        }

        public int Per250
        {
            get;
            set;
        }

        public int Per500
        {
            get;
            set;
        }

        public int Per1000
        {
            get;
            set;
        }

        public DateTime Timestamp { get; set; }

        public int GetDiscount(int amount)
        {
            if (amount < 100) return 0;
            if (amount < 250) return Per100;
            if (amount < 500) return Per250;
            if (amount < 1000) return Per500;
            return Per1000;
        }

    }
}
