using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PerfectBuild.Data;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class UnitController : Controller
    {
        private ApplicationContext appContext;
        private IStringLocalizer<SharedErrorMessages> localizerErrorMessage;
        private IStringLocalizer<UnitController> localizer;

        public UnitController(ApplicationContext appContext, IStringLocalizer<SharedErrorMessages> localizerErrorMessage, IStringLocalizer<UnitController> localizer)
        {
            this.appContext = appContext;
            this.localizerErrorMessage = localizerErrorMessage;
            this.localizer = localizer;
        }

        [HttpGet]
        public IActionResult List()
        {
            var units = appContext.Units.OrderBy(x => x.Name).ToList();
            ViewBag.Tittle = localizer["UnitList"];
            return View(units);
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                var unit = appContext.Units.Find(id);
                if (unit != null)
                {
                    appContext.Units.Remove(unit);
                    await appContext.SaveChangesAsync();
                    return RedirectToAction("List");
                }
            }
            ModelState.AddModelError(localizerErrorMessage["UnitIdNotFoundShort"], localizerErrorMessage["UnitIdNotFoundLong"]);
            return RedirectToAction("List");
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
                        appContext.Update(unit);
                    }
                    else
                    {
                        await appContext.AddAsync(unit);
                    }
                    await appContext.SaveChangesAsync();
                }
                else View(unit);
            }
            ModelState.AddModelError(localizerErrorMessage["UnitIdNotFoundShort"], localizerErrorMessage["UnitIdNotFoundLong"]);
            return RedirectToAction("List");
        }
    }
}
