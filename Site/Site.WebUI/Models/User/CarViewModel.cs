using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.WebUI.Models.User
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TransmissionType { get; set; }
        public string FuelType { get; set; }
        public double Price { get; set; }
        public List<double> PriceList { get; set; }
        public double FuelConsumptionMin { get; set; }
        public double? FuelConsumptionMax { get; set; }
        public double EngineCapacity { get; set; }
        public string PictureName { get; set; }
        public byte[] Picture { get; set; }
    }
}
