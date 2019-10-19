using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PerfectBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Data
{
    public static class DbInitializer
    {
        public static void CreateDb(IApplicationBuilder app)
        {
            Context context = app.ApplicationServices.GetRequiredService<Context>();

            if (!context.Users.Any())
            {
                {
                    User user = new User { Login = "VitLy", Password = "19791979" };
                    context.Users.Add(user);
                    context.SaveChanges();

                    Profile profile = new Profile
                    {
                        FName = "Vitaly",
                        LName = "Borischak",
                        DayBirth = new DateTime(1979, 04, 01),
                        Sex = true,
                        Height = 183,
                        Weight = 103,
                        UserId = user.Id
                    };
                    context.Profiles.Add(profile);
                    context.SaveChanges();

                    List<Exercise> exercises = new List<Exercise>
            {
                new Exercise{Id=1,Name="Выпады с гантелями",Description=""},
                new Exercise{Id=2,Name="Жим гантелей лежа на полу",Description=""},
                new Exercise{Id=3,Name="Жим гантелей сидя",Description=""},
                new Exercise{Id=4,Name="Подтягивания ",Description=""},
                new Exercise{Id=5,Name="Подъем гантелей на бицепс стоя ",Description=""},
                new Exercise{Id=6,Name="Подъем гантелей на бицепс хватом молоток",Description=""},
                new Exercise{Id=7,Name="Подъем на носки с гантелями ",Description=""},
                new Exercise{Id=8,Name="Подъем ног в положении лежа ",Description=""},
                new Exercise{Id=9,Name="Подъем туловища из положения лежа ",Description=""},
                new Exercise{Id=10,Name="Приседания с гантелями ",Description=""},
                new Exercise{Id=11,Name="Становая тяга с гантелями ",Description=""},
                new Exercise{Id=12,Name="Тяга гантелей в наклоне ",Description=""},
                new Exercise{Id=13,Name="Французский жим ",Description=""},
                new Exercise{Id=14,Name="Шраги с гантелями ",Description=""},
                new Exercise{Id=15,Name="Орбитрек ",Description=""},
            };

                    context.Exercises.AddRange(exercises);
                    context.SaveChanges();

                    Param param = new Param { Id = 1, ProfileId = profile.Id, Date = DateTime.Today, Weight = 102, Breast = 110, Waist = 100, Buttock = 100, Thigh = 60 };
                    context.Params.Add(param);
                    context.SaveChanges();

                    List<Category> categories = new List<Category>
            {
                new Category{Id=2,Name="Тренировка в зале"},
                new Category { Id = 1, Name = "Домашняя тренировка" },
            };

                    context.Categories.AddRange(categories);
                    context.SaveChanges();

                    List<TrainingProgramHead> trPr = new List<TrainingProgramHead>
            {
                new TrainingProgramHead { Id = 1, CategoryId = 1,Date = new DateTime(2019, 11, 02),Name = "Тренировка1: Ноги, плечи, пресс" },
                new TrainingProgramHead { Id = 2, CategoryId = 1,Date = new DateTime(2019, 11, 02),Name = "Тренировка2: Спина, грудь" },
                new TrainingProgramHead { Id = 3, CategoryId = 1,Date = new DateTime(2019, 11, 02),Name = "Тренировка3: Бицепс, трицепс, пресс" },
                new TrainingProgramHead { Id = 4, CategoryId = 1,Date = new DateTime(2019, 11, 02),Name = "Кардио" },
            };

                    context.TrainingProgramHeads.AddRange(trPr);
                    context.SaveChanges();


                    List<TrainingProgramSpec> trSp1 = new List<TrainingProgramSpec>
        {
                //Приседания с гантелями                   12,3     
                //Становая тяга с гантелями                12,3    
                //Жим гантелей сидя                         9.8     
                //Подъем на носки с гантелями               9.8     
                //Шраги с гантелями                         9.8     
                //Подъем туловища из положения лежа    
                //Подтягивания                  
            new TrainingProgramSpec {Id=1,ExId=10,ProgramHeadId=1},
            new TrainingProgramSpec {Id=2,ExId=11,ProgramHeadId=1},
            new TrainingProgramSpec {Id=3,ExId=3,ProgramHeadId=1},
            new TrainingProgramSpec {Id=4,ExId=7,ProgramHeadId=1},
            new TrainingProgramSpec {Id=5,ExId=14,ProgramHeadId=1},
            new TrainingProgramSpec {Id=6,ExId=9,ProgramHeadId=1},
            new TrainingProgramSpec {Id=7,ExId = 4, ProgramHeadId = 1 }
        };
                    context.TrainingProgramSpecs.AddRange(trSp1);
                    context.SaveChanges();

                    List<Set> sets = new List<Set>
            {
                new Set{TrPrSpecId=1,SetNum=1,Weight=12.3f,SetAmount=12},//Приседания с гантелями, 3х12 = 12+12+12    
                new Set{TrPrSpecId=1,SetNum=2,Weight=12.3f,SetAmount=12},//Приседания с гантелями, 3х12 = 12+12+12   
                new Set{TrPrSpecId=1,SetNum=3,Weight=12.3f,SetAmount=12},//Приседания с гантелями, 3х12 = 12+12+12   
                new Set{TrPrSpecId=2,SetNum=1,Weight=12.3f,SetAmount=12},//Становая тяга с гантелями, 3x12 
                new Set{TrPrSpecId=2,SetNum=2,Weight=12.3f,SetAmount=12},//Становая тяга с гантелями, 3x12 
                new Set{TrPrSpecId=2,SetNum=3,Weight=12.3f,SetAmount=12},//Становая тяга с гантелями, 3x12 
                new Set{TrPrSpecId=3,SetNum=1,Weight=9.8f,SetAmount=12},//Жим гантелей сидя, 3x12    
                new Set{TrPrSpecId=3,SetNum=2,Weight=9.8f,SetAmount=12},//Жим гантелей сидя, 3x12   
                new Set{TrPrSpecId=3,SetNum=3,Weight=9.8f,SetAmount=12},//Жим гантелей сидя, 3x12   
                new Set{TrPrSpecId=4,SetNum=1,Weight=12.3f,SetAmount=20},//Подъем на носки с гантелями, 3х20
                new Set{TrPrSpecId=4,SetNum=2,Weight=12.3f,SetAmount=20},//Подъем на носки с гантелями, 3х20
                new Set{TrPrSpecId=4,SetNum=3,Weight=12.3f,SetAmount=20},//Подъем на носки с гантелями, 3х20
                new Set{TrPrSpecId=5,SetNum=1,Weight=12.3f,SetAmount=15},//Шраги с гантелями, 3х15
                new Set{TrPrSpecId=5,SetNum=2,Weight=12.3f,SetAmount=15},//Шраги с гантелями, 3х15
                new Set{TrPrSpecId=5,SetNum=3,Weight=12.3f,SetAmount=15},//Шраги с гантелями, 3х15
                new Set{TrPrSpecId=6,SetNum=1,SetAmount=25},//Подъем туловища из положения лежа, 3х25 
                new Set{TrPrSpecId=6,SetNum=2,SetAmount=25},//Подъем туловища из положения лежа, 3х25 
                new Set{TrPrSpecId=6,SetNum=3,SetAmount=12},//Подъем туловища из положения лежа, 3х25 
                new Set{TrPrSpecId=7,SetNum=1,SetAmount=12},//Подтягивания, 3х12  
                new Set{TrPrSpecId=7,SetNum=2,SetAmount=12},//Подтягивания, 3х12  
                new Set{TrPrSpecId=7,SetNum=3,SetAmount=12}//Подтягивания, 3х12  
            };
                    context.Sets.AddRange(sets);
                    context.SaveChanges();
                }
            }
        }
       
    }
}
