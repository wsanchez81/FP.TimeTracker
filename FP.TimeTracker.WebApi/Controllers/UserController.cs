using FP.TimeTracker.Dto.Response;
using FP.TimeTracker.WebApi.TokenAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.TimeTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;

        public UserController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [HttpGet]
        [Route("Login")]
        public Task< LoginVM> Login(string usr, string pwd)
        {
            var result = new LoginVM { IsAuthenticated  = false};

            result.IsAuthenticated = _tokenManager.Authenticate(usr, pwd);
            if (result.IsAuthenticated)
            {
                result.Token = _tokenManager.NewToken();
            }
            return Task.FromResult(result);
        }
       
    }
}
