using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.WebUI.Models.User
{
    public class UserStatusModel
    {
        public IEnumerable<UserModel> UnlockedUsers { get; set; }
        public IEnumerable<UserModel> BlockedUsers { get; set; }

    }
}