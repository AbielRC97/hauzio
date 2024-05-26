﻿using Microsoft.AspNetCore.Mvc;

namespace hauzio.webapi.Connections
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/Login")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/api/Locations")]
        public IActionResult Location()
        {
            return View();
        }
    }
}
