using CustomerAPI.Data;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public interface IAuthenticationKeyService
    {
        Task<AuthKey> AuthenticateAsync(string authKey);
    }
}
