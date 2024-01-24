using Catalog.Host.DbContextData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.DbContextData.EntityConfig;

public class ItemEntityConfiguration: IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("item");
        builder.HasKey(item => item.Id);
        builder.Property(item => item.Id)
            .HasColumnName("id")
            .UseIdentityColumn()
            .IsRequired();

        builder.HasOne(item => item.CatalogItem)
            .WithMany()
            .HasForeignKey(item => item.CatalogItemId);

        builder.Property(item => item.CatalogItemId)
            .HasColumnName("catalog_item_id")
            .IsRequired();
    }
}