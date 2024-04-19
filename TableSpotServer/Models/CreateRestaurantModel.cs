using System.ComponentModel.DataAnnotations;

namespace TableSpot.Models;

public class CreateRestaurantModel
{
    [Required, StringLength(64, MinimumLength = 3)] public string Name { get; set; } = null!;
    [Required, StringLength(64, MinimumLength = 3)] public string Address { get; set; } = null!;
    
    [Required, StringLength(512, MinimumLength = 3)] public string Description { get; set; } = null!;
    [Required, StringLength(256, MinimumLength = 3)] public string ImageUrl { get; set; } = null!;
    [Required, Range(1, 99, ErrorMessage = "Category field is invalid")] public int CategoryId { get; set; }
    [EmailAddress, StringLength(32, MinimumLength = 3)] public string? Email { get; set; }
    [StringLength(32, MinimumLength = 3)]public string? Website { get; set; }
    [Phone] public string? PhoneNumber { get; set; }
}