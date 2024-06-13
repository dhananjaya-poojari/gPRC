using gRPC.Protos;
using gRPC.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Server.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Customer> _customers =
                    [
                        new Customer
                            {
                Id = "12345678",
                    Name = "John Doe",
        Address = "1234 Elm Street, Springfield, IL 62704, USA",
        Age = 30
                        },
    new Customer
    {
        Id = "87654321",
        Name = "Jane Smith",
        Address = "5678 Oak Street, Springfield, IL 62704, USA",
        Age = 25
    },
    new Customer
    {
        Id = "11223344",
        Name = "Alice Johnson",
        Address = "9101 Maple Avenue, Springfield, IL 62704, USA",
        Age = 40
    }
                ];
            foreach (Customer customer in _customers)
            {
                modelBuilder.Entity<Customer>().HasData(customer);
            }
        }

    }
}
