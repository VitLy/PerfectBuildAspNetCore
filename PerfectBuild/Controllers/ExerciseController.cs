using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ExerciseController : Controller
    {
        readonly ApplicationContext context;
        readonly int itemOnPages = 6;
        bool nameAscend = true;
        bool descriptionAscend = true;

        public ExerciseController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult List(int currentPage = 1, string sortBy = "")
        {
            int totalItems = context.Exercises.Count();
            var exercises = context.Exercises.Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
            var sortedExercises = exercises.OrderBy(x => x.Name).ToList();
            if (sortBy == "name")
            {
                nameAscend = false ? nameAscend = true : nameAscend = false;
                if (nameAscend == true)
                {
                    sortedExercises = exercises.OrderBy(x => x.Name).ToList();
                }
                else
                {
                    sortedExercises = exercises.OrderBy(x => x.Name).OrderByDescending(x => x.Name).ToList();
                }
            }
            else if (sortBy == "description")
            {
                descriptionAscend = false ? nameAscend = true : descriptionAscend = false;
                if (descriptionAscend == true)
                {
                    sortedExercises = exercises.OrderBy(x => x.Description).ToList();
                }
                else
                {
                    sortedExercises = exercises.OrderByDescending(x => x.Name).ToList();
                }
            }
            return View(new ListItemViewModel<Exercise>
            {
                Items = sortedExercises,CurrentPage = currentPage,
                ItemOnPages = itemOnPages,TotalItems = totalItems
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Change", new Exercise());
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            Exercise exercise = context.Exercises.Find(id);
            return View("Change", exercise);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Exercise exercise = context.Exercises.Find(id);
            context.Remove(exercise);
            await context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Change(Exercise exercise)
        {
            if (exercise != null)
            {
                if (exercise.Id == 0) //ветка добавления
                {
                    if (ModelState.IsValid)
                    {
                        await context.Exercises.AddAsync(exercise);
                        await context.SaveChangesAsync();
                    }
                }
                else // ветка обновления данных
                {
                    context.Exercises.Update(exercise);
                    await context.SaveChangesAsync();
                }
            }
            return RedirectToAction("List");
        }
    }
}
