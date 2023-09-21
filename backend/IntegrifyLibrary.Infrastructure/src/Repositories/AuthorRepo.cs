using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;
public class AuthorRepo : BaseRepo<Author>, IAuthorRepo
{
    public AuthorRepo(DatabaseContext context) : base(context)
    {

    }

    public async Task<Author> GetOneByAuthorName(string authorName)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.AuthorName == authorName);
    }

    public override async Task<List<Author>> GetAll(QueryOptions queryOptions)
    {
        return _dbSet
            .Include(author => author.Books)
            .ToList();
    }


    public override async Task<Author> GetOne(Guid id)
    {
        var entity = _dbSet.Include(author => author.Books).FirstOrDefault(a => a.AuthorId == id);
        if (entity is null)
        {
            throw new KeyNotFoundException("Id is not found");
        }

        return entity;
    }
}