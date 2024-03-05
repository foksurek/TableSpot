using System.ComponentModel.DataAnnotations;

namespace TableSpot.Contexts;

public class LoginModel
{
    [Required]
    [StringLength(64, MinimumLength = 3)]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    [StringLength(64, MinimumLength = 8)]
    public string Password { get; set; } = null!;

}