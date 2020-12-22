using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestingMVC.Models;
using TestingMVC.Models.Data;
using TestingMVC.Security;

namespace TestingMVC
{
    public class Startup
    {
        private object controller;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            //.AddDataAnnotationsLocalization();
            //Elave etdiklerim
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(@"Server=DESKTOP-HKBHGD3;Database=AppMvcDB;Trusted_Connection=True;MultipleActiveResultSets=True;"));

            //IdentityDbContext Configure 
            services.AddIdentity<ApplicationUser, IdentityRole>(
                    options =>
                    {
                        //Configure Password with my parameters
                        options.Password.RequiredLength = 8;
                        options.Password.RequiredUniqueChars = 3;
                        options.Password.RequireNonAlphanumeric = false;
                        options.SignIn.RequireConfirmedAccount = true;
                    })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                //Claims policy
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));
                options.AddPolicy("EditRolePolicy", policy =>
                    policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                //options.InvokeHandlersAfterFailure = false;
            });

            services.AddAuthentication()
                .AddGoogle(options =>
            {
                options.ClientId = "747185369603-obqhcnbuvg8g3dl8cq4eh5rqpj935t6u.apps.googleusercontent.com";
                options.ClientSecret = "hbpgPNK1DKOyX6ywvbGBWtYu";
            })
                .AddFacebook(options => 
                {
                    options.ClientId = "363618808207033";
                    options.ClientSecret = "8776644efc260422c28b51cac98114cf";
                });




            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context =>
            //        context.User.IsInRole("Admin") &&
            //        context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
            //        context.User.IsInRole("Super Admin")
            //    ));
            //});


            //Depentesy enjection
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

            services.ConfigureApplicationCookie(options =>
            {
                //Configure AccessDenied Path
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddApplicationInsightsTelemetry();


        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {

                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
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
        }

    }
}
