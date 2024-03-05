namespace TableSpot.Models;

public class RestaurantResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public object Category { get; set; } = null!;
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? PhoneNumber { get; set; }
}