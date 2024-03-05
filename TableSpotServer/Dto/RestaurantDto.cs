using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableSpot.Dto;

public class RestaurantDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string Description { get; set; }
    //TODO: Save image and save file path in database instead of saving the image url in the database
    // Eventually, get favicon from resturant website if it exists
    [Required] public string ImageUrl { get; set; }
    [ForeignKey("Category")] public int CategoryId { get; set; }
    public CategoryDto Category { get; set; } = null!;
    
    [ForeignKey("Account")] public int AccountId { get; set; }
    public AccountDto Account { get; set; } = null!;
    
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? PhoneNumber { get; set; }
}