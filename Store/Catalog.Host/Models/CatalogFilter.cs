using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models;

public class CatalogFilter
{
    [Required(ErrorMessage = "Brand is required")]
    [StringLength(25, ErrorMessage = "String length should be less than 25")]
    public int Brand { get; set; }
    
    [Required(ErrorMessage = "Type is required")]
    [StringLength(25, ErrorMessage = "String length should be less than 25")]
    public int Type { get; set; }
    
    [Required(ErrorMessage = "Category is required")]
    [StringLength(25, ErrorMessage = "String length should be less than 25")]
    public int Category { get; set; }
    
    public override string ToString()
    {
        return $"CatalogFilter: brand: {Brand}, type: {Type}, " +
               $"category: {Category}";
    }

}