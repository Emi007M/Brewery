using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BreweryService.Models
{
    public class BeerContext : DbContext, IDesignTimeDbContextFactory<BeerContext>
    {
        public BeerContext()
        {
        }

        public BeerContext(DbContextOptions<BeerContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";

            var cfgBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            optionsBuilder.UseSqlite(cfgBuilder.Build().GetValue<string>("DatabaseConnectionString", "Data Source=brewery.db"));
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }

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

        public BeerContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BeerContext>();

            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";

            var cfgBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            builder.UseSqlite(cfgBuilder.Build().GetValue<string>("DatabaseConnectionString", "Data Source=brewery.db"));

            return new BeerContext(builder.Options);
        }
    }
}