using System.ComponentModel.DataAnnotations;
namespace FoodAPI.Models;

public class Pizza
{
    [Key] public int Id { get; set; }

    [Required, MaxLength(80)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(200)]
    public string Ingredients { get; set; } = null!;

    [Range(0, 1000)]
    public decimal Price { get; set; }

    [Required, MaxLength(20)]
    public string Size { get; set; } = null!;
}