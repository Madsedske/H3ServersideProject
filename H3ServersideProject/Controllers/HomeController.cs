using H3ServersideProject.Attributes;
using H3ServersideProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace H3ServersideProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The main view.
        /// </summary>
        public IActionResult Index() 
        {
            return View();
        }


        /// <summary>
        /// The view for the prices.
        /// </summary>
        public IActionResult Priser()
        {            
            return View();
        }

        [Auth]
        /// <summary>
        /// The view for the receipt.
        /// </summary>
        public IActionResult Receipt()
        {
            return View();
        }
    }
}