using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductAssemblySystem.UserManagement.Domain.Entities;

namespace ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                    right => right.HasOne<Role>().WithMany().HasForeignKey(r => r.RoleId),
                    left => left.HasOne<User>().WithMany().HasForeignKey(u => u.UserId));

            builder.ComplexProperty(u => u.Name, n =>
            {
                n.IsRequired();

                n.Property(n => n.FirstName).IsRequired().HasColumnName("FirstName").HasMaxLength(64);
                n.Property(n => n.LastName).IsRequired().HasColumnName("LastName").HasMaxLength(64);
            });

            builder.ComplexProperty(u => u.Email, e =>
            {
                e.IsRequired();

                e.Property(e => e.EmailAddress).IsRequired().HasColumnName("Email").HasMaxLength(64);
            });

            builder.ComplexProperty(u => u.PhoneNumber, p =>
            {
                p.IsRequired();

                p.Property(p => p.Number).IsRequired().HasColumnName("PhoneNumber").HasMaxLength(64);
            });

            builder.ComplexProperty(u => u.Address, a =>
            {
                a.IsRequired();

                a.Property(a => a.PostalCode).IsRequired().HasColumnName("PostalCode").HasMaxLength(64);
                a.Property(a => a.Region).IsRequired().HasColumnName("Region").HasMaxLength(64);
                a.Property(a => a.City).IsRequired().HasColumnName("City").HasMaxLength(64);
                a.Property(a => a.Street).IsRequired().HasColumnName("Street").HasMaxLength(64);
                a.Property(a => a.HouseNumber).IsRequired().HasColumnName("HouseNumber").HasMaxLength(64);
                a.Property(a => a.ApartmentNumber).IsRequired().HasColumnName("ApartmentNumber").HasMaxLength(64);
            });

            builder.ComplexProperty(u => u.PasswordHash, p =>
            {
                p.IsRequired();

                p.Property(p => p.Hash).IsRequired().HasColumnName("PasswordHash").HasMaxLength(128);
            });   
        }
    }
}
