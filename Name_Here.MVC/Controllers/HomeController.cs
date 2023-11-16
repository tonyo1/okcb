using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

using Name_Here.Cosmos.ModelBuilding;
using Name_Here.MVC.Models;
using Name_Here.Repositories;

using System;
using System.Diagnostics;
using System.Security.Claims;

namespace Name_Here.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly AppDBContext _repo;
        readonly IConfiguration config;

        public HomeController(ILogger<HomeController> logger, AppDBContext repo, IConfiguration config)
        {
            this.config = config;
            this._repo = repo;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var claimsIdentity = (ClaimsIdentity)HttpContext.User.Identity;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // todo secure this
        public IActionResult ShowUsers()
        {
            return View(Repository.Users.ToList());
        }

        [Authorize]
        public IActionResult JsonView()
        {
            var tmp = Repository.Users.ToList();
            var tmp1 = tmp.Serialize();
            ViewData["txt"] = tmp1;
            return View();
        }

        [Authorize]
        public async Task< IActionResult> CosmoData()
        {
            var str = HttpContext.User.Claims;
             
            var tmp = _repo.AppUsers.ToList();
            var tmp1 = tmp.Serialize();
            ViewData["txt"] = tmp1;

            string? s = await  HttpContext.GetTokenAsync("AppUser");

            return View("JsonView");
        }
    }
}