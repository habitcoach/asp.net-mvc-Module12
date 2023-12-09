using Microsoft.AspNetCore.Mvc;
using sessionDemo.Models;
using System.Diagnostics;

namespace sessionDemo.Controllers
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
            // Set a value in the session
            HttpContext.Session.SetString("UserName", "John");
           


            //cookie (just add this code here after installing this package Install-Package Microsoft.AspNetCore.Http


            // Set a cookie
            Response.Cookies.Append("username", "JohnDoeCookie", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1) // Cookie expiration time (1 day in this example)
            });

            return View();
        }

        public IActionResult Privacy()

        {
            //session
            string userName = HttpContext.Session.GetString("UserName");

            ViewData["username"] = userName;

            //cookie

            var usernameCookie = Request.Cookies["username"];

            ViewBag.UsernameCookie = usernameCookie;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}