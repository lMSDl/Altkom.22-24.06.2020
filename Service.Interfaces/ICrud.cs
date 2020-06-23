using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICrud<T> where T : Entity //new(), class
    {
        int Create(T entity);
        ICollection<T> Read();
        T Read(int id);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
