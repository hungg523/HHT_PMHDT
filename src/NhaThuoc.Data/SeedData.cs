using Microsoft.EntityFrameworkCore;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data
{
    public static class SeedData
    {
        public static void SeedDataGenerate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                PhoneNumber = "0123456789",
                Email = "hieukaijay@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("123456788A@a!"),
                IsActive = true,
                CreatedAt = DateTime.Now,
                Role = 1,
                FirstName = "Admin"
            });
        }
    }
}