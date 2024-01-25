using Catalog.Host.DbContextData.Entities;

namespace Catalog.Host.DbContextData;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.ItemBrands.Any())
        {
            await context.ItemBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

            await context.SaveChangesAsync();
        }
        
        if (!context.ItemTypes.Any())
        {
            await context.ItemTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }
        
        if (!context.ItemCategories.Any())
        {
            await context.ItemCategories.AddRangeAsync(GetPreconfiguredCatalogCategories());

            await context.SaveChangesAsync();
        }
        
        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredCatalogCatalogItems());

            await context.SaveChangesAsync();
        }
        
        if (!context.Items.Any())
        {
            await context.Items.AddRangeAsync(GetPreconfiguredCatalogStocks());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<ItemBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<ItemBrand>
        {
            new() { Brand = "brand-1" },
            new() { Brand = "brand-2" },
            new() { Brand = "brand-3" },
            new() { Brand = "brand-4" },
            new() { Brand = "brand-5" }
        };
    }
    
    private static IEnumerable<ItemType> GetPreconfiguredCatalogTypes()
    {
        return new List<ItemType>
        {
            new() { Type = "type-1" },
            new() { Type = "type-2" },
            new() { Type = "type-3" },
            new() { Type = "type-4" },
            new() { Type = "type-5" }
        };
    }
    
    private static IEnumerable<ItemCategory> GetPreconfiguredCatalogCategories()
    {
        return new List<ItemCategory>
        {
            new() { Category = "category-1" },
            new() { Category = "category-2" },
            new() { Category = "category-3" },
            new() { Category = "category-4" },
            new() { Category = "category-5" }
        };
    }
    
    private static IEnumerable<CatalogItem> GetPreconfiguredCatalogCatalogItems()
    {
        return new List<CatalogItem>
        {
            new()
            {
                Name = "name-1", ItemBrandId = 1, ItemTypeId = 1, Price = 120, Image = "img-1", ItemCategoryId = 1,
                Description = "description"
            },
            new()
            {
                Name = "name-2", ItemBrandId = 2, ItemTypeId = 2, Price = 150, Image = "img-2", ItemCategoryId = 2,
                Description = "description"
            },
            new()
            {
                Name = "name-1", ItemBrandId = 3, ItemTypeId = 3, Price = 160, Image = "img-3", ItemCategoryId = 3,
                Description = "description"
            },
            new()
            {
                Name = "name-1", ItemBrandId = 4, ItemTypeId = 4, Price = 170, Image = "img-4", ItemCategoryId = 4,
                Description = "description"
            },
            new()
            {
                Name = "name-1", ItemBrandId = 5, ItemTypeId = 5, Price = 200, Image = "img-5", ItemCategoryId = 5,
                Description = "description"
            }
        };
    }
    
    private static IEnumerable<Item> GetPreconfiguredCatalogStocks()
    {
        return new List<Item>
        {
            new() { CatalogItemId = 1, Quantity = 1, Size = "s" },
            new() { CatalogItemId = 1, Quantity = 2, Size = "m" },
            new() { CatalogItemId = 2, Quantity = 1, Size = "s" },
            new() { CatalogItemId = 2, Quantity = 3, Size = "m" },
            new() { CatalogItemId = 2, Quantity = 1, Size = "l" },
            new() { CatalogItemId = 2, Quantity = 2, Size = "xl" },
            new() { CatalogItemId = 3, Quantity = 1, Size = "xs" },
            new() { CatalogItemId = 3, Quantity = 1, Size = "m" },
            new() { CatalogItemId = 3, Quantity = 1, Size = "l" },
            new() { CatalogItemId = 3, Quantity = 2, Size = "xl" },
        };
    }
}