using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class DbStudentService : DbEntityService<Student>, IStudentService
    {
        public async Task<ICollection<Student>> ReadAsync(string lastName)
        {
            using (var context = new DbContext())
            {
                return await context.Set<Student>().Where(x => x.LastName == lastName).ToListAsync();
            }
        }
    }
}
