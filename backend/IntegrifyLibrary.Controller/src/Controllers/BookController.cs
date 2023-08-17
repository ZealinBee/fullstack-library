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
    public override async Task<ActionResult<BookDto>> CreateOne([FromBody] BookDto dto)
    {
        var createdObject = await _bookService.CreateOne(dto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }

    [Authorize(Roles = "Librarian")]
    [HttpPatch("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    [ProducesResponseType(statusCode: 404)]
    public override async Task<ActionResult<BookDto>> UpdateOne([FromRoute] Guid id, [FromBody] BookDto dto)
    {
        var item = await _bookService.UpdateOne(id, dto);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [Authorize(Roles = "Librarian")]
    [HttpDelete("{id}")]
    public override async Task<ActionResult<bool>> DeleteOne([FromRoute] Guid id)
    {
        var item = await _bookService.DeleteOne(id);
        if (item == false)
        {
            return NotFound();
        }
        return NoContent();
    }
}