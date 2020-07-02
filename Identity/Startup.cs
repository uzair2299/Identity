using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //The Startup Class contains 2 methods – ConfigureServices() and Configure().
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            
            services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(
                   Configuration.GetConnectionString("IdentityContextConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.SignIn.RequireConfirmedAccount = true;

                })
                .AddDefaultTokenProviders()
                .AddDefaultUI()
         .AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                
               // options.Password.RequireDigit = true; //Requires a number between 0 - 9 in the password. Default is true
                options.Password.RequiredLength = 8; //	The minimum length of the password is 6
               // options.Password.RequireNonAlphanumeric = false; //Requires a non-alphanumeric character in the password. Default is true
               // options.Password.RequireUppercase = true;//Requires an uppercase character in the password.Default is true
               // options.Password.RequireLowercase = false; //Requires a lowercase character in the password.Default is true
               //options.Password.RequiredUniqueChars = 1;//Requires the number of distinct characters in the password. 1


                //Sign In Options
                //options.SignIn.RequireConfirmedEmail = false;
                //options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline which is also know by the name middleware.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
        }
    }
}
