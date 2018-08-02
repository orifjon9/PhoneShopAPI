using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShopAPI.Data.Access.DAL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        
        void Commit();
        Task CommitAsync();
    }
}