using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        readonly ApplicationContext appContext;
        readonly int itemOnPages = 6;
        bool nameAscend = true;
        bool descriptionAscend = true;

        public ExerciseController(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public IActionResult List(int currentPage = 1, string sortBy = "")
        {
            int totalItems = appContext.Exercises.Count();
            var exercises = appContext.Exercises.Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
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
            var units = appContext.Units.ToList();
            return View("Change", new ChangeExerciseViewModel {Units=units });
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            Exercise exercise = appContext.Exercises.Include(x => x.Unit).Where(x => x.Id.Equals(id)).FirstOrDefault();
            var units = appContext.Units.ToList();
            var viewModel = new ChangeExerciseViewModel
            {
                Id = exercise.Id,
                Name = exercise.Name,
                UnitId = exercise.UnitId,
                Description = exercise.Description,
                Units = units
            };
            return View("Change", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Exercise exercise = appContext.Exercises.Find(id);
            appContext.Remove(exercise);
            await appContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Change(ChangeExerciseViewModel viewModel)
        {
            if (viewModel != null)
            {
                if (viewModel.Id == 0) //ветка добавления
                {
                    if (ModelState.IsValid)
                    {
                        Exercise exercise = new Exercise
                        {
                            Name = viewModel.Name,
                            Description = viewModel.Description,
                            UnitId = viewModel.UnitId,
                            OwnWeight = viewModel.OwnWeight
                        };
                        await appContext.Exercises.AddAsync(exercise);
                        await appContext.SaveChangesAsync();
                    }
                }
                else // ветка обновления данных
                {
                    if (ModelState.IsValid)
                    {
                        Exercise exercise = appContext.Exercises.Find(viewModel.Id);
                        if (exercise != null) 
                        {
                        exercise.Name = viewModel.Name;
                        exercise.Description = viewModel.Description;
                        exercise.UnitId = viewModel.UnitId;
                        exercise.OwnWeight = viewModel.OwnWeight;
                        appContext.Exercises.Update(exercise);
                        await appContext.SaveChangesAsync();
                        }
                    }
                }
            }
            return RedirectToAction("List");
        }
    }
}
