namespace BreweryService.Models
{
    public class Client
    {

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