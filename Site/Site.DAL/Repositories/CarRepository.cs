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
    public class CarRepository : IRepository<Car>
    {
        private CarContext db;
        public CarRepository(CarContext db) { this.db = db; }
        public IEnumerable<Car> GetAll()
        {
            return db.Cars.Include(c=>c.Image).Include(c=>c.CarType).Include(c=>c.Mark);
        }
        public Car Get(int id)
        {
            return db.Cars.Find(id);
        }
        public IEnumerable<Car> Find(Func<Car, Boolean> predicate)
        {
            return db.Cars.Where(predicate).ToList();
        }
        public void Create(Car car)
        {
            db.Cars.Add(car);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Car car = db.Cars.Include(c => c.CarType).Include(c => c.Mark).Where(c=>c.Id==id).FirstOrDefault();
            if (db.Marks.Where(m => m.Id == car.Mark.Id).Count() - 1 == 0)
                db.Marks.Remove(db.Marks.Where(m => m.Id == car.Mark.Id).FirstOrDefault());
            if (db.CarTypes.Where(m => m.Id == car.CarType.Id).Count() - 1 == 0)
                db.CarTypes.Remove(db.CarTypes.Where(m => m.Id == car.CarType.Id).FirstOrDefault());
            if (car != null)
            {
                car.IsDel = true;
                Update(car);
            }
        } 
        public void Update(Car car)
        {
            db.Entry(car).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
