using CustomerAPI.Data;
using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerAPIDbContext _customerDbContext;

        public CustomerRepository(CustomerAPIDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomerAsync(int customerId)
        {
            Customer customer = await _customerDbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
             _customerDbContext.Customers.Remove(customer);
            await _customerDbContext.SaveChangesAsync();
            return null;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _customerDbContext.Customers
                                .Include(a => a.Address)
                                .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersListAsync()
        {
            return await _customerDbContext.Customers
                                .Include(a => a.Address).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _customerDbContext.SaveChangesAsync() > 0;
        }
    }
}
