using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CustomerAPI.Filters
{
    /// <summary>
    /// Authentication Ticket Generator
    /// </summary>
    public class AuthenticationTicketGenerator
    {
        /// <summary>
        /// Create Ticket
        /// </summary>
        /// <param name="authKey"></param>
        /// <param name="schemeName"></param>
        /// <returns></returns>
        public static AuthenticationTicket Create(string authKey, string schemeName)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, authKey),
                new Claim(ClaimTypes.Name, authKey),
            };
            var identity = new ClaimsIdentity(claims, schemeName);
            var principal = new ClaimsPrincipal(identity);
            var authenticationTicket = new AuthenticationTicket(principal, schemeName);

            return authenticationTicket;
        }
    }
}
