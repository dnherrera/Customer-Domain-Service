using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CustomerAPI.Services;
using CustomerAPI.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomerAPI.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthenticationKeyService _authentication;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticationKeyService authentication) 
            : base(options, logger, encoder, clock)
        {
           _authentication = authentication;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            AuthKey auth = null;
            try
            {
                AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                auth = await _authentication.AuthenticateAsync(authHeader.Parameter);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if(auth == null)
                return AuthenticateResult.Fail("Invalid authentication key");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, auth.AuthenticationKey.ToString()),
                new Claim(ClaimTypes.Name, auth.AuthenticationKey),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
