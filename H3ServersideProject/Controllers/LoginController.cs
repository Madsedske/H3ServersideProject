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
        private readonly ILogin _login;

        public LoginController(ILogin login)
        {
            _login = login;
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
        [HttpPost("[action]")]
        public ActionResult<Login> Get([FromBody] Login login)
        {
            Console.WriteLine(login.Email);
            _login.GetuserLogin(login);
            return View();  
        }
    }
}
