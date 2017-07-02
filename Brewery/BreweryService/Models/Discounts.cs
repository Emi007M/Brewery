using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryService.Models
{
    public class Discounts
    {
        private int _per100;
        private int _per250;
        private int _per500;
        private int _per1000;

        public Discounts()
        {
            //  if(Client.GlobalDiscounts != null)
            //  {
            //      Per100 = Client.GlobalDiscounts.Per100;
            //      Per250 = Client.GlobalDiscounts.Per250;
            //     Per500 = Client.GlobalDiscounts.Per500;
            //      Per1000 = Client.GlobalDiscounts.Per1000;
            //  }
            //  else
            //  {
            Per100 = 1;
            Per250 = 2;
            Per500 = 4;
            Per1000 = 5;
            // }
        }

        public int Per100
        {
            get { return _per100; }
            set { _per100 = value; Recalculate(); }
        }

        public int Per250
        {
            get { return _per250; }
            set { _per250 = value; Recalculate(); }
        }

        public int Per500
        {
            get { return _per500; }
            set { _per500 = value; Recalculate(); }
        }

        public int Per1000
        {
            get { return _per1000; }
            set { _per1000 = value; Recalculate(); }
        }

        public int GetDiscount(int amount)
        {
            if (amount < 100) return 0;
            if (amount < 250) return Per100;
            if (amount < 500) return Per250;
            if (amount < 1000) return Per500;
            return Per1000;
        }

        private void Recalculate()
        {
            if (Per100 > Per250) Per250 = Per100;
            if (Per250 > Per500) Per500 = Per250;
            if (Per500 > Per1000) Per1000 = Per500;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}