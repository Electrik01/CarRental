using Site.DAL.EF;
using Site.DAL.Entities;
using Site.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.Repositories
{
    public class RentalRepository : IRepository<Rental>
    {
        private CarContext db;
        public RentalRepository(CarContext db) { this.db = db; }
        public IEnumerable<Rental> GetAll()
        {
            return db.Rentals.Include(c => c.Car).Include(c => c.ClientProfile);
        }
        public Rental Get(int id)
        {
            return db.Rentals.Find(id);
        }
        public void Create(Rental rental)
        {
            db.Rentals.Add(rental);
        }
        public IEnumerable<Rental> Find(Func<Rental, Boolean> predicate)
        {
            return db.Rentals.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Rental rental = db.Rentals.Find(id);
            if (rental != null)
                db.Rentals.Remove(rental);
        }
        public void Update(Rental rental)
        {
            Rental ren = db.Rentals.Where(r => r.Id == rental.Id).FirstOrDefault();
            ren.Status = rental.Status;
            ren.Message = rental.Message;
            db.Entry(ren).State = EntityState.Modified;
        }
    }
}
