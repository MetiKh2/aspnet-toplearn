using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;

namespace TopLearn.Site
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRazorPages();
            #region Ioc
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IPermessionService, PermessionService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IFroumService,FroumService>();
            #endregion
            #region DataBase Context
            services.AddDbContext<TopLearnContext>(option => option.UseSqlServer(Configuration.GetConnectionString("TopLearnConnection")));
            #endregion
            #region Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

            });
            #endregion

            #region RunTimeCompilaion
            var mvcBuilder = services.AddControllersWithViews();
#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) => {
                if (context.Request.Path.Value.ToString().ToLower().StartsWith("/buyepisodes"))
                {
                    var callingUrl = context.Request.Headers["Referer"].ToString();

                    if (callingUrl!=""&&(callingUrl.StartsWith("https://localhost:44393") || callingUrl.StartsWith("http://localhost:44393/")))
                    {
                        await next.Invoke();
                    }
                    else
                    {
                        context.Response.Redirect("/Login");
                    }
                }
                else
                {
                    await next.Invoke();
                }
            }
          
            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(

                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
       );
                endpoints.MapRazorPages();
            });
        }
    }
}
