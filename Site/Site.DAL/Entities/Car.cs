using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TransmissionType { get; set; }
        public string FuelType { get; set; }
        public double Price { get; set; }
        public double FuelConsumptionMin { get; set; }
        public double? FuelConsumptionMax { get; set; }
        public double EngineCapacity { get; set; }
        public Mark Mark { get; set; }
        public CarType CarType { get; set; }
        public bool IsDel { get; set; }
        public Image Image { get; set; }
    }
}
