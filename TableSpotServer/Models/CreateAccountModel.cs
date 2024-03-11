using System.ComponentModel.DataAnnotations;

namespace TableSpot.Models;

public class CreateAccountModel
{
    [Required]
    [StringLength(64, MinimumLength = 3)]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    [StringLength(64, MinimumLength = 8)]
    public string Password { get; set; } = null!;
    
    [Required]
    [StringLength(64, MinimumLength = 3)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(64, MinimumLength = 3)]
    public string Surname { get; set; } = null!;
    
    [Required]
    [AllowedValues([1, 2, 3, 4], ErrorMessage = "Invalid account type. Must be 1, 2, 3, or 4.")]
    public int AccountTypeId { get; set; }
}