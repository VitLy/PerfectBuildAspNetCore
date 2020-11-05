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
    public class UnitController : Controller
    {
        private ApplicationContext appContext;
        private IStringLocalizer<SharedErrorMessages> localizerErrorMessage;
        private IStringLocalizer<UnitController> localizer;

        readonly int itemOnPages = 6;

        public UnitController(ApplicationContext appContext, IStringLocalizer<SharedErrorMessages> localizerErrorMessage, IStringLocalizer<UnitController> localizer)
        {
            this.appContext = appContext;
            this.localizerErrorMessage = localizerErrorMessage;
            this.localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> List(FieldSort currentSort, int currentPage = 1, string sortBy = "")
        {
            if (TempData["ElementErrorShort"] != null || TempData["ElementErrorLong"] != null)
            {
                ModelState.AddModelError((string)TempData["ElementErrorShort"], (string)TempData["ElementErrorLong"] ?? (string)TempData["ElementErrorShort"]);
            }

            var units = await appContext.Units.ToListAsync();
            var totalItems = units.Count;

            var model = new UnitViewModel
            {
                CurrentPage = currentPage,
                CurrentSort = currentSort,
                ItemOnPages = itemOnPages,
                TotalItems = totalItems,
                SortBy = sortBy
            };

            #region Pagination
            if (String.IsNullOrEmpty(sortBy))
            {
                switch (currentSort)
                {
                    case FieldSort.shortNameAscend:
                        model.Items = units.OrderBy(x => x.ShortName).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                        break;
                    case FieldSort.shortNameDescend:
                        model.Items = units.OrderByDescending(x => x.ShortName).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                        break;
                    case FieldSort.fullNameAscend:
                        model.Items = units.OrderBy(x => x.Name).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                        break;
                    case FieldSort.descriptionDescend:
                        model.Items = units.OrderByDescending(x => x.Name).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                        break;
                    default:
                        model.Items = units.OrderBy(x => x.ShortName).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                        break;
                }
                return View(model);
            }
            #endregion

            #region Sorting
            if (sortBy == "shortName")
            {
                if (currentSort == FieldSort.shortNameAscend)
                {
                    model.CurrentSort = FieldSort.shortNameDescend;
                    model.Items = units.OrderByDescending(x => x.ShortName).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                }
                else
                {
                    model.CurrentSort = FieldSort.shortNameAscend;
                    model.Items = units.OrderBy(x => x.ShortName).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                }
            }
            else if (sortBy == "fullName")
            {
                if (currentSort == FieldSort.fullNameAscend)
                {
                    model.CurrentSort = FieldSort.fullNameDescend;
                    model.Items = units.OrderByDescending(x => x.Name).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                }
                else
                {
                    model.CurrentSort = FieldSort.fullNameAscend;
                    model.Items = units.OrderBy(x => x.Name).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
                }
            }
            else
            {
                model.CurrentSort = FieldSort.shortNameAscend;
                model.Items = units.OrderBy(x => x.ShortName).Skip((currentPage - 1) * itemOnPages).Take(itemOnPages).ToList();
            }
            #endregion

            return View(model);

        }

        [HttpGet]
        public IActionResult AddModify(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Tittle = localizer["AddUnitTittle"];
                return View(new Unit());
            }
            else
            {
                var unit = appContext.Units.Find(id);
                ViewBag.Tittle = localizer["ModifyUnitTittle"];
                if (unit != null)
                {
                    var viewModel = new Unit { Id = id, Name = unit.Name, ShortName = unit.ShortName };
                    return View(viewModel);
                }
                else
                {
                    ModelState.AddModelError(localizerErrorMessage["UnitIdNotFoundShort"], localizerErrorMessage["UnitIdNotFoundLong"]);
                }
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModify(Unit unit)
        {
            if (unit != null)
            {
                if (ModelState.IsValid)
                {
                    if (unit.Id != 0)
                    {
                        if (IsAllowedToModify(unit))
                        {
                            appContext.Update(unit);
                            await appContext.SaveChangesAsync();
                        }
                        else 
                        {
                            CreateTempData("ElementCannotBeChangedShort", "ElementCannotBeChangedLong");
                        }
                    }
                    else
                    {
                        if (IsPresentInDB(unit))
                        {
                            CreateTempData("ElementPresentinDBShort", "ElementPresentinDBLong");
                        }
                        else
                        {
                            await appContext.AddAsync(unit);
                            await appContext.SaveChangesAsync();
                        }
                    }
                }
            }
            else
            {
                CreateTempData("ElementIdNotFoundShort", "ElementIdNotFoundLong");
            }
            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    var unit = appContext.Units.Find(id);
                    if (unit != null)
                    {
                        appContext.Units.Remove(unit);
                        await appContext.SaveChangesAsync();
                    }
                }
                else
                {
                    ModelState.AddModelError(localizerErrorMessage["ElementIdNotFoundShort"], localizerErrorMessage["ElementIdNotFoundLong"]);
                    return View("List", appContext.Units.OrderBy(x => x.Name).ToList());
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

        #region PrivateMethods

        private void CreateTempData(string shortMessage, string longMessage)
        {
            string shortM = localizerErrorMessage[shortMessage];
            string longM = localizerErrorMessage[longMessage];
            TempData["ElementErrorShort"] = shortM;
            TempData["ElementErrorLong"] = longM;
        }

        private bool IsPresentInDB(Unit unit)
        {
            var isUnitPresentinDB = appContext.Units
                          .Where(x => x.Name.Trim().ToUpperInvariant().Equals(unit.Name.Trim().ToUpperInvariant(), StringComparison.InvariantCulture))
                          .Any();
            if (isUnitPresentinDB)
            {
                return true;
            }
            return false;
        }

        private bool IsAllowedToModify(Unit unit)
        {
            return !appContext.Exercises.Where(x => x.UnitId.Equals(unit.Id)).Any();
        }
        
        #endregion
    }
}
