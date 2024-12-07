using DeliveryOffice.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryOffice.Core.Configurations;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bills");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date);
        builder.Property(x => x.Warehouse).IsRequired();

        builder
            .HasOne(c => c.Buyer)
            .WithMany(x => x.Bills)
            .HasForeignKey(c => c.BuyerId);

        builder
            .HasOne(c => c.Supplier)
            .WithMany(x => x.Bills)
            .HasForeignKey(c => c.SupplierId);

        builder
            .HasMany(x => x.Products)
            .WithMany(x => x.Bills);
    }
}
