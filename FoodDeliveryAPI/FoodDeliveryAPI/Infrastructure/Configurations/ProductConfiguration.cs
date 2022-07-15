using FoodDeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDeliveryAPI.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Name).IsRequired(); 

            builder.Property(x => x.Price).IsRequired(); 

            builder.Property(x => x.Ingredients).IsRequired();


            
        }        

    }
}
