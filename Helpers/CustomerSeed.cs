using System.Collections.Generic;
using CustomerAPI.Data;
using CustomerAPI.Models;
using Newtonsoft.Json;

namespace CustomerAPI.Helpers
{
    public class CustomerSeed
    {
        private readonly CustomerAPIDbContext _customerDbContext;

        public CustomerSeed(CustomerAPIDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public void SeedCustomers()
        {
            string customerData = System.IO.File.ReadAllText("Data/CustomerSeedData.json");
            var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(customerData);

            _customerDbContext.AddRange(customers);
            _customerDbContext.SaveChanges();
        }
    }
}
