using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace H3ServersideProject.Controllers
{
    [Route("MyProfile")]
    [ApiController]
    public class MyProfileController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;

        public MyProfileController(IUserRepo userRepo, ILogger<MyProfileController> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [Produces("application/json")]
        [HttpGet, Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult MyProfile()
        {
            var checkCookie = Request.Cookies["LoginCookie"];

            if (checkCookie != null)
            {
                User tempUser = _userRepo.GetUserData(checkCookie);
                return View(tempUser);
            }
            else
            {
                // Email doesn't exist
                _logger.LogError($"User isn't logged in");
                return View();
            }
        }

        [HttpGet("action")]
        public IActionResult Logout()
        {
            var checkCookie = Request.Cookies["LoginCookie"];

            if (checkCookie != null)
            {
                Response.Cookies.Delete("LoginCookie");

            }
            return RedirectToAction("Index", "Login");

        }
    }
}
