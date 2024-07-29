using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FavoriteController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites()
    {
        return await _context.Favorites.ToListAsync();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Favorite>> PostFavorite(Favorite favorite)
    {
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetFavorite", new { id = favorite.Id }, favorite);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Favorite>> GetFavorite(int id)
    {
        var favorite = await _context.Favorites.FindAsync(id);
        if (favorite == null)
        {
            return NotFound();
        }
        return favorite;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFavorite(int id)
    {
        var favorite = await _context.Favorites.FindAsync(id);
        if (favorite == null)
        {
            return NotFound();
        }
        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
