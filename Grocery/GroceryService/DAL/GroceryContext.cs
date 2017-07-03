using GroceryService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryService.DAL
{
    public class GroceryContext : DbContext
    {
        public GroceryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Discounts> Discounts { get; set; }
    }
}
