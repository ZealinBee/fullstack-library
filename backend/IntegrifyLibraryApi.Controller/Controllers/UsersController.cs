using IntegrifyLibraryApi.Business;

using Microsoft.AspNetCore.Mvc;

namespace IntegrifyLibraryApi.Controller
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = _userService.GetAllUsers();
            return Ok(allUsers);
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var foundUser = _userService.GetUserById(id);
            if (foundUser == null)
            {
                return NotFound();
            }
            return Ok(foundUser);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var createdUser = _userService.CreateUser(userDto);
            return Ok(createdUser);
        }
    }

}