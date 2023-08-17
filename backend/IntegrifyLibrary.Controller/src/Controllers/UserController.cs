using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
    [HttpGet("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public override async Task<ActionResult<GetUserDto>> GetOne([FromRoute] Guid id)
    {
        return Ok(await _userService.GetOne(id));
    }

    [Authorize(Roles = "Librarian")]
    [HttpGet]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public override async Task<ActionResult<List<GetUserDto>>> GetAll([FromQuery] QueryOptions queryOptions)
    {
        var items = await _userService.GetAll(queryOptions);
        if (items == null)
        {
            return NotFound();
        }
        return Ok(items);
    }


    [Authorize(Roles = "Librarian")]
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

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public override async Task<ActionResult<GetUserDto>> CreateOne([FromBody] CreateUserDto dto)
    {
        var createdObject = await _userService.CreateOne(dto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }

    [HttpPatch("{id:Guid}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    [ProducesResponseType(statusCode: 404)]
    public override async Task<ActionResult<GetUserDto>> UpdateOne([FromRoute] Guid id, [FromBody] UpdateUserDto dto)
    {
        var item = await _userService.UpdateOne(id, dto);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [Authorize(Roles = "User")]
    [HttpDelete("delete-account")]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public async Task<ActionResult> DeleteOwnAccount()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null)
        {
            var userId = userIdClaim.Value;
            var result = await _userService.DeleteOne(Guid.Parse(userId));
            if (result)
            {
                return Ok();
            }
        }
        return NoContent();
    }
}