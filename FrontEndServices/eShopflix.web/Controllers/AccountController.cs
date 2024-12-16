using eShopflix.web.HttpClients;
using eShopflix.web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;

namespace eShopflix.web.Controllers
{
    public class AccountController : Controller
    {    
        AuthServiceClient _authServiceClient;
        public AccountController(AuthServiceClient authServiceClient)
        {
                _authServiceClient = authServiceClient;
        }
        public IActionResult Login()
        {
            return View();
        }

        private void GenerateToken(UserModel model)
        {
            var stringData= JsonSerializer.Serialize(model);
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.UserData,stringData),
                new Claim(ClaimTypes.Role, string.Join(",", model.Roles))

            };

            var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal= new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        }
       

        [HttpPost]
        public IActionResult  Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _authServiceClient.Login(model).Result;
                if (user != null)
                {
                    GenerateToken(user);
                    if (user != null && user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    }
                }
                

            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
