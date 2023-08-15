using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class BookRepo : BaseRepo<Book>
{
    public BookRepo(DatabaseContext context) : base(context)
    {

    }
}