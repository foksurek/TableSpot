using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableSpot.Dto;

public class EmployeeDto
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Account")]
    public int AccountId { get; set; }
    public AccountDto Account { get; set; } = null!;
    
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
    public RestaurantDto Restaurant { get; set; } = null!;
    
}