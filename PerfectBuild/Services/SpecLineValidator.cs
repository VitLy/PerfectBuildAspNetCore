using Microsoft.Extensions.Localization;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Services
{
    public class SpecLineValidator
    {
        IStringLocalizer<SharedErrorMessages> msgLocalizer;

        public SpecLineValidator(IStringLocalizer<SharedErrorMessages> msgLocalizer)
        {
            this.msgLocalizer = msgLocalizer;
        }

        public bool IsSpecLineHasCorrectWeight(int exerciseId, float weight, IList<Exercise> exercises, out string textErrorShortMessage, out string textErrorLongMessage)
        {
            if (exercises == null)
            {
                throw new ArgumentNullException(nameof(exercises));
            }
            if (exerciseId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(exerciseId));
            }

            var exercise = exercises.Where(x => x.Id.Equals(exerciseId)).FirstOrDefault();

            if (exercise == null)
            {
                throw new ArgumentException(nameof(exerciseId) + "not found in DB ", nameof(exerciseId));
            }

            if (weight <= 0)
            {
                textErrorShortMessage = msgLocalizer["WeigtLessThenZeroShort"];
                textErrorLongMessage = msgLocalizer["WeigtLessThenZeroLong"];
                return false;
            }
            if (exercise.OwnWeight == true & weight != 1)
            {
                textErrorShortMessage = msgLocalizer["IncorrectWeightShort"];
                textErrorLongMessage = msgLocalizer["IncorrectWeightLong"];
                return false;
            }
            textErrorShortMessage =textErrorLongMessage= "ok";
            return true;
        }
    }
}
