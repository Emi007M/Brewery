using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryService.Models
{

    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        public string Type { get; set; }

        /// <summary>
        /// Cost of producing a single bottle
        /// </summary>
        public float Cost { get; set; }
        /// <summary>
        /// Wholesale price of a single bottle
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// Current amount of bottles in the brewery
        /// </summary>
        public int Amount { get; set; }
        public int ProductionDaily { get; set; }
        /// <summary>
        /// true if not removed from production
        /// </summary>
        public bool InProduction { get; set; }

        public int ProducedBottles { get; set; }
        public float ProducedCosts { get; set; }
        public int SoldBottles { get; set; }
        public float SoldIncome { get; set; }


        public Beer()
        {
            InProduction = true;
            Cost = 3;
            Price = 6;
            Amount = 0;
            ProductionDaily = 0;
            ProducedCosts = 0;
            ProducedBottles = 0;
            SoldIncome = 0;
            SoldBottles = 0;
        }

        /// <summary>
        /// Should be invoked everyday !!!
        /// </summary>
        public void Produce()
        {
            ProducedBottles += (int)ProductionDaily;
            ProducedCosts += (float)ProductionDaily * Cost;
            Amount += (int)ProductionDaily;
        }
       

        public bool Buy(int _amount, int _discount)
        {
            if (_amount > Amount) return false;

            SoldBottles += _amount;
            SoldIncome += _amount * Price * (100 - _discount) / 100f;
            Amount -= _amount;

            //TODO SAVE ORDER

            return true;
        }
       

        public float GetBalance()
        {
            return SoldIncome - ProducedCosts;
        }




    }
}
