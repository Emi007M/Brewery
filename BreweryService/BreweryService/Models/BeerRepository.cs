﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BeerContext _context;
        public BeerRepository(BeerContext context)
        {
            _context = context;

            if (_context.Beers.Count() == 0) //for now initial db of beers
            {
                Add(new Beer
                {
                    Type = "Pale Ale",
                   // Amount = 400,
                    ProductionDaily = 200,
                    Price = 5
                });
                Add(new Beer
                {
                    Type = "Dutch Lager",
                    //  Amount = 500,
                    ProductionDaily = 300,
                    Price = 7
                });
                Add(new Beer
                {
                    Type = "Scotch Stout",
                    //  Amount = 300,
                    ProductionDaily = 200,
                    Price = 10
                });


                
            }
              //  Find(1).Produce();
             //   Find(2).Produce();
             //   Find(3).Produce();
            //    Find(1).Produce();
           // _context.SaveChanges();
        }

        public void Add(Beer item)
        {
            _context.Beers.Add(item);
            _context.SaveChanges();
        }

        public Beer Find(long key)
        {
            return _context.Beers.FirstOrDefault(t => t.Id == key);
        }

        public IEnumerable<Beer> GetAll()
        {
            return _context.Beers.ToList();
        }

        public void Remove(long key)
        {
            var entity = _context.Beers.First(t => t.Id == key);
            _context.Beers.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Beer item)
        {
            _context.Beers.Update(item);
            _context.SaveChanges();
        }

        public OverallMoney GetOverallMoney()
        {
            return new OverallMoney(_context);

        }

        public void ProduceAll()
        {
            _context.ProduceAll();
            _context.SaveChanges();
        }

        
    }
}