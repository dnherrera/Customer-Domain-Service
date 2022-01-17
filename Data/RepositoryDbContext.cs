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
        public virtual DbSet<CustomerModel> Customers { get; set; }

        /// <summary>
        /// Gets or sets Address
        /// </summary>
        /// <value>
        /// The Address.
        /// </value>
        public virtual DbSet<AddressModel> Addresses { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressModel>(entity =>
            {
                entity.HasKey(nameof(AddressModel.CustomerId));
                entity.HasOne(am => am.Customer).WithMany(u => u.Addresses).HasForeignKey(am => am.CustomerId);
            });
        }
    }
}
