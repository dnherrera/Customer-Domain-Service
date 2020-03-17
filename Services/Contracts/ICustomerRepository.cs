using CustomerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersListAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<bool> SaveAll();
        Task<Customer> DeleteCustomerAsync(int customerId);
    }
}
