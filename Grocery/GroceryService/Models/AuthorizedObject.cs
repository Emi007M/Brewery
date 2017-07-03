using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryService.Models
{
    public class AuthorizedObject<T> where T : class
    {
        public string ClientName { get; set; }
        public string ClientKey { get; set; }
        public T Object { get; set; }
    }
}
