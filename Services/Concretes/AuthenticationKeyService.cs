using CustomerAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public class AuthenticationKeyService : IAuthenticationKeyService
    {
        private List<AuthKey> _key = new List<AuthKey>
        {
            new AuthKey { AuthenticationKey = "abcd1234" }
        };

        public async Task<AuthKey> AuthenticateAsync(string authKey)
        {
            AuthKey key = await Task.Run(() => _key.SingleOrDefault(x => x.AuthenticationKey == authKey));

            if (key == null)
                return null;

            return key;
        }
    }
}
