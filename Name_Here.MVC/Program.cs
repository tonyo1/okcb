using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace Name_Here.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Users", policy =>
                                  policy.RequireClaim("Role", "1", "2", "3", "4"));
            });

            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)    
                .AddCookie(options =>
                      {
                          options.AccessDeniedPath = new PathString("/SignOn");
                          options.LoginPath = new PathString("/SignOn");
                          options.LogoutPath = new PathString("/Home/SignOut");
                      })
                 .AddGoogle(googleOptions =>
                      {
                          googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                          googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                          googleOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                      });
               


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}