using System.ComponentModel.DataAnnotations;

namespace TableSpot.Models;

public class ModifyRestaurantModel
{
    public string NewValue { get; set; } = null!;
    [AllowedValues(["name", "address", "description", "imageUrl", "categoryId", "email", "website", "phoneNumber"], ErrorMessage = "Invalid field."), Required(ErrorMessage = "Field is required.")]
    public string Field { get; set; } = null!;
}