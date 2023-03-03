using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Headers;

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

        /// <summary>
        /// The view to show the upcoming movie page. 
        /// </summary>
        [HttpGet]
        public IActionResult Showing()
        {
            var cookie = Request.Cookies["LoginCookie"];
            return View();
        }

        /// <summary>
        /// Gets the reservation schedule for the chosen date. 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult GetReserve([FromBody] string date)
        {
            int movieID;

            // Not the right solution. Not enough time set up the right solution for date picker.
            if (date == "2023/03/13" || date == "2023/03/15" || date == "2023/03/17" || date == "2023/03/19")
            {
                movieID = 1;
            }
            else
            {
                movieID = 2;
            }
            List<int> reservedSeats = new List<int>();
            reservedSeats = _showingRepo.GetReservation(movieID, Convert.ToDateTime(date));

            return Ok(reservedSeats);
        }

        /// <summary>
        /// Post the reservation into the database and send the chosen seats.
        /// Also checks for cookie to ensure the user is logged in to send the email.
        /// The MakeReservation is only half done.
        /// </summary>
        /// <param name="seats"></param>
        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult MakeReservation([FromBody] int[] seats)
        {
            string email = Request.Cookies["LoginCookie"];
            int movieID = 1;
            DateTime date = new DateTime(2023, 03, 13);

            // If more seats is chosen, then it can loop to store them all.
            foreach (int seat in seats)
            {
                _showingRepo.InsertReservation(movieID, date, email, seat);
            }
            _logger.LogInformation($"Reservation made.");
            return StatusCode(200);
        }
    }
}
