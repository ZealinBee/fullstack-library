using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;
public class BaseRepo<T> : IBaseRepo<T> where T : class
{
    protected readonly DatabaseContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepo(DatabaseContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public T CreateOne(T item)
    {
        _dbSet.Add(item);
        _context.SaveChanges();
        return GetOne((Guid)item.Id);
    }

    public T GetOne(Guid id)
    {
        var entity = _dbSet.Find(id);
        if (entity is null)
        {
            throw new KeyNotFoundException("Id is not found");
        }

        return entity;
    }

    public List<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T UpdateOne(T item)
    {
        _dbSet.Update(item);
        _context.SaveChanges();
        return item;
    }

    public bool DeleteOne(T item)
    {
        _dbSet.Remove(item);
        _context.SaveChanges();
        return true;
    }

}