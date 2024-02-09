using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;

namespace NAF.Api.Controllers
{
    [AllowAnonymous]
    public class HealthCheckController : NafControllerBase
    {
        public HealthCheckController(IUserAppService userAppService) : base(userAppService)
        {
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
