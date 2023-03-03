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

        [HttpGet]
        public IActionResult Showing()
        {
            var cookie = Request.Cookies["LoginCookie"];
            return View();
        }


        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult GetReserve([FromBody] string date)
        {
            int movieID;

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

        [Consumes("application/json")]
        [HttpPost("[action]")]
        public ActionResult MakeReservation([FromBody] int[] seats)
        {
            string email = Request.Cookies["LoginCookie"];
            int movieID = 1;
            DateTime date = new DateTime(2023, 03, 13);

            //if (date == "2023/03/13" || date == "2023/03/15" || date == "2023/03/17" || date == "2023/03/19")
            //    movieID = 1;
            //else if (date == "2023/03/14" || date == "2023/03/16" || date == "2023/03/18" || date == "2023/03/20")
            //    movieID = 2;
            //else
            //{
            //    movieID = 3;
            //    _logger.LogError($"Wrong Date");
            //    return StatusCode(500);
            //}

            foreach (int seat in seats)
            {
                _showingRepo.InsertReservation(movieID, date, email, seat);
            }

            _logger.LogInformation($"Reservation made.");
            return StatusCode(200);
        }
    }
}
