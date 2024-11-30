using DeliveryOffice.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryOffice.Core.Configurations;

public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable("Buyers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

        builder
            .HasMany(x => x.Bills)
            .WithOne(x => x.Buyer)
            .HasForeignKey(x => x.BuyerId);

        builder.HasIndex(x => x.Name)
            .HasDatabaseName($"IX_{nameof(Buyer)}_{nameof(Buyer.Name)}")
            .IsUnique()
            .HasFilter("[Name] is not null");
    }
}
