using Microsoft.EntityFrameworkCore;
using FoodAPI.Models;
namespace FoodAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Taco> Tacos { get; set; }
    public DbSet<Pizza>Pizzas { get; set; }
    
    public DbSet<Burger> Burgers { get; set; } = null!;

}