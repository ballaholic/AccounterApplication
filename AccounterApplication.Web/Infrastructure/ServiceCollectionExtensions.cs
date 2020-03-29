namespace AccounterApplication.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcWithFilter(this IServiceCollection services)
        {
            services.AddControllersWithViews(options => options
                                                            .Filters
                                                                .Add(new AutoValidateAntiforgeryTokenAttribute()))
                        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                        .AddDataAnnotationsLocalization();

            services.AddRazorPages();

            return services;
        }
    }
}
