using System.ComponentModel.DataAnnotations.Schema;

namespace TableSpot.Dto;

public class OrderDto
{
    public int Id { get; set; }
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
    public RestaurantDto Restaurant { get; set; } = null!;
    public ICollection<OrderElementDto> OrderElement { get; set; } = null!;
}