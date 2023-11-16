using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Name_Here.Cosmos;
using Name_Here.Models; 
using Name_Here.Repositories;
using Name_Here.Cosmos.ModelBuilding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Name_Here.MVC
{
    public class Program
    {
        static WebApplication app;
        public static async Task Main(string[] args)
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

                          googleOptions.Events.OnCreatingTicket = async ctx =>
                          {
                              await ddd(ctx);
                              
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


            builder.Services.AddScoped<IRepository, CosmosRepo>();

            builder.Services.AddScoped<AppDBContext>();

            //       builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AppDBContext>();
            //      builder.Services.AddDbContext<AppDBContext>();


            
            app = builder.Build();


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

          await  app.RunAsync();
        }

        static async Task<int> ddd(OAuthCreatingTicketContext ctx)
        {
            var scope = app.Services.CreateScope();

            var repo = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();
         
            ctx.Identity?.AddClaim(new Claim("Role", "99"));
             

            repo.Dispose();
            scope.Dispose();
            return await Task.FromResult(0);
        }
    }
}