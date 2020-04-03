namespace AccounterApplication.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.DependencyInjection;

    using ModelBinders;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcWithFilterAndLocalization(this IServiceCollection services)
        {
            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                })
                .AddDataAnnotationsLocalization()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services
                .AddRazorPages();

            return services;
        }
    }
}
