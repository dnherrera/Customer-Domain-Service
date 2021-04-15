using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data
{
    /// <summary>
    /// Repository Db Context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/>
    public class RepositoryDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets Customers
        /// </summary>
        /// <value>
        /// The Customers.
        /// </value>
        public DbSet<CustomerModel> Customers { get; set; }

        /// <summary>
        /// Gets or sets Address
        /// </summary>
        /// <value>
        /// The Address.
        /// </value>
        public DbSet<AddressModel> Address { get; set; }
    }
}
