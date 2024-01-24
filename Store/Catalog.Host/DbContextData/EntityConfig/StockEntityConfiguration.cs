using Catalog.Host.DbContextData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.DbContextData.EntityConfig;

public class StockEntityConfiguration: IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("stock");
        builder.HasKey(stock => stock.Id);
        builder.Property(stock => stock.Id)
            .HasColumnName("id")
            .UseIdentityColumn()
            .IsRequired();

        builder.HasOne(stock => stock.Item)
            .WithMany()
            .HasForeignKey(stock => stock.ItemId);

        builder.Property(stock => stock.ItemId)
            .HasColumnName("item_id")
            .IsRequired();

        builder.Property(stock => stock.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(stock => stock.Size)
            .HasColumnName("size")
            .IsRequired();
    }
}