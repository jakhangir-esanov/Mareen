using Mareen.Domain.Commons;
using System.Linq.Expressions;

namespace Mareen.DAL.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task InsertAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Drop(T entity);
    Task<T> SelectAsync(Expression<Func<T, bool>> expression = null!, string[] includes = null!);
    Task<T> SelectNoFilterAsync(Expression<Func<T, bool>> expression);
    IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!, bool isNoTracking = true, string[] includes = null!);
    Task SaveAsync();
}
