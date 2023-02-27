using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Helpers;
using H3ServersideProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace H3ServersideProject.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserRepo _userRepo;

        public LoginController(IUserRepo userRepo)
        {
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
        [HttpPost]
        public ActionResult<User> Get([FromBody] User user)
        {           
            if (ModelState.IsValid)
            {                
                _userRepo.GetUser(user.Email);

                //_userRepo.save();
                return RedirectToPage("Login");
            }

            return RedirectToPage("Login");
        }
    }
}
