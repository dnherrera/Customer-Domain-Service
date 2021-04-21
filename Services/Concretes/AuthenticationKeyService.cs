using System.Threading.Tasks;
using CustomerAPI.Settings;
using Microsoft.Extensions.Options;

namespace CustomerAPI.Services
{
    /// <summary>
    /// Authentication Key Service
    /// </summary>
    public class AuthenticationKeyService : IAuthenticationKeyService
    {
        private readonly AuthKeySetting _authKeySetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationKeyService"/>
        /// </summary>
        /// <param name="authKeySetting"></param>
        public AuthenticationKeyService(IOptions<AuthKeySetting> authKeySetting)
        {
            _authKeySetting = authKeySetting.Value;
        }

        /// <summary>
        /// Authenticate Key
        /// </summary>
        /// <param name="authKey">The auth key.</param>
        /// <returns></returns>
        public async Task<string> AuthenticateKeyAsync(string authKey)
        {
            var isValidKey = await Task.Run(() => _authKeySetting.AuthenticationKey == authKey);
            
            if (!isValidKey)
            {
                return null;
            }

            return _authKeySetting.AuthenticationKey;
        }
    }
}
