using Microsoft.AspNetCore.Mvc;
using IntegrifyLibrary.Services.Abstractions;
using IntegrifyLibrary.Dto;

namespace IntegrifyLibrary.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = _userRepository.GetAllUsers();
            return Ok(allUsers);
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var foundUser = _userRepository.GetUserById(id);
            if (foundUser == null)
            {
                return NotFound();
            }
            return Ok(foundUser);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var createdUser = _userRepository.CreateUser(userDto);
            return Ok(createdUser);
        }
    }

}