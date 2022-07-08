using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id); //Podesavam primarni kljuc tabele

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).HasMaxLength(30).IsRequired();
            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Password).IsRequired();

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Surname).IsRequired();

            builder.Property(x => x.DateOfBirth).IsRequired();

            builder.Property(x => x.Address).IsRequired();

        }
    }
}
