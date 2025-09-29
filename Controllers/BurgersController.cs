using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodAPI.Data;
using FoodAPI.Models;

namespace FoodAPI.Controllers;

[ApiController]
[Route("api/[controller]")] // => /api/burgers
public class BurgersController(AppDbContext db) : ControllerBase
{
    [HttpGet] public async Task<IEnumerable<Burger>> Get()
        => await db.Burgers.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Burger>> Get(int id)
        => await db.Burgers.FindAsync(id) is { } b ? b : NotFound();

    [HttpPost]
    public async Task<ActionResult<Burger>> Post(Burger dto)
    {
        db.Burgers.Add(dto);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Burger dto)
    {
        if (id != dto.Id) return BadRequest();
        if (!await db.Burgers.AnyAsync(b => b.Id == id)) return NotFound();
        db.Entry(dto).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var b = await db.Burgers.FindAsync(id);
        if (b is null) return NotFound();
        db.Burgers.Remove(b);
        await db.SaveChangesAsync();
        return NoContent();
    }
}