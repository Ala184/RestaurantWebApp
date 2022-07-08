using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id); 

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Comment).HasMaxLength(512);

            builder.HasOne(x=>x.User)
                   .WithMany(x=>x.Orders)
                   .HasForeignKey(x=>x.UsersId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Products);
            //.WithMany(x => x.Orders);

            builder.Property(x => x.DelivererId).HasDefaultValue(0);

        }
    }
}
