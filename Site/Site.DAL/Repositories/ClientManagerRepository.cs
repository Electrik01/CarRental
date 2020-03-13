using Site.DAL.EF;
using Site.DAL.Entities;
using Site.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.Repositories
{
    class ClientManagerRepository: IClientManager
    {
        CarContext db;
        
        public ClientManagerRepository(CarContext context)
        {
            db = context;
        }
        public void Create(ClientProfile client)
        {
            db.ClientProfiles.Add(client);
            db.SaveChanges();
        }
        public IEnumerable<ClientProfile> GetProfiles()
        {
            return db.ClientProfiles;
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
