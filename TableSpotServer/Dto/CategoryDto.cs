using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableSpot.Dto;

public class CategoryDto
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    public ICollection<RestaurantDto> Restaurants { get; set; } = null!;
}