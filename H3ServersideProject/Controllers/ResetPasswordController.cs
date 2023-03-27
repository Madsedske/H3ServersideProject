using H3ServersideProject.Data;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H3ServersideProject.Controllers
{
    [Route("/ResetPassword")]
    [ApiController]
    public class ResetPasswordController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;

        public ResetPasswordController(IUserRepo userRepo, ILogger<ResetPasswordController> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        /// <summary>
        /// View for register a user.
        /// </summary>
        [HttpGet]
        public IActionResult ResetPassword()
        {
            var cookie = Request.Cookies["LoginCookie"];
            return View();
        }

        [Consumes("application/json")]
        [HttpPost("Post")]
        public ActionResult SendEmail([FromBody] string email)
        {
            return Ok(email);
        }
    }
}
