using System.ComponentModel.DataAnnotations.Schema;

namespace TableSpot.Dto;

public class OrderElementDto
{
    public int Id { get; set; }
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public OrderDto Order { get; set; } = null!;
    public int Quantity { get; set; }
    [ForeignKey("MenuItem")]
    public int MenuItemId { get; set; }
    public MenuDto MenuItem { get; set; } = null!;
}