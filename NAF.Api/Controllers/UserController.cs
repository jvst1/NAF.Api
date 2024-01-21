using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest createUserRequest)
        {
            try
            {
                var result = await _userAppService.CreateUser(createUserRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
