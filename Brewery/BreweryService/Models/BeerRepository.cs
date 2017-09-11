using System;
using System.Collections.Generic;
using System.Linq;

namespace BreweryService.Models
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BeerContext _context;

        public BeerRepository(BeerContext context)
        {
            _context = context;
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
            return _context.Overall;
        }

        public void ProduceAll()
        {
            _context.ProduceAll();
            _context.SaveChanges();
        }
    }
}