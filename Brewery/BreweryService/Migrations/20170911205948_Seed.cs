using BreweryService.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BreweryService.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var rand = new Random();
            using (var db = new BeerContext())
            {
                Console.WriteLine("Beers");
                for (int i = 0; i <= TestCfg.Beers; i++)
                {
                    db.Beers.Add(new Beer
                    {
                        Type = "Test beer " + i.ToString(),
                        Amount = rand.Next(TestCfg.MinOrder * TestCfg.Clients, TestCfg.MaxOrder * TestCfg.Clients),
                        ProductionDaily = rand.Next(100, 500),
                        Price = rand.Next(2, 10)
                    });
                }
                db.SaveChanges();

                Console.WriteLine("Clients");
                for (int i = 0; i <= TestCfg.Clients; i++)
                {
                    db.Clients.Add(new Client("client_" + i.ToString(), "password"));
                }
                db.SaveChanges();

                Console.WriteLine("Orders");
                for (int i = 0; i <= TestCfg.Orders; i++)
                {
                    var beerId = rand.Next(0, db.Beers.Count() - 1);
                    var clientId = rand.Next(0, db.Clients.Count() - 1);
                    var amount = rand.Next(TestCfg.MinOrder, TestCfg.MaxOrder);

                    Beer b = db.Beers.Skip(beerId).Take(1).First();
                    if (b == null)
                    {
                        Console.WriteLine("No beer " + beerId);
                        continue;
                    }

                    float price = b.Price;

                    var c = db.Clients.Skip(clientId).Take(1).First();
                    if (c == null)
                    {
                        Console.WriteLine("No client " + clientId);
                        continue;
                    }

                    int discount = c.Discounts.GetDiscount(amount);

                    Order o = new Order(b.Id, c.Id, amount, price, discount);
                    if(b.Buy(amount, discount))
                        db.Orders.Add(o);

                }

                db.SaveChanges();
            }

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            using (var db = new BeerContext())
            {
                db.Orders.RemoveRange(db.Orders);
                db.Clients.RemoveRange(db.Clients);
                db.Beers.RemoveRange(db.Beers);
                db.SaveChanges();
            }
        }
    }
}
