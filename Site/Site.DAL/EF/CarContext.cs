using Microsoft.AspNet.Identity.EntityFramework;
using Site.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.EF
{
    public class CarContext : IdentityDbContext<ApplicationUser>
    {
        static CarContext()
        {
            //Database.SetInitializer<CarContext>(new MyContextInitializer());
        }
        public CarContext(string connectionString) : base(connectionString) { }
        public CarContext() : base() { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }

    class MyContextInitializer : CreateDatabaseIfNotExists<CarContext>
    {
        protected override void Seed(CarContext db)
        {
            Car c1 = new Car { Name = "SMART СABRIO", EngineCapacity=0.9f,CarType = new CarType {Name= "budget" }, FuelConsumptionMin=4, FuelConsumptionMax=6, FuelType= "petrol", Mark=new Mark { Name="Smart"} };
            db.Cars.Add(c1);
            db.SaveChanges();
        }
    }

}
