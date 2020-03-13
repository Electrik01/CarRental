using Microsoft.AspNet.Identity.EntityFramework;
using Site.DAL.Entities;
using Site.DAL.Identity;
using Site.DAL.Interfaces;
using Site.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private CarContext db;
        private CarRepository CarRepository;
        private CarTypeRepository CarTypeRepository;
        private MarkRepository MarkRepository;
        private RentalRepository RentalRepository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public EFUnitOfWork(string connectionString)
        {
            db = new CarContext(connectionString);
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                return userManager;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
                return roleManager;
            }
        }
        public IClientManager ClientManager
        {
            get
            {
                if(clientManager==null)
                    clientManager = new ClientManagerRepository(db);
                return clientManager;
            }
        }
        public IRepository<Car> Cars
        {
            get
            {
                if (CarRepository == null)
                    CarRepository = new CarRepository(db);
                return CarRepository;
            }
        }
        public IRepository<Mark> Marks
        {
            get
            {
                if (MarkRepository == null)
                    MarkRepository = new MarkRepository(db);
                return MarkRepository;
            }
        }
        public IRepository<CarType> CarTypes
        {
            get
            {
                if (CarTypeRepository == null)
                    CarTypeRepository = new CarTypeRepository(db);
                return CarTypeRepository;
            }
        }
        public IRepository<Rental> Rentals
        {
            get
            {
                if (RentalRepository == null)
                    RentalRepository = new RentalRepository(db);
                return RentalRepository;
            }
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
