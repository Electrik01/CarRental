using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.WebUI.Models.Manager
{
    public class CarAddModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TransmissionType { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]
        public double FuelConsumptionMin { get; set; }
        public double? FuelConsumptionMax { get; set; }
        [Required]
        public double EngineCapacity { get; set; }
        [Required]
        public string MarkS { get; set; }
        [Required]
        public string CarTypeS { get; set; }
        [Required]
        public double Price { get; set; }
    }
}