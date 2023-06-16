using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using user_labgrown.Models;

namespace user_labgrown.Controllers
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
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            else
            {
                ViewBag.Login = false;
            }
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}