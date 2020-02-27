namespace AccounterApplication.Web
{
    using System.Reflection;
    
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Http;

    using Data;
    using Data.Models;
    using Infrastructure;
    using Services.Mapping;
    using Services.Contracts;
    using Services.Implementations;
    using Controllers.Models;
    using Data.Repositories;
    using Data.Common.Repositories;
    using Microsoft.AspNetCore.Identity;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Data Layer
            services
                .AddDbContext<AccounterDbContext>(options => options
                    .UseSqlServer(Configuration.GetDefaultConnectionString()));

            // Authentication Layer
            services
                .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AccounterDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            // Views
            services
                .AddMvcWithFilter();

            services.AddSingleton(this.Configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application Services
            services.AddTransient<IExpenseService, ExpenseService>();           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            app.UseExceptionHandling(env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints();

            app.MigrateDatabase();
        }
    }
}
