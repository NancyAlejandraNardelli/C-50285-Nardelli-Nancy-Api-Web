using System.Linq.Expressions;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, string? includeProperties = null);
        Task Delete(T entity);
        Task Save();
        Task<bool> LogicalDelete(int id);

    }
}
