using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class DbEntityService<T> : ICrud<T> where T : Entity
    {
        public int Create(T entity)
        {
            using (var context = new DbContext())
            {
                entity.Id = 0;
                entity = context.Set<T>().Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DbContext())
            {
                var entity = context.Set<T>().Find(id);
                context.Set<T>().Remove(entity);
                context.SaveChanges();
                //context.Dispose();
            }
        }

        public ICollection<T> Read()
        {
            using (var context = new DbContext())
            {
                var entities = context.Set<T>().ToList();
                return entities;
            }
        }

        public T Read(int id)
        {
            using (var context = new DbContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public void Update(int id, T entity)
        {
            using (var context = new DbContext())
            {
                entity.Id = id;
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
