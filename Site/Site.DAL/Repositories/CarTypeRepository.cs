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
    public class CarTypeRepository : IRepository<CarType>
    {
        private CarContext db;
        public CarTypeRepository(CarContext db) { this.db = db; }
        public IEnumerable<CarType> GetAll()
        {
            return db.CarTypes;
        }
        public CarType Get(int id)
        {
            return db.CarTypes.Find(id);
        }
        public IEnumerable<CarType> Find(Func<CarType, Boolean> predicate)
        {
            return db.CarTypes.Where(predicate).ToList();
        }
        public void Create(CarType carType)
        {
            db.CarTypes.Add(carType);
        }
        public void Delete(int id)
        {
            CarType carType = db.CarTypes.Find(id);
            if (carType != null)
                db.CarTypes.Remove(carType);
        }
        public void Update(CarType carType)
        {
            db.Entry(carType).State = EntityState.Modified;
        }
    }
}


