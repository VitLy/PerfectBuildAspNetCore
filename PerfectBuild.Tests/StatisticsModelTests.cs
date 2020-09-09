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
            #region Arrange
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

            List<TrainingHead> heads = new List<TrainingHead>
            {
                new TrainingHead{Id=1,Date=date1},
                new TrainingHead{Id=2,Date=date2}
            };

            List<TrainingSpec> specs = new List<TrainingSpec>
            {
                new TrainingSpec{HeadId=1,ExId=1,Set=1,Weight=10,Amount=1},
                new TrainingSpec{HeadId=1,ExId=2,Set=1,Weight=11,Amount=1},
                    new TrainingSpec{HeadId=1,ExId=3,Set=1,Weight=12,Amount=10},
                new TrainingSpec{HeadId=1,ExId=4,Set=1,Weight=13,Amount=1},
                new TrainingSpec{HeadId=1,ExId=5,Set=1,Weight=14,Amount=1},

                new TrainingSpec{HeadId=1,ExId=1,Set=2,Weight=10,Amount=1},
                new TrainingSpec{HeadId=1,ExId=2,Set=2,Weight=11,Amount=1},
                    new TrainingSpec{HeadId=1,ExId=3,Set=2,Weight=12,Amount=10},
                new TrainingSpec{HeadId=1,ExId=4,Set=2,Weight=13,Amount=1},
                new TrainingSpec{HeadId=1,ExId = 5,Set=2,Weight=14,Amount=1},

                new TrainingSpec{HeadId=1,ExId=1,Set=3,Weight=10,Amount=1},
                new TrainingSpec{HeadId=1,ExId=2,Set=3,Weight=11,Amount=1},
                    new TrainingSpec{HeadId=1,ExId=3,Set=3,Weight=12,Amount=10},
                new TrainingSpec{HeadId=1,ExId=4,Set=3,Weight=13,Amount=1},
                new TrainingSpec{HeadId=1,ExId=5,Set=3,Weight=14,Amount=1},

                new TrainingSpec{HeadId=2,ExId=1,Set=1,Weight=10,Amount=1},
                new TrainingSpec{HeadId=2,ExId=2,Set=1,Weight=11,Amount=1},
                    new TrainingSpec{HeadId=2,ExId=3,Set=1,Weight=15,Amount=8},
                new TrainingSpec{HeadId=2,ExId=4,Set=1,Weight=13,Amount=1},
                new TrainingSpec{HeadId=2,ExId=5,Set=1,Weight=14,Amount=1},

                 new TrainingSpec{HeadId=2,ExId=1,Set=2,Weight=10,Amount=1},
                new TrainingSpec{HeadId=2,ExId=2,Set=2,Weight=11,Amount=1},
                    new TrainingSpec{HeadId=2,ExId=3,Set=2,Weight=15,Amount=8},
                new TrainingSpec{HeadId=2,ExId=4,Set=2,Weight=13,Amount=1},
                new TrainingSpec{HeadId=2,ExId=5,Set=2,Weight=14,Amount=1},

                    new TrainingSpec{HeadId=2,ExId=3,Set=3,Weight=12,Amount=45},
            };

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
            #endregion

            //Act
            StatisticsModel sm = new StatisticsModel();

            UserGeneralData ingressData = new UserGeneralData { ExerciseId = exerciseId, UserExercises = exercises, UserSpecs = specs, UserHead = heads };
            var result = sm.GetExerciseData(ingressData);

            //Assert
            Assert.Empty(expectedData.Except(result["Average Weight"]));
        }

        [Fact]
        public void IsCorrectGetRecordExercises()
        {
            #region Arrange

            List<Exercise> exercises = new List<Exercise>
            {
                new Exercise{Id=1,Name="Прыжки на месте"},
                new Exercise{Id=2,Name="Становая тяга"},
                new Exercise{Id=3,Name="Шраги"},

            };

            DateTime date1 = new DateTime(2020, 01, 01);
            DateTime date2 = new DateTime(2020, 01, 07);
            DateTime date3 = new DateTime(2020, 01, 08);


            List<TrainingHead> heads = new List<TrainingHead>
            {
                new TrainingHead {Id=1,Date=date1},
                new TrainingHead {Id=2,Date=date1},
                new TrainingHead {Id=3,Date=date2}
            };

            List<TrainingSpec> specs = new List<TrainingSpec>
            {
            #region date1="2020/01/01 - HeadId=1"
		        new TrainingSpec{HeadId=1,ExId=1,Set=1,Weight=10,Amount=15},
                new TrainingSpec{HeadId=1,ExId=2,Set=1,Weight=11,Amount=15},
                new TrainingSpec{HeadId=1,ExId=3,Set=1,Weight=12,Amount=10},

                new TrainingSpec{HeadId=1,ExId=1,Set=2,Weight=10,Amount=15},
                new TrainingSpec{HeadId=1,ExId=2,Set=2,Weight=11,Amount=15},
                new TrainingSpec{HeadId=1,ExId=3,Set=2,Weight=12,Amount=10},

                new TrainingSpec{HeadId=1,ExId=1,Set=3,Weight=10,Amount=15},
                new TrainingSpec{HeadId=1,ExId=2,Set=3,Weight=11,Amount=15},
                new TrainingSpec{HeadId=1,ExId=3,Set=3,Weight=12,Amount=10},
                //
                //  Id=1:Total=450
                //  Id=2:Total=495
                //  Id=3:Total=360
                //
            #endregion
            #region date1="2020/01/01" - HeadId=2"
                new TrainingSpec{HeadId=2,ExId=1,Set=1,Weight=10,Amount=1},
                new TrainingSpec{HeadId=2,ExId=2,Set=1,Weight=10,Amount=1},
                new TrainingSpec{HeadId=2,ExId=3,Set=1,Weight=12,Amount=15},

                new TrainingSpec{HeadId=2,ExId=1,Set=2,Weight=10,Amount=1},
                new TrainingSpec{HeadId=2,ExId=2,Set=2,Weight=11,Amount=1},
                new TrainingSpec{HeadId=2,ExId=3,Set=2,Weight=12,Amount=15},

                new TrainingSpec{HeadId=2,ExId=1,Set=3,Weight=10,Amount=1},
                new TrainingSpec{HeadId=2,ExId=2,Set=3,Weight=11,Amount=1},
                new TrainingSpec{HeadId=2,ExId=3,Set=3,Weight=12,Amount=15},
                //
                //  Id=1:Total=30
                //  Id=2:Total=33
                //  Id=3:Total=540
                //
            #endregion
            #region date2="2020/01/07 - HeadId=3"
		        new TrainingSpec{HeadId=3,ExId=1,Set=1,Weight=10,Amount=1},
                new TrainingSpec{HeadId=3,ExId=2,Set=1,Weight=15,Amount=15},
                new TrainingSpec{HeadId=3,ExId=3,Set=1,Weight=15,Amount=8},

                new TrainingSpec{HeadId=3,ExId=2,Set=2,Weight=15,Amount=15},
                new TrainingSpec{HeadId=3,ExId=2,Set=3,Weight=15,Amount=15}};
            //
            //  Id=1:Total=10
            //  Id=2:Total=675
            //  Id=3:Total=40
            //
            #endregion
            var userData = new UserGeneralData { DateFrom = date1, DateTo = date2, UserExercises = exercises, UserSpecs = specs,UserHead=heads };
            #endregion

            //Act
            StatisticsModel statisticsModel = new StatisticsModel();
            var result = statisticsModel.GetExerciseRecords(userData);

            #region Assert
            var expectetData = new List<Point<int, float>>
            {
                //expecting data
                //Id=1 Total=450 Date=2020/01/01 HeadId=1
                //Id=2 Total=675 Date=2020/01/07 HeadId=3
                //Id=3 Total=540 Date=2020/01/01 HeadId=2 

                new Point<int, float>{X=1,Y=450,Label=date1.ToString("d MMM yyyy")},
                new Point<int, float>{X=2,Y=675,Label=date2.ToString("d MMM yyyy")},
                new Point<int, float>{X=3,Y=540,Label=date1.ToString("d MMM yyyy")}
            };

            //Assert
            Assert.Empty(expectetData.Except(result));
            #endregion
        }
    }
}
