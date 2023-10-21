using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using Name_Here.MVC.Models;

using System.Diagnostics;
using System.Security.Claims;

namespace Name_Here.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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

        [ClaimRequirementAttribute("one", "two")]
        public IActionResult JsonView()
        {
            var tmp = Repository.Users.ToList();
            var tmp1 = tmp.Serialize();
            ViewData["txt"] = tmp1;
            return View();
        }

        public IActionResult ForceAuthenticate()
        {
            var identity = (ClaimsIdentity)Request.HttpContext.User.Identity;
            identity.AddClaim(new Claim("one", "two"));

            return View("Index");
        }
    }
} 