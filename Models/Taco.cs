namespace FoodAPI.Models;

public class Taco
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Ingredientes { get; set; } = null!;
    public decimal Precio { get; set; }
    public string Tamanio { get; set; } = null!; // e.g. pequeño, mediano, grande
    public DateTime CreadoEnUtc { get; set; } = DateTime.UtcNow;
}