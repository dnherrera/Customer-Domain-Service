using System.Threading.Tasks;
using CustomerAPI.Settings;

namespace CustomerAPI.Services
{
    /// <summary>
    /// Authentication Key Service Interface
    /// </summary>
    public interface IAuthenticationKeyService
    {
        /// <summary>
        /// Authenticate Key Async
        /// </summary>
        /// <param name="authKey"></param>
        /// <returns>Auth Key</returns>
        Task<string> AuthenticateKeyAsync(string authKey);
    }
}
