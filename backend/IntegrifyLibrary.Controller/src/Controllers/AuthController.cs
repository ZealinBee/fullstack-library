using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;

namespace IntegrifyLibrary.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 401)]
        public async Task<ActionResult<string>> VerifyCredentials([FromBody] LoginUserDto credentials)
        {
            try
            {
                return Ok(await _authService.VerifyCredentials(credentials));
            }
            catch (CustomException e)
            {
                return StatusCode(e.StatusCode, e.ErrorMessage);
            }
        }
    }
}
