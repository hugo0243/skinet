using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> prodEntityBuilder)
        {
            prodEntityBuilder.Property(prod => prod.Id).IsRequired();
            prodEntityBuilder.Property(prod => prod.Name).IsRequired().HasMaxLength(100);
            prodEntityBuilder.Property(prod => prod.Description).IsRequired().HasMaxLength(180);
            prodEntityBuilder.Property(prod => prod.Price).HasColumnType("decimal(18,2)");
            prodEntityBuilder.Property(prod => prod.PictureUrl).IsRequired();
            prodEntityBuilder.HasOne(b => b.ProductBrand).WithMany()
                             .HasForeignKey(prod => prod.ProductBrandId);
            prodEntityBuilder.HasOne(t => t.ProductType).WithMany()
                             .HasForeignKey(prod => prod.ProductTypeId);
        }
    }
}