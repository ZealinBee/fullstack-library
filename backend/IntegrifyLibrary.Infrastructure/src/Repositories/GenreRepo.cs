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
}

