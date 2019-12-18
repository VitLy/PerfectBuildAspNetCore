using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles="Admin,User")]
    public class CategoryController:Controller
    {
        ApplicationContext appContext;
        readonly int itemOnPages = 6;

        bool nameAscend = true;
        bool descriptionAscend = true;

        public CategoryController(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public IActionResult List(int currentPage = 1) 
        {
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
            return View("Change", new Category());
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            Category category = appContext.Categories.Find(id);
            return View("Change", category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = appContext.Categories.Find(id);
            appContext.Remove(category);
            await appContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Change(Category category)
        {
            if (category != null)
            {
                if (category.Id == 0) //ветка добавления
                {
                    if (ModelState.IsValid)
                    {
                        await appContext.Categories.AddAsync(category);
                        await appContext.SaveChangesAsync();
                    }
                }
                else // ветка обновления данных
                {
                    appContext.Categories.Update(category);
                    await appContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("List");
        }
    }
}
