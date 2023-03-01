using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H3ServersideProject.Controllers
{
    [Route("Showing")]
    [ApiController]
    public class ShowingController : Controller
    {
        private readonly ILogger _logger;
        private readonly IShowingRepo _showingRepo;

        public ShowingController(IShowingRepo showingRepo, ILogger<ShowingController> logger)
        {
            _logger = logger;
            _showingRepo = showingRepo;
        }

        [HttpGet]
        public IActionResult Showing()
        {
            var cookie = Request.Cookies["LoginCookie"];
            return View();
        }

        [HttpGet("action")]
        public void GetReserve()
        {
            int movieID = 1; // Get from website
            DateTime date = DateTime.Now; // Get from website

            List<int> reservedSeats = new List<int>();
            reservedSeats = _showingRepo.GetReservation(movieID, date);
            foreach (int seat in reservedSeats)
            {
                // Javascript change [seat] color to red
            }
        }
    }
}
