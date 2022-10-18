using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder
            .Property(x => x.Email)
            .HasMaxLength(62)
            .IsRequired();

        builder
           .Property(x => x.FirstName)
           .HasMaxLength(46);

        builder
           .Property(x => x.SecondName)
           .HasMaxLength(46);


        builder
         .Property(x => x.PasswordHash)
         .IsRequired();

        builder
         .Property(x => x.PasswordSalt)
         .IsRequired();

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);

        builder.ToTable("User");
    }
}
