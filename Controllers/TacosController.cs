using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodAPI.Models;
using FoodAPI.Data; // Asegúrate que tu AppDbContext esté en este namespace

namespace FoodAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TacosController : ControllerBase
{
    private readonly AppDbContext _context;

    public TacosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/tacos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Taco>>> Get(string? nombre, string? tamanio)
    {
        var query = _context.Tacos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nombre))
        {
            query = query.Where(f => f.Nombre.Contains(nombre));
        }

        if (!string.IsNullOrWhiteSpace(tamanio))
        {
            query = query.Where(f => f.Tamanio != null && f.Tamanio.Equals(tamanio));
        }

        return Ok(await query.ToListAsync());
    }

    // GET: api/tacos/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Taco>> GetById(int id)
    {
        var food = await _context.Tacos.FindAsync(id);
        if (food == null) return NotFound();
        return Ok(food);
    }

    // POST: api/tacos
    [HttpPost]
    public async Task<ActionResult<Taco>> Create(Taco food)
    {
        food.CreadoEnUtc = DateTime.UtcNow;
        _context.Tacos.Add(food);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = food.Id }, food);
    }

    // PUT: api/tacos/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Replace(int id, Taco food)
    {
        if (id != food.Id)
        {
            return BadRequest("El ID de la URL no coincide con el del objeto enviado.");
        }

        var existing = await _context.Tacos.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Nombre = food.Nombre;
        existing.Ingredientes = food.Ingredientes;
        existing.Precio = food.Precio;
        existing.Tamanio = food.Tamanio;
        // No actualizamos CreadoEnUtc para preservar la fecha original

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/tacos/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _context.Tacos.FindAsync(id);
        if (existing == null) return NotFound();

        _context.Tacos.Remove(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
