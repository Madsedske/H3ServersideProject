using H3ServersideProject.Data;
using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Helpers;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml;

namespace H3ServersideProject.Controllers
{
    [Route("/Register")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;

        public RegisterController(IUserRepo userRepo, ILogger<RegisterController> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var cookie = Request.Cookies["LoginCookie"];
            return View();
        }

        // POST: api/Users
        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult<User> Post([FromBody] User user)
        {
            PasswordService pswService = new PasswordService();
            
            try
            {
                if (ModelState.IsValid)
                {
                    UserPassword userData= new UserPassword();

                    pswService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    userData.PasswordHash = passwordHash;
                    userData.PasswordSalt = passwordSalt;

                    _userRepo.Insert(user, userData);
                    _logger.LogInformation($"User registered.");
                    return StatusCode(200);
                }
                return RedirectToPage("Login");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed on creating user: {ex}");
            }
        }

        //[HttpPost("[action]")]
        //[Consumes("application/json")]
        //public string TestPost([FromBody] User user) => JsonConvert.SerializeObject(user);
    }
}
