using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectBuild.Data;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult UserList()
        {
            var result = userManager.Users.ToList();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ModifyUserRoles(string id)
        {
            User user = await userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (user != null)
            {
                SortedDictionary<string, bool> roles = new SortedDictionary<string, bool>();
                var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);

                foreach (var item in userRoles)
                {
                    roles.Add(item, true);
                }
                var notUserRoles = roleManager.Roles.ToList().Select(x => x.Name).Except(userRoles);

                foreach (var item in notUserRoles)
                {
                    roles.Add(item, false);
                }

                UserRolesModel userRolesModel = new UserRolesModel()
                {
                    UserId = user.Id,
                    UserName = user.NormalizedUserName,
                    Roles = roles
                };
                return View(userRolesModel);
            }
            return RedirectToAction("UserList");
        }

        [HttpPost]
        public async Task<IActionResult> ModifyUserRoles(UserRolesModel userRoles)
        {
            if (userRoles != null)
            {
                User user = await userManager.FindByIdAsync(userRoles.UserId).ConfigureAwait(false);
                if (user != null)
                {
                    List<String> rolesToDelete = userRoles.Roles.Where(x => x.Value == false).Select(x => x.Key).ToList();
                    List<String> rolesToAdd = userRoles.Roles.Where(x => x.Value == true).Select(x => x.Key).ToList();
                    foreach (var role in rolesToDelete)
                    {
                        if (await userManager.IsInRoleAsync(user, role).ConfigureAwait(false))
                        {
                            await userManager.RemoveFromRoleAsync(user, role).ConfigureAwait(false);
                        }
                    }
                    foreach (var role in rolesToAdd)
                    {
                        if (!await userManager.IsInRoleAsync(user, role).ConfigureAwait(false))
                        {
                            await userManager.AddToRoleAsync(user, role).ConfigureAwait(false);
                        }
                    }
                }
            }
            return RedirectToAction("UserList");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                User user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    try
                    {
                        await userManager.DeleteAsync(user);
                    }
                    catch (DbUpdateException e)
                    {
                        ModelState.AddModelError(e.Message, "Cannot Delete User");
                    }
                }
            }
            return RedirectToAction("UserList");
        }
        [HttpPost]
        public async Task<IActionResult> SeedData()
        {
            //ToDo:Продумать повторное подтверждение на удаление данных и запаолнение перовначальными значениями
            //ToDo:Обновить SeedData под новую структуру базы данных

            ConfigureStartDatabase dbHandler = new ConfigureStartDatabase(HttpContext);
            dbHandler.Seed();
            return RedirectToAction("Index", "Home");
        }
    }
}
