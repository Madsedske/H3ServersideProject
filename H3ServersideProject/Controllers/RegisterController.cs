using H3ServersideProject.Data;
using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace H3ServersideProject.Controllers
{
    [Route("Register")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IUserRepo _userRepo;

        public RegisterController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _userRepo.Insert(user);
            _userRepo.save();
            return RedirectToPage("Login");
        }
    }
}
