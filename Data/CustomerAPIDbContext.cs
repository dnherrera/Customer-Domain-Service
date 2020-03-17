using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomerAPI.Data
{
    public class CustomerAPIDbContext : DbContext
    {
        public CustomerAPIDbContext(DbContextOptions<CustomerAPIDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
