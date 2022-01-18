using CustomerAPI.Data;
using CustomerAPI.Models;
using CustomerAPI.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services.Concretes
{
    /// <summary>
    /// Customer Repository
    /// </summary>
    public class CustomerRepository : BaseRepository<CustomerModel>, ICustomerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="repositoryDbContext"></param>
        /// <param name="logger"></param>
        public CustomerRepository(RepositoryDbContext repositoryDbContext, ILogger<ICustomerRepository> logger) : base(logger, repositoryDbContext)
        {
        }

        /// <summary>
        /// Create Customer Async
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<CustomerModel> CreateCustomerAsync(CustomerModel customer)
        {
            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();
            return customer;
        }

        /// <summary>
        /// Delete Customer Async
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<int> DeleteCustomerByIdentifierAsync(int customerId)
        {
            var customer = await DbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            DbContext.Customers.Remove(customer);
            await DbContext.SaveChangesAsync();
            return customerId;
        }

        /// <summary>
        /// Get Customer by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public async Task<CustomerModel> GetCustomerByName(string name, DateTime? dateOfBirth)
        {
            return await DbContext.Customers.Where(c => c.Name == name && c.DateOfBirth == dateOfBirth).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Customer By Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerModel> GetCustomerByIdentifierAsync(int id)
        {
            return await DbContext.Customers.Include(a => a.Addresses).Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Customer List Collection
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomerModel>> GetCustomersListAsync()
        {
            return await DbContext.Customers.Include(a => a.Addresses).ToListAsync();
        }
        
        /// <summary>
        /// Update Customer List Collection
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<CustomerModel> UpdateCustomerAsync(CustomerModel customer)
        {
            DbContext.Update(customer);
            await DbContext.SaveChangesAsync();
            return customer;
        }
    }
}
