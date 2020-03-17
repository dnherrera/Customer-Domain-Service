using CustomerAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Data
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
            var customers = JsonConvert.DeserializeObject<List<Customer>>(customerData);

            _customerDbContext.AddRange(customers);
            _customerDbContext.SaveChanges();
        }
    }
}
