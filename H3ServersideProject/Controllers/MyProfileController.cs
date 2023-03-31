using H3ServersideProject.Attributes;
using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Helpers;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Policy;
using System.Web.Helpers;
using System.Web.WebPages;

namespace H3ServersideProject.Controllers
{
    [Auth]
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

        /// <summary>
        /// The view for MyProfile. Checks for a cookie that contains an email.
        /// If not, then it cant show the view.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// A function to logout the user. It deletes the cookie.
        /// </summary>
        /// <returns></returns>
        [HttpGet("action")]
        public IActionResult Logout()
        {
            var checkCookie = Request.Cookies["LoginCookie"];

            if (checkCookie != null)
            {
                Response.Cookies.Delete("LoginCookie");
                Response.Cookies.Delete("AuthCookie");
            }
            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        /// The view for a user to update there data.
        /// Checks for a cookie.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// The post request to update the users data. Sets the data into a temporary class.
        /// Checks again for a cookie.
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult UpdateUser([FromBody] UpdateUserDTO updateUser)
        {
            var checkCookie = Request.Cookies["LoginCookie"];

            if (checkCookie is not null)
            {
                PasswordService pswService = new PasswordService();

                // Run the method GetUserData with the email from the cookie to get all the users data.
                User tempUser = _userRepo.GetUserData(checkCookie);
                // Run the method GetUser to get the users password.
                UserPassword tempUserPassword = _userRepo.GetUser(checkCookie);

                // If's to check if the user has written anything data to update. The user can choose not update everything.
                if (!updateUser.Address.IsNullOrEmpty())
                    tempUser.Address = updateUser.Address;
                if (!updateUser.Name.IsNullOrEmpty())
                    tempUser.Name = updateUser.Name;
                if (!updateUser.PhoneNumber.IsNullOrEmpty())
                    tempUser.PhoneNumber = updateUser.PhoneNumber;
                if (!updateUser.Password.IsNullOrEmpty())
                {
                    // Hash the new password if the user choose the update it.
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
    }
}