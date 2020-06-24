using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IStudentService : ICrud<Student>
    {
        Task<ICollection<Student>> ReadAsync(string lastName);
    }
}
