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

    public virtual T CreateOne(T item)
    {
        _dbSet.Add(item);
        _context.SaveChanges();
        return item;
    }

    public virtual T GetOne(Guid id)
    {
        var entity = _dbSet.Find(id);
        if (entity is null)
        {
            throw new KeyNotFoundException("Id is not found");
        }

        return entity;
    }

    public virtual List<T> GetAll(QueryOptions queryOptions)
    {
        var items = _dbSet
            .AsEnumerable()
            .Where(e =>
                e.GetType().GetProperty(queryOptions.FilterBy)!.GetValue(e)!.ToString()!.ToLower().Contains(
                    queryOptions.Filter.ToLower()
                    )
            )
            .OrderBy(e => e.GetType().GetProperty(queryOptions.OrderBy));
        if (queryOptions.OrderByDirection == "desc")
        {
            return items.OrderDescending()
                .Skip((queryOptions.Page - 1) * queryOptions.PageSize)
                .Take(queryOptions.PageSize)
                .ToList();
        }
        return _dbSet.ToList();
    }

    public virtual T UpdateOne(T item)
    {
        _dbSet.Update(item);
        _context.SaveChanges();
        return item;
    }

    public virtual bool DeleteOne(T item)
    {
        _dbSet.Remove(item);
        _context.SaveChanges();
        return true;
    }

}