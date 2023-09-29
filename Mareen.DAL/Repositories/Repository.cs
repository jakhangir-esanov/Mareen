using Mareen.DAL.Contexts;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mareen.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext appDbContext;

    private readonly DbSet<T> dbSet;

    public Repository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
        dbSet = appDbContext.Set<T>();
    }

    public async Task InsertAsync(T entity)
    {
        await dbSet.AddAsync(entity);   
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
    }

    public void Drop(T entity)
    {
        dbSet.Remove(entity);
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null!)
    {
        IQueryable<T> query = dbSet.Where(expression).AsQueryable();

        if(includes is not null)
            foreach(var include in includes)
                query = query.Include(include);

        var result = await query.FirstOrDefaultAsync(expression);
        return result;
    }

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression, bool isNoTracking = true, string[] includes = null!)
    {
        IQueryable<T> query = expression is null ? dbSet.AsQueryable() : dbSet.Where(expression).AsQueryable();

        query = isNoTracking ? query.AsNoTracking() : query;

        if(includes is not null)
            foreach(var include in includes)
                query = query.Include(include);

        return query;
    }

    public async Task SaveAsync()
    {
        await appDbContext.SaveChangesAsync();
    }
}