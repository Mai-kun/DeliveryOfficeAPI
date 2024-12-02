using DeliveryOffice.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryOffice.Core.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Unit).HasMaxLength(50);

        builder
            .HasMany(x => x.Bills)
            .WithMany(x => x.Products);

        builder.HasIndex(x => x.Name)
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.Name)}")
            .IsUnique()
            .HasFilter("[Name] is not null");
    }
}
