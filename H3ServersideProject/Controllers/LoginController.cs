using Microsoft.AspNetCore.Mvc;

namespace H3ServersideProject.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : Controller
    {

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
    }
}
