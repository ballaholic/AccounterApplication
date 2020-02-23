namespace AccounterApplication.Web
{
    using Data;
    using Web.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AccounterDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandling(env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints();

            app.SeedData();
        }
    }
}
