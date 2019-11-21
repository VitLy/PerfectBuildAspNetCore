using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext context;
        public HomeController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Exercises.ToList());
        }
    }
}
