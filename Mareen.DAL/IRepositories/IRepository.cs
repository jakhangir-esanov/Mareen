using Mareen.Domain.Commons;
using System.Linq.Expressions;

namespace Mareen.DAL.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task InsertAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Drop(T entity);
    Task<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null!);
    IQueryable<T> SelectAll(Expression<Func<T, bool>> expression, bool isNoTracking = true, string[] includes = null!);
    Task SaveAsync();
}
