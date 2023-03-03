using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Helpers;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Web.Helpers;
using System.Web.WebPages;

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
        [HttpGet]
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


        [HttpGet("GetUserView")]
        public IActionResult GetUserView()
        {
            var checkCookie = Request.Cookies["LoginCookie"];

            if (checkCookie != null)
            {
                return View("GetUserView");
            }
            else
            {
                // Email doesn't exist
                _logger.LogError($"User isn't logged in");
            }
            return View("Home");
        }


        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult UpdateUser([FromBody] UpdateUserDTO updateUser)
        {
            var checkCookie = Request.Cookies["LoginCookie"];

            if (checkCookie is not null)
            {
                PasswordService pswService = new PasswordService();
                User tempUser = _userRepo.GetUserData(checkCookie);
                UserPassword tempUserPassword = _userRepo.GetUser(checkCookie);

                if (!updateUser.Address.IsNullOrEmpty())
                    tempUser.Address = updateUser.Address;
                if (!updateUser.Name.IsNullOrEmpty())
                    tempUser.Name = updateUser.Name;
                if (!updateUser.PhoneNumber.IsNullOrEmpty())
                    tempUser.PhoneNumber = updateUser.PhoneNumber;
                if (!updateUser.Password.IsNullOrEmpty())
                {
                    pswService.CreatePasswordHash(updateUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    tempUserPassword.PasswordHash = passwordHash;
                    tempUserPassword.PasswordSalt = passwordSalt;
                }
                _userRepo.Update(tempUser, tempUserPassword);

                return View("MyProfile");
            }
            else
            {
                // Email doesn't exist
                _logger.LogError($"User isn't logged in");
            }           
            return View("Home");
        }


        //[HttpGet("action")]
        //public IActionResult Delete()
        //{
        //    var checkCookie = Request.Cookies["LoginCookie"];

        //    if (checkCookie != null)
        //    {
        //        _userRepo.RemoveUser(checkCookie);
        //    }
        //    return RedirectToAction("Index", "Login");
        //}
    }
}