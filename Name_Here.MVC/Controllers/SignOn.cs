using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using NuGet.Packaging;
using System.Security.Claims;

namespace Name_Here.MVC.Controllers
{
    public class SignOn : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            return new ChallengeResult(
              GoogleDefaults.AuthenticationScheme,
              new AuthenticationProperties
              {
                  RedirectUri = Url.Action(nameof(GoogleResponse))
              });
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("External");

            var claimsIdentity = new ClaimsIdentity("OKCB");

            claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier));
            claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.Email));

            await HttpContext.SignInAsync(
                "OKCB",
                new ClaimsPrincipal(claimsIdentity));

          //  return LocalRedirect(returnUrl);
          
            return Json(claimsIdentity);
        }

        //
        public async Task<IActionResult> LogOut()
        { 
            await HttpContext.SignOutAsync();
           

            //  return LocalRedirect(returnUrl);

            return Json("{Bye}");
        }
    }
}