using Site.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.BLL.DTO
{
    public class CarDTO
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
        public string MarkS { get; set; }
        public string CarTypeS { get; set; }
        public bool IsDel { get; set; }
        public string PictureName { get; set; }
        public byte[] Picture { get; set; }
    }
}
