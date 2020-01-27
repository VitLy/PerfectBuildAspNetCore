using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PerfectBuild.Infrastructure
{
    public class FloatModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            ILoggerFactory loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
            IModelBinder binder = new FloatModelBinder(new SimpleTypeModelBinder(typeof(float), loggerFactory));
            return context.Metadata.ModelType == typeof(float) ? binder : null;
        }
    }
}
