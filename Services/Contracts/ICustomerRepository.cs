using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Services.Contracts
{
    /// <summary>
    /// Customer Repo Interface
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get Customer List Async
        /// </summary>
        /// <returns></returns>
        Task<List<CustomerModel>> GetCustomersListAsync();

        /// <summary>
        /// Get Customer by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomerModel> GetCustomerByIdentifierAsync(int id);

        /// <summary>
        /// Get Customer by name Async
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        Task<CustomerModel> GetCustomerByName(string name, DateTime? dateOfBirth);

        /// <summary>
        /// Create Customer Async
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<CustomerModel> CreateCustomerAsync(CustomerModel customer);

        /// <summary>
        /// Update Customer Async
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<CustomerModel> UpdateCustomerAsync(CustomerModel customer);

        /// <summary>
        /// Delete Customer By Id Async
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<int> DeleteCustomerByIdentifierAsync(int customerId);
    }
}
