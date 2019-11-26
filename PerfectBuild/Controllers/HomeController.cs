using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Data;
using System.Linq;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "User")]
    [Authorize(Roles = "Admin")]

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
