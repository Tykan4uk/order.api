using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderApi.Data.Entities;

namespace OrderApi.Data.EntityConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product")
                .HasKey(h => h.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasMaxLength(255);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnName("Price");

            builder.Property(p => p.Type)
                .IsRequired()
                .HasColumnName("ProductType");

            builder.HasOne(h => h.Order)
                .WithMany(w => w.Products)
                .HasForeignKey(h => h.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
