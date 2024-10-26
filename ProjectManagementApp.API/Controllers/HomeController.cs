using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementApp.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
