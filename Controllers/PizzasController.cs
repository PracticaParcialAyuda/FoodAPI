using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodAPI.Data;
using FoodAPI.Models;

namespace FoodAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    private readonly AppDbContext _context;
    public PizzasController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        => await _context.Pizzas.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pizza>> GetPizza(int id)
        => await _context.Pizzas.FindAsync(id) is { } p ? p : NotFound();

    [HttpPost]
    public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        _context.Pizzas.Add(pizza);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPizza), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPizza(int id, Pizza pizza)
    {
        if (id != pizza.Id) return BadRequest();
        _context.Entry(pizza).State = EntityState.Modified;
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException)
        { if (!_context.Pizzas.Any(p => p.Id == id)) return NotFound(); throw; }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePizza(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);
        if (pizza is null) return NotFound();
        _context.Pizzas.Remove(pizza);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}