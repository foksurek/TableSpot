using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableSpot.Dto;

public class MenuDto
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
    public RestaurantDto Restaurant { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
}