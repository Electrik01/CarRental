using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.BLL.BusinessModels
{
    public class PriceBLL
    {
     
        
        public double GetPrice(DateTime start, DateTime end, double price)
        {
            if (end == null || start == null || price == 0)
                return 0;
            double days = (int)(end.Date - start.Date).TotalDays;
            if (days < 0)
                return -1;
            if (days <= 2)
                return days * price;
            if (days <= 7)
                return days * price* 0.95;
            if (days <= 29)
                return days * price * 0.9;
            else
                return days * price * 0.85;
        }
        public PriceBLL() { }
        public List<double> GetAll(double price)
        {
            List<double> res_all = new List<double>();
            res_all.Add(price);
            res_all.Add(price * 0.95);
            res_all.Add(price * 0.9);
            res_all.Add(price * 0.85);
            return res_all;
        }
    }
}
