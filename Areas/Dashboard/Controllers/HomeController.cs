using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.Areas.Dashboard.Controllers
{
    [Area("Dasboard")]
    public class HomeController : Controller
    {
        [Route("/Pulpit")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
