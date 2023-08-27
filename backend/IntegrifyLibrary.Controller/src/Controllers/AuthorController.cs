using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace IntegrifyLibrary.Controllers;
[ApiController]
public class AuthorController : BaseController<Author, CreateAuthorDto, GetAuthorDto, UpdateAuthorDto>
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService) : base(authorService)
    {
        _authorService = authorService;
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    [Authorize(Roles = "Librarian")]
    public override async Task<ActionResult<GetAuthorDto>> CreateOne([FromBody] CreateAuthorDto dto)
    {
        var createdObject = await _authorService.CreateOne(dto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }
}