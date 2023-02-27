using H3ServersideProject.Data;
using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Models;
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
        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult<User> Post([FromBody]User user)
        {
            if (ModelState.IsValid)
            {              
                _userRepo.Insert(user);
                //_userRepo.save();
                return RedirectToPage("Login");
            }

            return RedirectToPage("Login");
        }

        //[HttpPost("[action]")]
        //[Consumes("application/json")]
        //public string TestPost([FromBody] User user) => JsonConvert.SerializeObject(user);
    }
}
