using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PerfectBuild.Data;
using PerfectBuild.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Infrastructure
{
    /// <summary>
    /// Заполнение базы данных первоначальными данными
    /// Предполагается, что фукнционал используется из рабочего кабинета администратора
    /// Перед заполнением, все базы, кроме Identity очищаются
    /// </summary>
    internal class ConfigureStartDatabase
    {
        readonly private HttpContext httpContext;
        private ApplicationContext appContext;

        public ConfigureStartDatabase(HttpContext httpContext)
        {
            this.httpContext = httpContext;
            this.appContext = httpContext.RequestServices.GetRequiredService<ApplicationContext>();
        }

        internal void Seed()
        {
            UserManager<User> userManager = httpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var currentUserId = userManager.GetUserId(httpContext.User);

            if (!string.IsNullOrWhiteSpace(currentUserId))
            {
                if ((appContext.Exercises.Count() + appContext.Params.Count() + appContext.Profiles.Count() +
                    appContext.Categories.Count() + appContext.TrainingProgramHeads.Count() + appContext.TrainingProgramSpecs.Count() +
                     appContext.TrainingHeads.Count() + appContext.TrainingSpecs.Count()) > 0)

                {
                    DeleteData();
                }
                AddProfile(currentUserId);
                AddExercises();
            }
            else
            {
                throw new UnauthorizedAccessException(nameof(httpContext.User));
            }

            #region AddTables
            void AddProfile(string userId)
            {
                Profile profile = new Profile
                {
                    UserId = userId,
                    FName = "Admin",
                    LName = "Admin",
                    DayBirth = new DateTime(1991, 08, 23),
                    Sex = Sex.Man,
                    Height = 190,
                    Weight = 90
                };
                appContext.Profiles.Add(profile);
                appContext.SaveChanges();
            }

            void AddExercises()
            {
                List<Exercise> exercises = new List<Exercise>
                            {
                                new Exercise{Name="Выпады с гантелями",Description="",OwnWeight=false},
                                new Exercise{Name="Жим гантелей лежа на полу",Description="",OwnWeight=false},
                                new Exercise{Name="Жим гантелей сидя",Description="",OwnWeight=false},
                                new Exercise{Name="Подтягивания ",Description="",OwnWeight=true},
                                new Exercise{Name="Подъем гантелей на бицепс стоя ",Description="",OwnWeight=false},
                                new Exercise{Name="Подъем гантелей на бицепс хватом молоток",Description="",OwnWeight=false},
                                new Exercise{Name="Подъем на носки с гантелями ",Description="",OwnWeight=false},
                                new Exercise{Name="Подъем ног в положении лежа ",Description="",OwnWeight=true},
                                new Exercise{Name="Подъем туловища из положения лежа ",Description="",OwnWeight=true},
                                new Exercise{Name="Приседания с гантелями ",Description="",OwnWeight=false},
                                new Exercise{Name="Становая тяга с гантелями ",Description="",OwnWeight=false},
                                new Exercise{Name="Тяга гантелей в наклоне ",Description="",OwnWeight=false},
                                new Exercise{Name="Французский жим ",Description="",OwnWeight=false},
                                new Exercise{Name="Шраги с гантелями ",Description="",OwnWeight=false},
                                new Exercise{Name="Орбитрек ",Description="",OwnWeight=false},
                            };
                appContext.Exercises.AddRange(exercises);
                appContext.SaveChanges();
            }
            #endregion

            void DeleteData()
            {
                appContext.Exercises.RemoveRange(appContext.Exercises);
                appContext.Params.RemoveRange(appContext.Params);
                appContext.Profiles.RemoveRange(appContext.Profiles);
                appContext.Categories.RemoveRange(appContext.Categories);
                appContext.TrainingHeads.RemoveRange(appContext.TrainingHeads);
                appContext.TrainingSpecs.RemoveRange(appContext.TrainingSpecs);
                appContext.TrainingProgramHeads.RemoveRange(appContext.TrainingProgramHeads);
                appContext.TrainingProgramSpecs.RemoveRange(appContext.TrainingProgramSpecs);

                appContext.SaveChanges();
            }
        }
    }
}
