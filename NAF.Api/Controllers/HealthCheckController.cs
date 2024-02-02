using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NAF.Api.Controllers
{
    [AllowAnonymous]
    public class HealthCheckController : NafControllerBase
    {
        public HealthCheckController()
        {
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
