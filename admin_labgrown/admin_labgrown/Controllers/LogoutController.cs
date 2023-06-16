using Microsoft.AspNetCore.Mvc;

namespace admin_labgrown.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("login", "0");
            return RedirectToAction("Index", "Login");
        }
    }
}
