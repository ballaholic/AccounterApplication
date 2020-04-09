namespace AccounterApplication.Web
{
    using System.Reflection;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Data;
    using Data.Models;
    using Data.Common;
    using Data.Repositories;
    using Data.Common.Repositories;

    using Infrastructure;

    using Services.Mapping;
    using Services.Contracts;
    using Services.Implementations;

    using Web.ViewModels;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add localization services (StringLocalizer, HtmlLocalizer, etc.)
            services.AddLocalization(options => options.ResourcesPath = "LocalizationResources");

            // Data Layer
            services
                .AddDbContext<AccounterDbContext>(options => options
                    .UseSqlServer(Configuration.GetDefaultConnectionString()));

            // Authentication Layer
            services
                .AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<AccounterDbContext>();

            // Cookies configuration
            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            // Views
            services.AddMvcWithFilterAndLocalization();

            services.AddSingleton(this.Configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableUserEntityRepository<>), typeof(EfDeletableUserEntityRepository<>));
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application Services
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IExpenseGroupService, ExpenseGroupService>();
            services.AddTransient<IMonthlyIncomeService, MonthlyIncomeService>();
            services.AddTransient<IComponentsService, ComponentsService>();
            services.AddTransient<ICurrenciesService, CurrenciesService>();
            services.AddTransient<IComponentTypesService, ComponentTypesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            app.SeedDatabase(env);

            app.UseLocalization();

            app.UseExceptionHandling(env);

            app.UseStaticFiles();

            app.UseHttpsRedirection();
      
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints();            
        }
    }
}
