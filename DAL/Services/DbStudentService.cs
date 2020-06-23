using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class DbStudentService : DbEntityService<Student>, IStudentService
    {
        public ICollection<Student> Read(string lastName)
        {
            using (var context = new DbContext())
            {
                return context.Set<Student>().Where(x => x.LastName == lastName).ToList();
            }
        }
    }
}
