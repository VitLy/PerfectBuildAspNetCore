using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PerfectBuild.Controllers;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.Document;
using PerfectBuild.Models.ViewModels;
using PerfectBuild.Services.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace PerfectBuild.Tests
{
    public class StatisticMenuControllerTests
    {
        // TODO: Изолировать зависимость HttpContext для получения значения User 
        //[Fact]
        //public void IsCorrectQuerryByGetLastTraining()
        //{
        //    //
        //    #region Arrange
        //    var exercises = new List<Exercise>
        //    {
        //       new Exercise{Id=1,Name="Подтягивание", Description="Упражнение для модульного тестирования"},
        //       new Exercise{Id=2,Name="Отжимание", Description="Упражнение для модульного тестирования"},
        //       new Exercise{Id=3,Name="Жим лежа", Description="Упражнение для модульного тестирования"},
        //       new Exercise{Id=4,Name="Становая тяга", Description="Упражнение для модульного тестирования"},
        //       new Exercise{Id=5,Name="Французский жим", Description="Упражнение для модульного тестирования"},
        //       new Exercise{Id=6,Name="Кардио", Description="Упражнение для модульного тестирования"},
        //       new Exercise{Id=7,Name="Приседания", Description="Упражнение для модульного тестирования"}
        //    };
        //    User petr = new User
        //    {
        //        Id = "1",
        //        UserName = "Petr"
        //    };
        //    User vitaly = new User
        //    {
        //        Id = "2",
        //        UserName = "Vitaly"
        //    };
        //    var users = new List<User>();
        //    users.Add(petr);
        //    users.Add(vitaly);

        //    var username = "FakeUserName";
        //    var identity = new GenericIdentity(username, "");

        //    long assertedDate = new DateTime(2020, 06, 01, 12, 00, 00, DateTimeKind.Utc).Ticks;
        //    var dbSetMockTrainingHead = new Mock<DbSet<TrainingHead>>();
        //    var dbSetMockTrainingSpec = new Mock<DbSet<TrainingSpec>>();
        //    var mockContext = new Mock<ApplicationContext>();
        //    var mockProvider = new Mock<CanvasJSProvider>();
        //    var mockPrincipal = new Mock<ClaimsPrincipal>();
        //    mockPrincipal.Setup(x => x.Identity).Returns(identity);
        //    mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

        //    var mockHttpContext = new Mock<HttpContext>();
        //    mockHttpContext.Setup(m => m.User).Returns(mockPrincipal.Object);
        //    mockHttpContext.Setup(x => x.User).Returns(new GenericPrincipal(new GenericIdentity(String.Empty),new string[0]));
        //    var mockUserManagerProvider = MockUserManager<User>(users);
        //    mockContext.As<IQueryable<TrainingHead>>().Setup(repo => repo.Provider).Returns(GetHeadData(users).Provider);
        //    mockUserManagerProvider.Setup(op => op.GetUserId(mockHttpContext.Object.User)).Returns("2");

        //    StatisticMenuController statisticController = new StatisticMenuController(mockContext.Object, mockProvider.Object, mockUserManagerProvider.Object);

        //    LastTraining lastTrainingStatistics = new LastTraining
        //    {
        //        StartedTraining = assertedDate,
        //        EndTraining = assertedDate,
        //        Exercises = new List<Line>
        //        {
        //            new Line { Id = 1, Exercise = exercises[3].Name, ExerciseId = exercises[3].Id, Set = 1, Weight = 100, Amount = 4, Order = 1 },
        //            new Line { Id = 2, Exercise = exercises[4].Name, ExerciseId = exercises[4].Id, Set = 1, Weight = 100, Amount = 15, Order = 2 },
        //            new Line { Id = 3, Exercise = exercises[6].Name, ExerciseId = exercises[6].Id, Set = 1, Weight = 12, Amount = 15, Order = 3 },
        //            new Line { Id = 4, Exercise = exercises[3].Name, ExerciseId = exercises[3].Id, Set = 2, Weight = 100, Amount = 3, Order = 4 },
        //            new Line { Id = 5, Exercise = exercises[4].Name, ExerciseId = exercises[4].Id, Set = 2, Weight = 100, Amount = 12, Order = 5 },
        //            new Line { Id = 6, Exercise = exercises[6].Name, ExerciseId = exercises[6].Id, Set = 2, Weight = 12, Amount = 14, Order = 6 }
        //        }
        //    };
        //    UserSummaryStatistics userSummaryData = new UserSummaryStatistics { Date = assertedDate, UserId = "2", LastTraining = lastTrainingStatistics, UserName = "Vitaly" };
        //    #endregion
        //    //Act
        //    var model = (statisticController.MainStatisticsPage() as ViewResult)?.ViewData.Model as UserSummaryStatistics;

        //    //Assert
        //    Assert.True(model.Equals(userSummaryData));
        //}
        //#region ArrangeGetDataPrivateMethods
        //private IQueryable<TrainingHead> GetHeadData(List<User> users)
        //{
        //    var data = new List<TrainingHead>
        //    {
        //        new TrainingHead{Id=1,Calories=250,User=users.Find(x=>x.UserName=="Vitaly"),UserId=users.Find(x=>x.UserName=="Vitaly").Id,Name="Тестовая тренировка1",Date=new DateTime(2020,06,01,11,00,03,DateTimeKind.Utc),DateEnd=new DateTime(2020,06,01,11,00,03,DateTimeKind.Utc)},
        //        new TrainingHead{Id=2,Calories=251,User=users.Find(x=>x.UserName=="Petr"),UserId=users.Find(x=>x.UserName=="Petr").Id,Name="Тестовая тренировка2",Date=new DateTime(2020,06,01,11,00,02,DateTimeKind.Utc),DateEnd=new DateTime(2020,06,01,11,00,02,DateTimeKind.Utc)},
        //        new TrainingHead{Id=3,Calories=252,User=users.Find(x=>x.UserName=="Vitaly"),UserId=users.Find(x=>x.UserName=="Vitaly").Id,Name="Тестовая тренировка3",Date=new DateTime(2020,06,01,12,00,00,DateTimeKind.Utc),DateEnd=new DateTime(2020,06,01,12,00,00,DateTimeKind.Utc)},
        //        new TrainingHead{Id=4,Calories=253,User=users.Find(x=>x.UserName=="Vitaly"),UserId=users.Find(x=>x.UserName=="Vitaly").Id,Name="Тестовая тренировка4",Date=new DateTime(2020,05,12,11,00,00,DateTimeKind.Utc),DateEnd=new DateTime(2020,05,12,11,00,00,DateTimeKind.Utc)},
        //    }.AsQueryable<TrainingHead>();
        //    return data;
        //}

        //public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        //{
        //    var store = new Mock<IUserStore<TUser>>();
        //    var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        //    mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        //    mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        //    mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        //    mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
        //    mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        //    mgr.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("2");

        //    return mgr;
        //}

        //private IQueryable<TrainingSpec> GetSpecData(ref List<Exercise> exercises)
        //{
        //    return new List<TrainingSpec>
        //   {
        //        //Vitaly1
        //       new TrainingSpec{Id=1,Exercise=exercises[0],ExId=exercises[0].Id,HeadId=1,Set=1,Weight=100,Amount=4,Order=1},
        //       new TrainingSpec{Id=2,Exercise=exercises[1],ExId=exercises[0].Id,HeadId=1,Set=1,Weight=100,Amount=15,Order=2},
        //       new TrainingSpec{Id=3,Exercise=exercises[2],ExId=exercises[0].Id,HeadId=1,Set=1,Weight=15,Amount=15,Order=3},
        //       new TrainingSpec{Id=4,Exercise=exercises[3],ExId=exercises[0].Id,HeadId=1,Set=1,Weight=12,Amount=15,Order=4},
        //       new TrainingSpec{Id=5,Exercise=exercises[0],ExId=exercises[0].Id,HeadId=1,Set=2,Weight=100,Amount=3,Order=5},
        //       new TrainingSpec{Id=6,Exercise=exercises[1],ExId=exercises[0].Id,HeadId=1,Set=2,Weight=100,Amount=12,Order=6},
        //       new TrainingSpec{Id=7,Exercise=exercises[2],ExId=exercises[0].Id,HeadId=1,Set=2,Weight=15,Amount=15,Order=7},
        //       new TrainingSpec{Id=8,Exercise=exercises[3],ExId=exercises[0].Id,HeadId=1,Set=2,Weight=12,Amount=14,Order=8},
        //       //petr1
        //       new TrainingSpec{Id=9,Exercise=exercises[5],ExId=exercises[5].Id,HeadId=2,Weight=100,Amount=45,Order=1},

        //       //Vitaly2
        //       new TrainingSpec{Id=10,Exercise=exercises[4],ExId=exercises[4].Id,HeadId=3,Set=1,Weight=100,Amount=4,Order=1},
        //       new TrainingSpec{Id=11,Exercise=exercises[5],ExId=exercises[5].Id,HeadId=3,Set=1,Weight=100,Amount=15,Order=2},
        //       new TrainingSpec{Id=12,Exercise=exercises[7],ExId=exercises[7].Id,HeadId=3,Set=1,Weight=12,Amount=15,Order=3},
        //       new TrainingSpec{Id=13,Exercise=exercises[4],ExId=exercises[4].Id,HeadId=3,Set=2,Weight=100,Amount=3,Order=4},
        //       new TrainingSpec{Id=14,Exercise=exercises[5],ExId=exercises[5].Id,HeadId=3,Set=2,Weight=100,Amount=12,Order=5},
        //       new TrainingSpec{Id=15,Exercise=exercises[7],ExId=exercises[7].Id,HeadId=3,Set=2,Weight=12,Amount=14,Order=6},

        //       //Vitaly3
        //       new TrainingSpec{Id=16,Exercise=exercises[6],ExId=exercises[6].Id,HeadId=4,Set=2,Weight=12,Amount=14,Order=8 }
        //   }.AsQueryable();
        //}
        //#endregion
    }
}
