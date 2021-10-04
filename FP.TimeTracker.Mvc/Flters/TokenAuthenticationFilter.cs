using FP.TimeTracker.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FP.TimeTracker.Mvc.Flters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        IHttpClientFactory _clientFactory;
        public TokenAuthenticationFilter(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = true;
            
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;

            string auth = context.HttpContext.Request.Headers["Authorization"];//context.HttpContext.Request.Headers.Add("Authorization", "Bearer " + token);
            string token = auth.Replace("Bearer ", "");
            if (result)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                try
                {
                    var client = _clientFactory.CreateClient("meta");

                    var loginResult = client.GetFromJsonAsync<LoginVM>(string.Format("user/verifytoken?token={0}", token));
                    if (!loginResult.Result.IsAuthenticated)
                    {
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    context.ModelState.AddModelError("Unauthorized", ex.ToString() );
                }
                
            }
            if (!result)
            {
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}
