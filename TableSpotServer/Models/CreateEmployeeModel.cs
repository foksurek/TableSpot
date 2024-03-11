using System.ComponentModel.DataAnnotations;

namespace TableSpot.Models;

public class CreateEmployeeModel
{
    [Required]
    public int RestaurantId { get; set; }
    [Required, EmailAddress, StringLength(64, MinimumLength = 3)]
    public string Email { get; set; } = null!;
    [Required, StringLength(64, MinimumLength = 3)]
    public string Name { get; set; } = null!;
    [Required, StringLength(64, MinimumLength = 3)]
    public string Surname { get; set; } = null!;
}