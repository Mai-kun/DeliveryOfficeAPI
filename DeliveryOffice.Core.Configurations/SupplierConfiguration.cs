using DeliveryOffice.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryOffice.Core.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Address).IsRequired();

        builder
            .HasMany(x => x.Bills)
            .WithOne(x => x.Supplier)
            .HasForeignKey(x => x.SupplierId);

        builder.HasIndex(x => x.Name)
            .HasDatabaseName($"IX_{nameof(Supplier)}_{nameof(Supplier.Name)}")
            .IsUnique()
            .HasFilter("[Name] is not null");
    }
}
