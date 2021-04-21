using System.Collections.Generic;
using CustomerAPI.Data;
using CustomerAPI.Models;
using Newtonsoft.Json;

namespace CustomerAPI.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerSeed
    {
        private readonly RepositoryDbContext _customerDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerDbContext"></param>
        public CustomerSeed(RepositoryDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SeedCustomers()
        {
            string customerData = System.IO.File.ReadAllText("Data/CustomerSeedData.json");
            var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(customerData);

            _customerDbContext.AddRange(customers);
            _customerDbContext.SaveChanges();
        }
    }
}
