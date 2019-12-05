﻿using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    public class ExerciseController:Controller
    {
        public IActionResult List(ApplicationContext context)
        {
            return View(context.Exercises);
        }
    }
}
