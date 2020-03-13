using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.WebUI.Models.User
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}