using APlataCore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APlataCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddRouting();
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("defaultApi", "api/{controller}/{action}/{id?}");
            //});

           
            #region MyRegion

            //var routeBuilder = new RouteBuilder(app);

            //routeBuilder.MapRoute(name: "default", template: "{controller}/{action}/{id?}", new { controller = "Home", action = "Index" });
            //routeBuilder.MapRoute(name: "defaultApi", template: "api/{controller}/{action}/{id?}");
            //routeBuilder.MapRoute(name: "searchRouteApi", template: "api/{controller}/{action}", 
            //    defaults: new { }, 
            //    constraints: new { action = new AlphaRouteConstraint() });

            //routeBuilder.MapRoute("api/{controller}/{action}/{id?}/{*catchall}",
            //    async context =>
            //    {
            //        await context.Response.WriteAsync("api/{controller}/{action}/{id?}/{*catchall} used");
            //    });

            //app.UseRouter(routeBuilder.Build());
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Default page.");
            //});

            #endregion
        }
    }
}
