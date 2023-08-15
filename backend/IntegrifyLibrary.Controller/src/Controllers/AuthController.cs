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
        public ActionResult<string> VerifyCredentials([FromBody] LoginUserDto credentials)
        {
            return Ok(_authService.VerifyCredentials(credentials));
        }
    }
}
