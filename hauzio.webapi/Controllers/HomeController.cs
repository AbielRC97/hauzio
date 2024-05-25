using Microsoft.AspNetCore.Mvc;

namespace hauzio.webapi.Connections
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
