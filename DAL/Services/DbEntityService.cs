using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class DbEntityService<T> : ICrud<T> where T : Entity
    {
        public async Task<int> CreateAsync(T entity)
        {
            using (var context = new DbContext())
            {
                entity.Id = 0;
                entity = context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
                return entity.Id;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var context = new DbContext())
            {
                var entity = await context.Set<T>().FindAsync(id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                //context.Dispose();
            }
        }

        public async Task<ICollection<T>> ReadAsync()
        {
            using (var context = new DbContext())
            {
                return await context.Set<T>().ToListAsync();

                //var entities = context.Set<T>().ToList();
                //return await Task.FromResult(entities);
            }
        }

        public async Task<T> ReadAsync(int id)
        {
            using (var context = new DbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        public async Task UpdateAsync(int id, T entity)
        {
            using (var context = new DbContext())
            {
                entity.Id = id;
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                await  context.SaveChangesAsync();
            }
        }
    }
}
