using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.WebUI.Models.User
{
    public class ProfileModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public RentalModel Rental { get; set; }
    }
}