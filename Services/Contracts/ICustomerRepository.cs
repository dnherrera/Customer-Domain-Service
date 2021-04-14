using CustomerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerModel>> GetCustomersListAsync();
        Task<CustomerModel> GetCustomerAsync(int id);
        Task<CustomerModel> CreateCustomerAsync(CustomerModel customer);
        Task<bool> SaveAll();
        Task<CustomerModel> DeleteCustomerAsync(int customerId);
    }
}
