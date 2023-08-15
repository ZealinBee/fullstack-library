using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business;

public class BookService : BaseService<Book, BookDto, BookDto, BookDto>, IBookService
{
    public BookService(IBookRepo bookRepo, IMapper mapper) : base(bookRepo, mapper)
    {
    }
}