using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FP.TimeTracker.WebApi.TokenAuthentication
{
    public interface ITokenManager
    {
        public bool Authenticate(string username, string password);
        public string NewToken();
        public ClaimsPrincipal VerfyToken(string token);
    }
}
