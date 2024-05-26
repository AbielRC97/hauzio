using Microsoft.AspNetCore.Mvc;

namespace hauzio.webapi.Controllers
{
    public class UbicacionController : Controller
    {
        [HttpGet]
        [Route("/locations")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
