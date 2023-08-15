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

    [Authorize]
    [HttpGet]
    public override ActionResult<List<GetUserDto>> GetAll()
    {
        return _userService.GetAll();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public override ActionResult<GetUserDto> GetOne([FromRoute] Guid id)
    {
        return Ok(_userService.GetOne(id));
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public override ActionResult<GetUserDto> CreateOne([FromBody] CreateUserDto dto)
    {
        var item = _userService.CreateOne(dto);
        if (item == null)
        {
            return BadRequest();
        }
        return CreatedAtAction("Created", item);
    }

}