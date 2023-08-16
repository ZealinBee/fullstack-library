using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace IntegrifyLibrary.Controllers;
[ApiController]

public class BookController : BaseController<Book, BookDto, BookDto, BookDto>
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
    public override ActionResult<BookDto> CreateOne([FromBody] BookDto dto)
    {
        var createdObject = _bookService.CreateOne(dto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }

    [Authorize(Roles = "Librarian")]
    [HttpPatch("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    [ProducesResponseType(statusCode: 404)]
    public override ActionResult<BookDto> UpdateOne([FromRoute] Guid id, [FromBody] BookDto dto)
    {
        var item = _bookService.UpdateOne(id, dto);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [Authorize(Roles = "Librarian")]
    [HttpDelete("{id}")]
    public override ActionResult<bool> DeleteOne([FromRoute] Guid id)
    {
        var item = _bookService.DeleteOne(id);
        if (item == false)
        {
            return NotFound();
        }
        return NoContent();
    }
}