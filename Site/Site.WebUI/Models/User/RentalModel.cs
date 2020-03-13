using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.WebUI.Models.User
{
    public class RentalModel
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        [Required]
        public string CarName { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeRented { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeReturned { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

    }
}