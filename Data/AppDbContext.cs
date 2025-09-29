using Microsoft.EntityFrameworkCore;
using FoodAPI.Models;
namespace FoodAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Taco> Tacos { get; set; }
}