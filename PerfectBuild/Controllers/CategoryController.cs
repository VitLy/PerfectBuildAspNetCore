using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using PerfectBuild.Data;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [AutoValidateAntiforgeryToken]
    public class CategoryController : Controller
    {
        ApplicationContext appContext;
        private readonly IStringLocalizer<SharedErrorMessages> localizerErrorMessage;
        readonly int itemOnPages = 6;

        bool nameAscend = true;
        bool descriptionAscend = true;

        public CategoryController(ApplicationContext appContext, IStringLocalizer<SharedErrorMessages> localizerErrorMessage)
        {
            this.appContext = appContext;
            this.localizerErrorMessage = localizerErrorMessage;
        }

        [HttpGet]
        public IActionResult List(int currentPage = 1)
        {
            if (TempData["ElementErrorShort"] != null || TempData["ElementErrorLong"] != null)
            {
                ModelState.AddModelError((string)TempData["ElementErrorShort"], (string)TempData["ElementErrorLong"] ?? (string)TempData["ElementErrorShort"]);
            }
            int totalItems = appContext.Categories.Count();
            var categories = appContext.Categories.ToList();
            var selectedCategories = categories.Skip(currentPage - 1).Take(itemOnPages).ToList();
            ListItemViewModel<Category> categoryViewModel = new ListItemViewModel<Category>
            {
                Items = selectedCategories,
                CurrentPage = currentPage,
                ItemOnPages = itemOnPages,
                TotalItems = totalItems
            };
            return View(categoryViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Tittle = "AddCategory";
            return View("Change", new Category());
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            ViewBag.Tittle = "ModifyCategory";
            Category category = appContext.Categories.Find(id);
            return View("Change", category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    var category = appContext.Categories.Find(id);
                    if (category != null)
                    {
                        appContext.Categories.Remove(category);
                        await appContext.SaveChangesAsync();
                    }
                }
                else
                {
                    ModelState.AddModelError(localizerErrorMessage["ElementIdNotFoundShort"], localizerErrorMessage["ElementIdNotFoundLong"]);
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
                        string messageElementCantBeDeletedShort = localizerErrorMessage["ElementCantBeDeletedShort"];
                        string messageElementCantBeDeletedLong = localizerErrorMessage["ElementCantBeDeletedLong"];
                        TempData["ElementErrorShort"] = messageElementCantBeDeletedShort;
                        TempData["ElementErrorLong"] = messageElementCantBeDeletedLong;
                        return RedirectToAction("List");
                    }
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Change(Category category)
        {
            if (category != null)
            {
                if (ModelState.IsValid)
                {
                    if (category.Id == 0) //ветка добавления
                    {
                        if (IsPresentInDB(category))
                        {
                            CreateTempData("ElementPresentinDBShort", "ElementPresentinDBLong");
                            return RedirectToAction("List");
                        }
                        await appContext.Categories.AddAsync(category);
                        await appContext.SaveChangesAsync();
                    }
                    else // ветка обновления данных
                    {
                        if (IsAllowedToModify(category))
                        {
                            appContext.Categories.Update(category);
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
                    return View(category);
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

        private bool IsPresentInDB(Category category)
        {
            var isUnitPresentinDB = appContext.Categories
                          .Where(x => x.Name.Trim().ToUpperInvariant().Equals(category.Name.Trim().ToUpperInvariant(), StringComparison.InvariantCulture))
                          .Any();
            if (isUnitPresentinDB)
            {
                return true;
            }
            return false;
        }

        private bool IsAllowedToModify(Category category)
        {
            return !appContext.TrainingProgramHeads.Where(x => x.CategoryId.Equals(category.Id)).Any();
        }

        #endregion
    }
}
