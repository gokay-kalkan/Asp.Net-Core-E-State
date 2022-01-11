using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using EState.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EState
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
        }
      
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddGoogle(opt => {

                opt.ClientId = "813149882281-o0c33q9m4dktn1ct8ukdum6tqmqjs0sf.apps.googleusercontent.com";
                opt.ClientSecret = "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";

            });
            services.AddDbContext<DataContext>();

           

           
            services.AddIdentity<UserAdmin, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnroöprsþtuüvyz0123456789-";
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
               
            });
            services.ConfigureApplicationCookie
                (
                opt => 
                {
                    opt.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                    opt.LoginPath = "/Account/Login/"; opt.AccessDeniedPath = "/Account/AccessDenied/"; opt.LogoutPath = "/Account/LogOut/"; opt.ExpireTimeSpan = TimeSpan.FromMinutes(6);
                    opt.Cookie = new CookieBuilder
                    {
                     
                        
                        HttpOnly = true,
                       SecurePolicy=CookieSecurePolicy.SameAsRequest,
                      

                    };
                });
            





            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<UserAdmin> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
        
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "advert",
                    //template: "{title}/{id}",
                    pattern: "{controller=Advert}/{action=Details}/{id?}/{AdvertTitle}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

              SeedIdentity.Seed3(userManager, roleManager);
        }
    }
}
