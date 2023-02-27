using H3ServersideProject.Data;
using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Helpers;
using H3ServersideProject.Models;
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

        public LoginController(IUserRepo userRepo, ILogger<RegisterController> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //// GET: api/SetCookie
        //[HttpGet]
        //public string Get(string favoriteMilkshake)
        //{
        //    CookieOptions co = new CookieOptions();
        //    co.Expires = DateTimeOffset.Now.AddMinutes(1);
        //    Response.Cookies.Append("favoriteMilkshake", favoriteMilkshake, co);
        //    //do stuff her
        //    return favoriteMilkshake;
        //}

        [Consumes("application/json")]
        [HttpPost("Post")]
        public ActionResult<User> Post([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                User tempUser = _userRepo.GetUser(userDTO.Email);
                //tempUser.Password = "Skipperskræk1";
                if (tempUser.Password is not null)
                {
                    if (tempUser.Password == userDTO.Password)
                    {
                        // Set the user as logged in
                        _logger.LogInformation($"User logged in");
                        return StatusCode(200);
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
