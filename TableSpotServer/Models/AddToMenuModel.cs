using System.ComponentModel.DataAnnotations;

namespace TableSpot.Models;

public class AddToMenuModel
{
    public int RestaurantId { get; set; }
    [StringLength(64, MinimumLength = 3)] 
    public string Name { get; set; } = null!;
    [StringLength(124, MinimumLength = 3)] 
    public string Description { get; set; } = null!;
    [Range(0, 9999)] 
    public decimal Price { get; set; }
    [Url] 
    public string? ImageUrl { get; set; }
    
}