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
        Task<int> CreateAsync(T entity);
        Task<ICollection<T>> ReadAsync();
        Task<T> ReadAsync(int id);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
