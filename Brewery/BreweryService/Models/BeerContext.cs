using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class BeerContext : DbContext
    {



        public BeerContext(DbContextOptions<BeerContext> options)
            : base(options)
        {
          //  Orders = new Orders(this);
          //  Clients = new Clients();
        }

        public DbSet<Beer> Beers { get; set; }

        public DbSet<Order>  Orders { get; set; }
        public DbSet<Client>  Clients { get; set; }

        public OverallMoney Overall
        {
            get
            {
                return new OverallMoney
                {
                    Outcome = GetOverallOutcome(),
                    Income = GetOverallIncome(),
                    BottlesProduced = GetOverallBottlesProduced(),
                    BottlesSold = GetOverallBottlesSold(),
                    Balance = GetOverallBalance()
                };
            }
        } 

        public float GetOverallOutcome()
        {
            //return 123f;
            return Beers.ToList().Sum(b => b.ProducedCosts);
        }

        public int GetOverallBottlesProduced()
        {
            return Beers.ToList().Sum(b => b.ProducedBottles);
        }

        public float GetOverallIncome()
        {
            return Beers.ToList().Sum(b => b.SoldIncome);
        }

        public int GetOverallBottlesSold()
        {
            return Beers.ToList().Sum(b => b.SoldBottles);
        }

        public float GetOverallBalance()
        {
            return Beers.ToList().Sum(b => b.GetBalance());
        }

        public void ProduceAll()
        {
            foreach (var beer in Beers)
                beer.Produce();        
        }
        
    }
}
