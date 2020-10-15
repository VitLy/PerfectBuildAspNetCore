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
                Unit rpt = new Unit {ShortName = "повт", Name = "повторение" };
                Unit duration = new Unit {ShortName = "мин", Name = "минута" };
                Unit distance = new Unit {ShortName = "км", Name = "километр" };
                List<Unit> units = new List<Unit>()
                {
                    rpt,duration,distance
                };

                appContext.Units.AddRange(units);
                appContext.SaveChanges();

                var rptUnitId = appContext.Units.Where(x => x.ShortName.Equals("повт")).FirstOrDefault().Id;
                int durationUnitId = appContext.Units.Where(x => x.ShortName.Equals("мин")).FirstOrDefault().Id;
                int distanceUnitId = appContext.Units.Where(x => x.ShortName.Equals("км")).FirstOrDefault().Id;

                List<Exercise> exercises = new List<Exercise>
                            {
                                new Exercise{Name="Выпады с гантелями",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Жим гантелей лежа на полу",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Жим гантелей сидя",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Подтягивания ",Description="",OwnWeight=true,UnitId=rptUnitId},
                                new Exercise{Name="Подъем гантелей на бицепс стоя ",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Подъем гантелей на бицепс хватом молоток",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Подъем на носки с гантелями ",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Подъем ног в положении лежа ",Description="",OwnWeight=true,UnitId=rptUnitId},
                                new Exercise{Name="Подъем туловища из положения лежа ",Description="",OwnWeight=true,UnitId=rptUnitId},
                                new Exercise{Name="Приседания с гантелями ",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Становая тяга с гантелями ",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Тяга гантелей в наклоне ",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Французский жим ",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Шраги с гантелями ",Description="",OwnWeight=false,UnitId=rptUnitId},
                                new Exercise{Name="Орбитрек ",Description="",OwnWeight=false,UnitId=durationUnitId},
                            };
                appContext.Exercises.AddRange(exercises);
                appContext.SaveChanges();
            }
            #endregion

            void DeleteData()
            {
                appContext.Profiles.RemoveRange(appContext.Profiles);
                appContext.Params.RemoveRange(appContext.Params);
                appContext.TrainingProgramHeads.RemoveRange(appContext.TrainingProgramHeads);
                appContext.TrainingProgramSpecs.RemoveRange(appContext.TrainingProgramSpecs);
                appContext.TrainingPlanHeads.RemoveRange(appContext.TrainingPlanHeads);
                appContext.TrainingPlanSpecs.RemoveRange(appContext.TrainingPlanSpecs);
                appContext.TrainingHeads.RemoveRange(appContext.TrainingHeads);
                appContext.TrainingSpecs.RemoveRange(appContext.TrainingSpecs);
                appContext.Categories.RemoveRange(appContext.Categories);
                appContext.Exercises.RemoveRange(appContext.Exercises);
                appContext.Units.RemoveRange(appContext.Units);


                appContext.SaveChanges();
            }
        }
    }
}
