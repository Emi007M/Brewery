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