using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PerfectBuild.Infrastructure
{
    public class FloatModelBinder : IModelBinder
    {
        private IModelBinder fallbackBinder;

        public FloatModelBinder(IModelBinder fallBackBinder)
        {
            this.fallbackBinder = fallBackBinder;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var floatValueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (floatValueResult == ValueProviderResult.None)
                return fallbackBinder.BindModelAsync(bindingContext);

            float parsedVal = 0;
            try
            {
                parsedVal = float.Parse(floatValueResult.FirstValue, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

            }
            bindingContext.Result = ModelBindingResult.Success(parsedVal);
            return Task.CompletedTask;
        }
    }
}
