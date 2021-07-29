using Microsoft.AspNetCore.Mvc;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return RedirectToAction("Login", "User");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
