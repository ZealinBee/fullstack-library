using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Controllers;
using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class GenreRepo : BaseRepo<Genre>, IGenreRepo
{
    private readonly DatabaseContext _context;
    public GenreRepo(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Genre> GetOneByGenreName(string genreName)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == genreName);
    }

    public override async Task<List<Genre>> GetAll(QueryOptions queryOptions)
    {
        return _dbSet
                .Include(genre => genre.Books)
                .ToList();
    }
}

