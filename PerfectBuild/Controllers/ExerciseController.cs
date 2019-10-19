using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    public class ExerciseController:Controller
    {
        public IActionResult List()
        {
            return View("test" as object);
        }
    }
}
