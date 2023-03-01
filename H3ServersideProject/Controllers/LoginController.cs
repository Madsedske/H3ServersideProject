using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace H3ServersideProject.Controllers
{
    [Route("/Login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;

        public LoginController(IUserRepo userRepo, ILogger<LoginController> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var cookie = Request.Cookies["LoginCookie"];

            if (cookie != null)
            {
                return RedirectToAction("MyProfile", "MyProfile");
            }

            return View();
        }

        [Consumes("application/json")]
        [HttpPost("Post")]
        public ActionResult<User> Post([FromBody] UserDTO userDTO)
        {
            var tokenService = new TokenService();
            var cookieOptions = new CookieOptions();

            if (ModelState.IsValid)
            {
                User tempUser = _userRepo.GetUser(userDTO.Email);

                if (tempUser.Password is not null)
                {
                    if (tempUser.Password == userDTO.Password)
                    {
                        cookieOptions.Expires = DateTime.Now.AddMinutes(2);
                        cookieOptions.Path = "/";
                        Response.Cookies.Append("LoginCookie", userDTO.Email, cookieOptions);

                        string token = tokenService.CreateToken(userDTO.Email);

                        // Set the user as logged in
                        _logger.LogInformation($"User logged in");
                        return StatusCode(200, token);
                    }
                    else
                    {
                        // Password incorrect
                        _logger.LogError($"Incorrect password");
                    }
                }
                else
                {
                    // Email doesn't exist
                    _logger.LogError($"Email doesn't exist");
                }
                //_userRepo.save();
            }

            return StatusCode(200);
        }
    }
}
