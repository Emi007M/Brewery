using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class OrderProposal
    {
        public string ClientName { get; set; }
        public string ClientKey { get; set; }
        public Dictionary<int, int> Beers { get; set; }
    }
}
