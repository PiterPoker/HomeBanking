using HomeBanking.API.Infrastructure.Auth;
using HomeBanking.API.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HomeBanking.API
{
    public class ByPassAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private string _currentUserId;
        private readonly AuthorizationParameters _authorization;
        private readonly IIdentityService _identityService;
        public ByPassAuthMiddleware(RequestDelegate next,
            IOptionsSnapshot<AuthorizationParameters> options,
            IIdentityService identityService)
        {
            _next = next;
            _currentUserId = null;
            _authorization = options.Value;
            _identityService = identityService;
        }


        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path == "/noauth")
            {
                var userid = context.Request.Query["userid"];
                if (!string.IsNullOrEmpty(userid))
                {
                    _currentUserId = userid;
                }
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/string";
                await context.Response.WriteAsync($"User set to {_currentUserId}");
            }

            else if (path == "/noauth/reset")
            {
                _currentUserId = null;
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/string";
                await context.Response.WriteAsync($"User set to none. Token required for protected endpoints.");
            }
            else
            {
                var currentUserId = _currentUserId;
                var authHeader = context.Request.Headers["Authorization"];
                if (authHeader != StringValues.Empty)
                {

                    var header = authHeader.FirstOrDefault();
                    if (!string.IsNullOrEmpty(header) && header.StartsWith("Login ") && header.Length > "Login ".Length)
                    {
                        currentUserId = header.Substring("Login ".Length);
                    }
                }


                if (!string.IsNullOrEmpty(currentUserId))
                {
                    var user = new ClaimsIdentity(new[] {
                    new Claim("emails", currentUserId),
                    new Claim("name", "Test user"),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "Test user"),
                    new Claim("nonce", Guid.NewGuid().ToString()),
                    new Claim("http://schemas.microsoft.com/identity/claims/identityprovider", "ByPassAuthMiddleware"),
                    new Claim("sub", currentUserId),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname","User"),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname","Microsoft")}
                    , "ByPassAuth");

                    context.User = new ClaimsPrincipal(user);
                }

                await _next.Invoke(context);
            }
        }
    }
}
