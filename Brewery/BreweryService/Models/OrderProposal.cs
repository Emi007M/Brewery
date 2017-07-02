using System.Collections.Generic;

namespace BreweryService.Models
{
    public class OrderProposal
    {
        public string ClientName { get; set; }
        public string ClientKey { get; set; }
        public Dictionary<int, int> Beers { get; set; }
    }
}