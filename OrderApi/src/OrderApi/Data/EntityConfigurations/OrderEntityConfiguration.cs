using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderApi.Data.Entities;

namespace OrderApi.Data.EntityConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Order")
                .HasKey(h => h.Id);

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasMaxLength(50);

            builder.Property(p => p.CreateDate)
                .IsRequired()
                .HasColumnName("CreateDate");
        }
    }
}
