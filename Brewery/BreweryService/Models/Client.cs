
using CryptoHelper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryService.Models
{
    public class Client
    {

        public Client(string name, string key)
        {
            Name = name;
            Key = Crypto.HashPassword(key);
            Discounts = new Discounts();
            Info = new List<ClientInfoFromShop>();
            Info.Add(  new ClientInfoFromShop() ); //generate single empty beer info, should be replaced with data from shop
        }

        public Client() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// password with which client will be able to connect to service
        /// sth like 1Q73TU
        /// </summary>
        public string Key { get; set; }
        public Discounts Discounts { get; set; }

         //   public static Discounts GlobalDiscounts { get; set; }
         /// <summary>
         /// data taken from client's shop service
         /// </summary>

         public List <ClientInfoFromShop> Info { get; set; }
    }
}