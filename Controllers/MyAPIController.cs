using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyAPIController : ControllerBase
    {
        [HttpGet("admin-only")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok(new { displayedMessage = "This endpoint requires the Admin role." });
        }

        [HttpGet("manager-or-admin")]
        [Authorize(Policy = "ManagerOrAdmin")]
        public IActionResult ManagerOrAdminEndpoint()
        {
            return Ok(new { displayedMessage = "This endpoint requires either the Manager or Admin role." });
        }

        [HttpGet("authenticated-user")]
        [Authorize(Policy = "AuthenticatedUser")]
        public IActionResult AuthenticatedUserEndpoint()
        {
            return Ok(new { displayedMessage = "This endpoint requires the user to be authenticated." });
        }

        [HttpGet("minimum-age-user")]
        [Authorize(Policy = "MinimumAgePolicy")]
        public IActionResult MinimumAgedUserEndpoint()
        {
            return Ok(new { displayedMessage = "This endpoint requires the user to have minimum age of 18." });
        }
    }
}
