using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Name_Here.Models;
using Name_Here.Repositories;

using System.Security.Claims;
using Cosmos.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNet.Identity;
using Cosmos.ModelBuilding;
using Microsoft.AspNetCore.Authentication;

namespace Name_Here.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services
                .AddAuthentication(options =>
                        {
                            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                        })
                .AddCookie()
                .AddGoogle(async googleOptions =>
                      {
                          googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                          googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                          googleOptions.SaveTokens = true;
                          googleOptions.CallbackPath = "/google-response";

                          googleOptions.Events.OnCreatingTicket = ctx =>
                          {
                              List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();
                              
                              tokens.Add(new AuthenticationToken()
                              {
                                  Name = "AppUser",
                                  Value = new AppUser().Serialize()
                              });

                              ctx.Properties.StoreTokens(tokens);

                              return Task.CompletedTask;
                          };
                      })
;
            // gets from secrets
            builder.Services.AddSingleton<IConfiguration>(new MyConfig()
            {
                GoogleClientId = builder.Configuration["Authentication:Google:ClientId"],
                GoogleSecret = builder.Configuration["Authentication:Google:ClientSecret"],
                AzureEndpoint = builder.Configuration["Authentication:Cosmos:accountEndpoint"],
                AzureSecret = builder.Configuration["Authentication:Cosmos:accountKey"],
                AzureDBName = builder.Configuration["Authentication:Cosmos:databaseName"]
            });
            builder.Services.AddScoped<AppDBContext>();
            

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