using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.BusinessLogic.Infrastructure
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        Task<bool> CreateAsync(T t);
        Task<bool> EditAsync(int? Id, T t);
        Task<bool> DeleteAsync(int? id);

        Task<List<T>> SearchQueryAsync(string query);
        Task<List<T>> SearchLinqAsync(string query);

    }
}
