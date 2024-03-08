using Doczy.Core.Entities.Common;
using System.Linq.Expressions;
namespace Doczy.DataAccess.Repositories.Interfaces.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true, params Expression<Func<T, object>>?[] includes);
      
        IQueryable<T> FindAllPaginate(Expression<Func<T, bool>> expression, int pageIndex, int pageSize, bool tracking = true, params Expression<Func<T, object>>?[] includes);
        IQueryable<T> FindAll(Expression<Func<T, bool>> expression, bool tracking = true, params Expression<Func<T, object>>?[] includes);
        Task<T> GetSingleAysnc(Expression<Func<T, bool>> expression);
        Task<bool> CreateAsync(T entity);
        bool Update(T entity);
        void Delete(T entity);
        bool SoftDelete(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<int> SaveAsync();
    }
}
