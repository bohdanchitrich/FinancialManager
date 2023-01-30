using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IAsyncRepository<T>
{
    Task<T> AddAsync(T entity);

    Task DeleteAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<List<T>> GetAllAsync();

    Task<List<T>> GetPagedAsync(int page, int pageSize);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);

    Task<T?> GetAsync(Expression<Func<T, bool>> expression);

}