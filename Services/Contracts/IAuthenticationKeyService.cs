using System.Threading.Tasks;
using CustomerAPI.Settings;

namespace CustomerAPI.Services
{
    public interface IAuthenticationKeyService
    {
        Task<AuthKey> AuthenticateAsync(string authKey);
    }
}
