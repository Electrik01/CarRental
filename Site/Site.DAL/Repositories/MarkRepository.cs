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
    public class MarkRepository : IRepository<Mark>
    {
        private CarContext db;
        public MarkRepository(CarContext db) { this.db = db; }
        public IEnumerable<Mark> GetAll()
        {
            return db.Marks;
        }
        public Mark Get(int id)
        {
            return db.Marks.Find(id);
        }
        public void Create(Mark mark)
        {
            db.Marks.Add(mark);
        }
        public void Delete(int id)
        {
            Mark mark = db.Marks.Find(id);
            if (mark != null)
                db.Marks.Remove(mark);
        }
        public IEnumerable<Mark> Find(Func<Mark, Boolean> predicate)
        {
            return db.Marks.Where(predicate).ToList();
        }
        public void Update(Mark mark)
        {
            db.Entry(mark).State = EntityState.Modified;
        }
    }
}
