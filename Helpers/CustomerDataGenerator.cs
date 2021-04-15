using System;
using System.Collections.Generic;
using System.Linq;
using CustomerAPI.Data;
using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerAPI.Helpers
{
    public class CustomerDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (RepositoryDbContext context = new RepositoryDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<RepositoryDbContext>>()))
            {
                // Look for any customer list
                if (context.Customers.Any())
                {
                    return;   // Data was already seeded
                }

                List<AddressModel> addressList = new List<AddressModel>()
                {
                    new AddressModel() { AddressLine1 = "201 Malagasang 1-D", AddressLine2 = "209 Bucandala 5", City = "Imus City", State = "Cavite", CustomerId = 1},
                    new AddressModel() { AddressLine1 = "155 Bayan Luma", AddressLine2 = "199 Aust 5", City = "Imus City", State = "Cavite", CustomerId = 1},
                    new AddressModel() { AddressLine1 = "198 Guadalupe Nuevo", AddressLine2 = "8797 Northern Lights", City = "Makati City", State = "Metro Manila", CustomerId = 2},
                    new AddressModel() { AddressLine1 = "6789 HV Dela Costa", AddressLine2 = "8694 Camella", City = "Pasay City", State = "Metro Manila", CustomerId = 2},
                    new AddressModel() { AddressLine1 = "4623 Bacoor", AddressLine2 = "Sabang St.", City = "Quezon City", State = "Metro Manila", CustomerId = 3},
                    new AddressModel() { AddressLine1 = "903 Bel-Air", AddressLine2 = "Dasmarinas St.", City = "Las Pinas City", State = "Metro Manila", CustomerId = 4},
                    new AddressModel() { AddressLine1 = "8323 Perea", AddressLine2 = "Subah", City = "Cubao City", State = "Metro Manila", CustomerId = 5}
                };

                ICollection<AddressModel> addressColletion = addressList;

                context.Customers.AddRange
                (
                    new CustomerModel() { Id = 1, FullName = "Juan Dela Cruz", Age = 25, DateOfBirth = DateTime.Parse("10/02/1995"), Address = addressColletion },
                    new CustomerModel() { Id = 2, FullName = "Jane Doe", Age = 24, DateOfBirth = DateTime.Parse("04/02/1996"), Address = addressColletion },
                    new CustomerModel() { Id = 3, FullName = "Marco Fuentes", Age = 23, DateOfBirth = DateTime.Parse("08/02/1997"), Address = addressColletion },
                    new CustomerModel() { Id = 4, FullName = "Mary Jane Herrera", Age = 23, DateOfBirth = DateTime.Parse("05/02/1997"), Address = addressColletion },
                    new CustomerModel() { Id = 5, FullName = "Catriona Grey", Age = 23, DateOfBirth = DateTime.Parse("09/02/1997"), Address = addressColletion }
                );

                context.SaveChanges();
            }
        }
    }
}
