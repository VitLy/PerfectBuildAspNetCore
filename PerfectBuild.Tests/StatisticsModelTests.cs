using PerfectBuild.Models;
using PerfectBuild.Models.Report;
using PerfectBuild.Models.Report.ExerciseStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PerfectBuild.Tests
{
    public class StatisticsModelTests
    {
        [Fact]
        public void IsCorrectGroupExercisesByWeight()
        {
            int exerciseId = 3;

            List<Exercise> exercises = new List<Exercise>
            {
                new Exercise{Id=1,Name="Прыжки на месте"},
                new Exercise{Id=2,Name="Становая тяга"},
                new Exercise{Id=3,Name="Шраги"},
                new Exercise{Id=4,Name="Молоток"},
                new Exercise{Id=5,Name="Трицепс"}
            };

            DateTime date1 = new DateTime(2020, 01, 01);
            DateTime date2 = new DateTime(2020, 01, 07);

            //arrange
            List<UserSpec> specs = new List<UserSpec>
            {
                new UserSpec{Date=date1,ExerciseId=1,Set=1,Weight=10,Amount=1},
                new UserSpec{Date=date1,ExerciseId=2,Set=1,Weight=11,Amount=1},
                    new UserSpec{Date=date1,ExerciseId=3,Set=1,Weight=12,Amount=10},
                new UserSpec{Date=date1,ExerciseId=4,Set=1,Weight=13,Amount=1},
                new UserSpec{Date=date1,ExerciseId=5,Set=1,Weight=14,Amount=1},

                new UserSpec{Date=date1,ExerciseId=1,Set=2,Weight=10,Amount=1},
                new UserSpec{Date=date1,ExerciseId=2,Set=2,Weight=11,Amount=1},
                    new UserSpec{Date=date1,ExerciseId=3,Set=2,Weight=12,Amount=10},
                new UserSpec{Date=date1,ExerciseId=4,Set=2,Weight=13,Amount=1},
                new UserSpec{Date=date1,ExerciseId = 5,Set=2,Weight=14,Amount=1},

                new UserSpec{Date=date1,ExerciseId=1,Set=3,Weight=10,Amount=1},
                new UserSpec{Date=date1,ExerciseId=2,Set=3,Weight=11,Amount=1},
                    new UserSpec{Date=date1,ExerciseId=3,Set=3,Weight=12,Amount=10},
                new UserSpec{Date=date1,ExerciseId=4,Set=3,Weight=13,Amount=1},
                new UserSpec{Date=date1,ExerciseId=5,Set=3,Weight=14,Amount=1},

                new UserSpec{Date=date2,ExerciseId=1,Set=1,Weight=10,Amount=1},
                new UserSpec{Date=date2,ExerciseId=2,Set=1,Weight=11,Amount=1},
                    new UserSpec{Date=date2,ExerciseId=3,Set=1,Weight=15,Amount=8},
                new UserSpec{Date=date2,ExerciseId=4,Set=1,Weight=13,Amount=1},
                new UserSpec{Date=date2,ExerciseId=5,Set=1,Weight=14,Amount=1},

                 new UserSpec{Date=date2,ExerciseId=1,Set=2,Weight=10,Amount=1},
                new UserSpec{Date=date2,ExerciseId=2,Set=2,Weight=11,Amount=1},
                    new UserSpec{Date=date2,ExerciseId=3,Set=2,Weight=15,Amount=8},
                new UserSpec{Date=date2,ExerciseId=4,Set=2,Weight=13,Amount=1},
                new UserSpec{Date=date2,ExerciseId=5,Set=2,Weight=14,Amount=1},

                    new UserSpec{Date=date2,ExerciseId=3,Set=3,Weight=12,Amount=45},
            };

            //Act
            StatisticsModel sm = new StatisticsModel();

            UserGeneralData ingressData = new UserGeneralData { ExerciseId = exerciseId, userExercises = exercises, userSpecs = specs };
            var data = sm.GetExerciseData(ingressData);

            //expecting data
            List<Point<DateTime, float>> expectedData = new List<Point<DateTime, float>>()
            {
                new Point<DateTime, float>{X=date1,Y=360},
                new Point<DateTime, float>{X=date2,Y=780}
            };

            //        new UserSpec { Date = date1, ExerciseId = 3, Set = 1, Weight = 12, Amount = 10 },         
            //        new UserSpec { Date = date1, ExerciseId = 3, Set = 2, Weight = 12, Amount = 10 }, =360
            //        new UserSpec { Date = date1, ExerciseId = 3, Set = 3, Weight = 12, Amount = 10 },

            //        new UserSpec { Date = date2, ExerciseId = 3, Set = 1, Weight = 15, Amount = 8 }, 
            //        new UserSpec { Date = date2, ExerciseId = 3, Set = 2, Weight = 15, Amount = 8 }, = 780
            //        new UserSpec{  Date = date2, ExerciseId = 3, Set = 3 ,Weight = 12, Amount = 45},

            Assert.Empty(expectedData.Except(data["Average Weight"]));
        }
    }
}
