using Site.DAL.Entities;
using Site.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Car> Cars { get; }
        IRepository<CarType> CarTypes { get; }
        IRepository<Mark> Marks { get; }
        IRepository<Rental> Rentals { get; }
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
        void Save();
    }
}
