using System.ComponentModel.DataAnnotations;

namespace TableSpot.Dto;

public class AccountDto
{
    [Required, Key] public int Id { get; set; }
    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
    [Required] public int AccountTypeId { get; set; }
    
    public ICollection<RestaurantDto> Restaurants { get; set; } = null!;
}