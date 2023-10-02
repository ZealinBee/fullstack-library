using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace IntegrifyLibrary.Controllers;
[ApiController]

public class BookController : BaseController<Book, BookDto, GetBookDto, BookDto>
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService) : base(bookService)
    {
        _bookService = bookService;
    }

    [Authorize(Roles = "Librarian")]
    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public override async Task<ActionResult<GetBookDto>> CreateOne([FromBody] BookDto dto)
    {
        var createdObject = await _bookService.CreateOne(dto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }
}