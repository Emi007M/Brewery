using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BreweryService.Models.BeerRepository;

namespace BreweryService.Models
{
    public interface IClientRepository
    {
        void Add(Client item);
        IEnumerable<Client> GetAll();
        Client Find(long key);
        Client FindByName(string clientName);
        void Remove(long key);
        void Update(Client item);
        bool IsKeyClientValid(long clientId, string key);
    }
}
