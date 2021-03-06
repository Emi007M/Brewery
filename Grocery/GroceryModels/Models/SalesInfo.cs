﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryModels.Models
{
    public class SalesInfo
    {
        public string BeerName { get; set; }
        public int InStock { get; set; }

        public double AvSale { get; set; }

        public int SoldYesterday { get; set; }
        public int SoldWeek { get; set; }
        public int SoldMonth { get; set; }
        public int SoldTotal { get; set; }

        public static async Task<IEnumerable<SalesInfo>> CreateSalesInfo(IEnumerable<Beer> beers, IEnumerable<Sale> sales)
        {
            var salesinfo = new List<SalesInfo>();

            await Task.Run(() =>
            {
                foreach (var beer in beers)
                {
                    var beerSales = sales.Where(s => s.Beer.Id == beer.Id);
                    var si = new SalesInfo()
                    {
                        BeerName = beer.Type,
                        InStock = beer.InStore,
                        SoldTotal = beerSales.Sum(s => s.Amount),
                        SoldMonth = beerSales.Where(s => s.Timestamp - DateTime.Now < TimeSpan.FromDays(30)).Sum(s => s.Amount),
                        SoldWeek = beerSales.Where(s => s.Timestamp - DateTime.Now < TimeSpan.FromDays(7)).Sum(s => s.Amount),
                        SoldYesterday = beerSales.Where(s => s.Timestamp - DateTime.Today.AddDays(-1) < TimeSpan.FromDays(1)).Sum(s => s.Amount)
                    };
                    if (beerSales.Count() > 0)
                        si.AvSale = si.SoldTotal / Math.Ceiling((beerSales.Max(s => s.Timestamp) - beerSales.Min(s => s.Timestamp)).TotalDays);
                    else
                        si.AvSale = 0;

                    salesinfo.Add(si);
                }
            });

            return salesinfo;
        }
    }
}
