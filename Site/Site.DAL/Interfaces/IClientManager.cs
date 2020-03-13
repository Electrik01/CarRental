using Site.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.Interfaces
{
    public interface IClientManager:IDisposable
    {
        void Create(ClientProfile client);
        IEnumerable<ClientProfile> GetProfiles();
        IEnumerable<ApplicationUser> GetAll();
    }
}
