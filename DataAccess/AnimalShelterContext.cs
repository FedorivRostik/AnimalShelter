using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
public class AnimalShelterContext : DbContext
{
    public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options)
           : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.SeedRolesAndAdmin();

    }

    public DbSet<Role>? Role { get; set; }
    public DbSet<User>? User { get; set; }
}
