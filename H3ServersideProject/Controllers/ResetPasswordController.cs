using H3ServersideProject.Data;
using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Helpers;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H3ServersideProject.Controllers
{
    [Route("/ResetPassword")]
    [ApiController]
    public class ResetPasswordController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;

        public ResetPasswordController(IUserRepo userRepo, ILogger<ResetPasswordController> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        /// <summary>
        /// View for register a user.
        /// </summary>
        [HttpGet]
        public IActionResult ResetPassword()
        {
            var cookie = Request.Cookies["LoginCookie"];
            return View();
        }

        // This sends an email with a new password once a user requests it with their email. This solution would need more security
        // as there is no verification that the user requesting a new password is that actual holder of the email.
        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult SendPassword([FromBody] UserDTO userDTO)
        {
            SendEmailHelper sendEmailHelper = new SendEmailHelper();
            PasswordService passwordService = new PasswordService();

            User tempUser = _userRepo.GetUserData(userDTO.Email);
            string newPassword = passwordService.RandomPassword();

            sendEmailHelper.SendMail(userDTO.Email, newPassword, tempUser);

            UserPassword userPassword = new UserPassword();

            passwordService.CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            userPassword.PasswordHash = passwordHash;
            userPassword.PasswordSalt = passwordSalt;

            _userRepo.Update(tempUser, userPassword);

            return Ok(userDTO.Email);
        }
    }
}
