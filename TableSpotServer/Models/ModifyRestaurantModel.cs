using System.ComponentModel.DataAnnotations;

namespace TableSpot.Models;

public class ModifyRestaurantModel
{
    public string NewValue { get; set; } = null!;
    [AllowedValues(["Name", "Address", "Description", "ImageUrl", "CategoryId", "Email", "Website", "PhoneNumber"], ErrorMessage = "Invalid field.")]
    public string Field { get; set; } = null!;
}