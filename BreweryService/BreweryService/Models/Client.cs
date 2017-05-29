
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryService.Models
{
    public class Client
    {

        public Client()
        {
            Name = "client_" + Id;
            Key = "123QH" + Id;
            Discounts = new Discounts();
        }

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

    }
}