using CorkbanTicketGen.Entities;
using CorkbanTicketGen.Infrastructure.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CorkbanTicketGen.Infrastructure.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

public class BaseRepository<T>(SqliteContext dbContext) : IRepository<T> where T : BaseEntity
{
    private DbSet<T> _dbSet = dbContext.Set<T>();
    
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}