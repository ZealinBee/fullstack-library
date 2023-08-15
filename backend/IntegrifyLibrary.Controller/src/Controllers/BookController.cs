using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;


namespace IntegrifyLibrary.Controllers;
[ApiController]

public class BookController : BaseController<Book, BookDto, BookDto, BookDto>
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService) : base(bookService)
    {
        _bookService = bookService;
    }

}