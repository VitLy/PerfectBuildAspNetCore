using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PerfectBuild.Data;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [AutoValidateAntiforgeryToken]
    public class ExerciseController : Controller
    {
        readonly ApplicationContext appContext;
        readonly private IStringLocalizer<SharedErrorMessages> localizerErrorMessage;

        readonly int itemOnPages = 6;
        bool nameAscend = true;
        bool descriptionAscend = true;

        public ExerciseController(ApplicationContext appContext, IStringLocalizer<SharedErrorMessages> localizerErrorMessage)
        {
            this.appContext = appContext;
            this.localizerErrorMessage = localizerErrorMessage;
        }

        [HttpGet]
        public IActionResult List(int currentPage = 1, string sortBy = "")
        {
            if (TempData["ElementErrorShort"] != null || TempData["ElementErrorLong"] != null)
            {
                ModelState.AddModelError((string)TempData["ElementErrorShort"], (string)TempData["ElementErrorLong"] ?? (string)TempData["ElementErrorShort"]);
            }

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
                Items = sortedExercises,
                CurrentPage = currentPage,
                ItemOnPages = itemOnPages,
                TotalItems = totalItems
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            var units = appContext.Units.ToList();
            return View("Change", new ChangeExerciseViewModel { Units = units });
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
                OwnWeight = exercise.OwnWeight,
                Units = units
            };
            return View("Change", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    Exercise exercise = appContext.Exercises.Find(id);
                    appContext.Remove(exercise);
                    await appContext.SaveChangesAsync();
                    return RedirectToAction("List");
                }
                else
                {
                    CreateTempData("ElementIdNotFoundShort", "ElementIdNotFoundLong");
                    return RedirectToAction("List");
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;
                if (sqlException != null)
                {
                    var number = sqlException.Number;

                    if (number == 547)
                    {
                        CreateTempData("ElementCantBeDeletedShort", "ElementCantBeDeletedLong");
                        return RedirectToAction("List");
                    }
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Change(ChangeExerciseViewModel viewModel)
        {
            if (viewModel != null)
            {
                if (ModelState.IsValid)
                {
                    Exercise exercise = new Exercise
                    {
                        Name = viewModel.Name,
                        Id = viewModel.Id,
                        Description = viewModel.Description,
                        UnitId = viewModel.UnitId,
                        OwnWeight = viewModel.OwnWeight
                    };

                    if (exercise.Id == 0) //ветка добавления
                    {
                        if (IsPresentInDB(exercise))
                        {
                            CreateTempData("ElementPresentinDBShort", "ElementPresentinDBLong");
                        }
                        else
                        {
                            await appContext.Exercises.AddAsync(exercise);
                            await appContext.SaveChangesAsync();
                        }
                    }
                    else // ветка обновления данных
                    {
                        if (await IsAllowedToModifyAsync(exercise))
                        {
                            appContext.Exercises.Update(exercise);
                            await appContext.SaveChangesAsync();
                        }
                        else
                        {
                            CreateTempData("ElementCannotBeChangedShort", "ElementCannotBeChangedLong");
                        }
                    }
                    return RedirectToAction("List");
                }
                else
                {
                    return View(viewModel);
                }
            }
            else
            {
                CreateTempData("ElementIdNotFoundShort", "ElementIdNotFoundLong");
            }
            return RedirectToAction("List");
        }

        #region PrivateMethods
        private void CreateTempData(string shortMessage, string longMessage)
        {
            string shortM = localizerErrorMessage[shortMessage];
            string longM = localizerErrorMessage[longMessage];
            TempData["ElementErrorShort"] = shortM;
            TempData["ElementErrorLong"] = longM;
        }

        private bool IsPresentInDB(Exercise exercise)
        {
            var isExercisePresentinDB = appContext.Exercises
                          .Where(x => x.Name.Trim().ToUpperInvariant().Equals(exercise.Name.Trim().ToUpperInvariant(), StringComparison.InvariantCulture))
                          .Any();
            if (isExercisePresentinDB)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> IsAllowedToModifyAsync(Exercise exercise)
        {
            var isFoundTrainingPlan = await appContext.TrainingPlanSpecs.Where(x => x.ExId.Equals(exercise.Id)).AnyAsync();
            var isFoundTrainingProgram = await appContext.TrainingProgramSpecs.Where(x => x.ExId.Equals(exercise.Id)).AnyAsync();
            var isFoundTraining = await appContext.TrainingSpecs.Where(x => x.ExId.Equals(exercise.Id)).AnyAsync();
            var result=isFoundTrainingPlan|isFoundTrainingProgram|isFoundTraining;
            return !result;
        }

        #endregion
    }
}
