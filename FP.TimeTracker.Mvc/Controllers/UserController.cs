using FP.TimeTracker.Dto.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class UserController : Controller
    {
        IHttpClientFactory _clientFactory;
        public UserController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Public");
        }
       
      
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();//(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            return RedirectToAction("Login", "Public");
        }
    }
}
