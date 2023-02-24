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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Priser()
        {
            return View();
        }

        public IActionResult Program()
        {
            return View();
        }
    }
}