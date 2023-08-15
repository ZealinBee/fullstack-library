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
    public override ActionResult<List<GetUserDto>> GetAll()
    {
        return _userService.GetAll();
    }

    [Authorize(Roles = "Librarian")]
    [HttpGet("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public override ActionResult<GetUserDto> GetOne([FromRoute] Guid id)
    {
        return Ok(_userService.GetOne(id));
    }

    // [Authorize(Roles = "Librarian")]
    [HttpPost("admin")]
    public ActionResult<GetUserDto> CreateAdmin([FromBody] CreateUserDto dto)
    {
        var item = _userService.CreateAdmin(dto);
        if (item == null)
        {
            return BadRequest();
        }
        return CreatedAtAction("Created", item);
    }

    [Authorize(Roles = "Librarian")]
    [HttpPatch("{id}")]
    public override ActionResult<GetUserDto> UpdateOne([FromRoute] Guid id, [FromBody] UpdateUserDto dto)
    {
        var item = _userService.UpdateOne(id, dto);
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
        var item = _userService.DeleteOne(id);
        if (item == false)
        {
            return NotFound();
        }
        return NoContent();
    }



}