using admin_labgrown.Models;
using Microsoft.AspNetCore.Mvc;

namespace admin_labgrown.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("login") == "1")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login tblRegister)
        {
            DateTime dateTime = DateTime.Now;
            String passerror = "Password is not correct";
            String usererror = "Username is not correct";

            String pass = "Labgrown@" + dateTime.ToString("dd/MM/yyyy");
            String username = "Labgrown@2023";
            Console.WriteLine(pass);
            if (tblRegister.Password != null && tblRegister.Password == pass && tblRegister.Username == username && tblRegister.Username != null)
            {
                HttpContext.Session.SetString("login", "1");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if(tblRegister.Password != pass && tblRegister.Password != null)
                {
                    ViewBag.Error = passerror;
                    return View();
                }
                else if(tblRegister.Username != username && tblRegister.Username != null)
                {
                    ViewBag.Error = usererror;
                    return View();
                }
                else
                {
                    return View();
                }
            }
            
        }
    }
}
