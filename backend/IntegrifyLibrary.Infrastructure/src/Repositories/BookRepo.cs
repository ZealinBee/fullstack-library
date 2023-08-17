using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class BookRepo : BaseRepo<Book>, IBookRepo
{
    private readonly DatabaseContext _context;
    public BookRepo(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Book> GetOneByBookName(string bookName)
    {
        return await _context.Books.FirstOrDefaultAsync(x => x.BookName == bookName);
    }

}