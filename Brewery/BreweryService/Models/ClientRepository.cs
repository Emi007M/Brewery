using CryptoHelper;
using Microsoft.EntityFrameworkCore;
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
        }

        public void Add(Client item)
        {
            _context.Clients.Add(item);
            _context.SaveChanges();
        }

        public Client Find(long key)
        {
            return _context.Clients.Include(c => c.Discounts).Include(c => c.Info).FirstOrDefault(t => t.Id == key);
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.Include(c => c.Discounts).Include(c => c.Info).ToList();
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

        public bool IsKeyClientValid(long clientId, string key)
        {
            return Crypto.VerifyHashedPassword(_context.Clients.Find(clientId).Key, key);
        }

        public Client FindByName(string clientName)
        {
            return _context.Clients.FirstOrDefault(t => t.Name == clientName);
        }
    }
}
