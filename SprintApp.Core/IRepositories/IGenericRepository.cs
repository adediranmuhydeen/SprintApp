using System.Linq.Expressions;

namespace SprintApp.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> DeleteAsync(Guid Id);
        Task<T> CreateAsync(T entity);
        Task<T> DeleteAsync(Expression<Func<T, bool>> exp, List<string> include = null);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> include = null);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> exp, List<string> include = null);
        Task AddManyAsync(List<T> list);
        T Update(T entity);
    }
}
