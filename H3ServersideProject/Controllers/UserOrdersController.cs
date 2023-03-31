using H3ServersideProject.Attributes;
using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H3ServersideProject.Controllers
{
    [Auth]
    [Route("/UserOrder")]
    [ApiController]
    public class UserOrdersController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;

        public UserOrdersController(IUserRepo userRepo, ILogger<UserOrdersController> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [Produces("application/json")]
        [HttpGet]
        public IActionResult Orders()
        {
            var checkCookie = Request.Cookies["LoginCookie"];

            List<GetUserReservation> getUserReservation = _userRepo.GetUserReservation(checkCookie);

            ViewBag.Order = getUserReservation;
            return View();
        }
    }
}
