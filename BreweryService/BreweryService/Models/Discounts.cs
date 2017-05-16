namespace BreweryService.Models
{
    public class Discounts
    {

        public int per100
        {
            get { return per100; }
            set { per100 = value; Recalculate(); }
        }
        public int per250
        {
            get { return per250; }
            set { per250 = value; Recalculate(); }
        }
        public int per500
        {
            get { return per500; }
            set { per500 = value; Recalculate(); }
        }
        public int per1000
        {
            get { return per1000; }
            set { per1000 = value; Recalculate(); }
        }

        public Discounts()
        {
            per100 = 1;
            per250 = 2;
            per500 = 4;
            per1000 = 5;
        }

        public int GetDiscount(int amount)
        {
            if (amount < 100)  return 0;
            if (amount < 250)  return per100;
            if (amount < 500)  return per250;
            if (amount < 1000) return per500;
            return per1000;
        }

        private void Recalculate()
        {
            if (per100 > per250) per250 = per100;
            if (per250 > per500) per500 = per250;
            if (per500 > per1000) per1000 = per500;
        }
        
    }
}