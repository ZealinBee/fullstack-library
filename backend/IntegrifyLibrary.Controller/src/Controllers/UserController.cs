using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IntegrifyLibrary.Controllers;
[ApiController]
public class UserController : BaseController<User, CreateUserDto, GetUserDto, UpdateUserDto>
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) : base(userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Librarian")]
    [HttpGet]
    public override async Task<ActionResult<List<GetUserDto>>> GetAll(QueryOptions queryOptions)
    {
        return await _userService.GetAll(queryOptions);
    }

    [Authorize(Roles = "Librarian")]
    [HttpGet("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public override async Task<ActionResult<GetUserDto>> GetOne([FromRoute] Guid id)
    {
        return Ok(await _userService.GetOne(id));
    }

    // [Authorize(Roles = "Librarian")]
    [HttpPost("admin")]
    public async Task<ActionResult<GetUserDto>> CreateAdmin([FromBody] CreateUserDto dto)
    {
        var item = await _userService.CreateAdmin(dto);
        if (item == null)
        {
            return BadRequest();
        }
        return CreatedAtAction("Created", item);
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public override async Task<ActionResult<GetUserDto>> CreateOne([FromBody] CreateUserDto dto)
    {
        var createdObject = await _service.CreateOne(dto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }

    [Authorize(Roles = "Librarian")]
    [HttpPatch("{id}")]
    public override async Task<ActionResult<GetUserDto>> UpdateOne([FromRoute] Guid id, [FromBody] UpdateUserDto dto)
    {
        var item = await _userService.UpdateOne(id, dto);
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
        var item = await _userService.DeleteOne(id);
        if (item == false)
        {
            return NotFound();
        }
        return NoContent();
    }
}