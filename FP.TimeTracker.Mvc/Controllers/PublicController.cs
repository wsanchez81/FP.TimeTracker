using FP.TimeTracker.Dto.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FP.TimeTracker.Mvc.Controllers
{
    public class PublicController : Controller
    {
        IHttpClientFactory _clientFactory;
        public PublicController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            return View("Login");
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {

                var client = _clientFactory.CreateClient("meta");

                var loginResult = await client.GetFromJsonAsync<LoginVM>(string.Format("user/login?usr={0}&pwd={1}", username, password));
                if (loginResult.IsAuthenticated)
                {
                   
                    var claims = new[] { new Claim(ClaimTypes.Name, username)};

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsIdentity.AddClaim(new Claim("Token", loginResult.Token));
                    
                    var principal = new ClaimsPrincipal();
                    
                    principal.AddIdentity(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "EmployeeTimeTracker");
                }

            }
            return View();
        }

    }
}
