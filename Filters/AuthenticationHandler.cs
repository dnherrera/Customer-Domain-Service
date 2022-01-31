using System;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CustomerAPI.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomerAPI.Filters
{
    /// <summary>
    /// Authentication Handler
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authentication.AuthenticationHandler{AuthenticationSchemeOptions}"/>
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthenticationKeyService _authentication;

        /// <summary>
        /// Initializes a new instance of <see cref="AuthenticationHandler"/>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        /// <param name="authentication"></param>
        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticationKeyService authentication) : base(options, logger, encoder, clock)
        {
           _authentication = authentication;
        }

        /// <summary>
        /// HandleAuthenticateAsync
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header.");
            }

            try
            {
                // Get the Auth Header and Validate
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                string authKey = await _authentication.AuthenticateKeyAsync(authHeader.Parameter);

                // Validate Auth Key
                if (authKey is null)
                {
                    return AuthenticateResult.Fail("Invalid Authentication Key.");
                }

                // Generate Auth Ticket
                var authenticationTicket = AuthenticationTicketGenerator.Create(authKey, Scheme.Name);

                return AuthenticateResult.Success(authenticationTicket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Invalid Authorization Header {ex}");
            }
        }
    }
}
