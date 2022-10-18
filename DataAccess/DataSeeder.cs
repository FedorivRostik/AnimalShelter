using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DataAccess;
public static class DataSeeder
{
    public static void SeedRolesAndAdmin(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new[]
        {
           new Role() { Id = 1, Name = "Admin" } ,
            new Role() {Id = 2, Name = "Manager"},
            new Role() {Id = 3, Name = "Client"}
        });

        byte[] passwordSalt;
        byte[] passwordHash;
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Admin1"));
        }
        var admin = new User()
        {
            Id = 1,
            FirstName = "Adam",
            SecondName = "Adamson",
            Email = "admin@gmail.com",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RoleId = 1
        };
        modelBuilder.Entity<User>().HasData(admin);

        // seed admin

    }
}
