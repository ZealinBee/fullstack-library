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
}