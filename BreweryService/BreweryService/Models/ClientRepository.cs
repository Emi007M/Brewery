using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class ClientRepository : IClientRepository
    {
        private readonly BeerContext _context;
        public ClientRepository(BeerContext context)
        {
            _context = context;

            //setting same discounts to all
         //   Client.GlobalDiscounts = new Discounts();


            if (_context.Clients.Count() == 0) //for now initial db of beers
            {
                Add(new Client("client_1", "admin123"));
                Add(new Client("client_2", "pswd"));
                Add(new Client("client_3", "123QWE"));
                Add(new Client("client_4", "1q2w3e4r"));

            }

           

        }

        public void Add(Client item)
        {
            _context.Clients.Add(item);
            _context.SaveChanges();
        }

        public Client Find(long key)
        {
            return _context.Clients.FirstOrDefault(t => t.Id == key);
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public void Remove(long key)
        {
            var entity = _context.Clients.First(t => t.Id == key);
            _context.Clients.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Client item)
        {
            _context.Clients.Update(item);
            _context.SaveChanges();
        }

        //public OverallMoney GetOverallMoney()
        //{
        //    return new OverallMoney(_context);

        //}

        //public void ProduceAll()
        //{
        //    _context.ProduceAll();
        //    _context.SaveChanges();
        //}

        
    }
}
