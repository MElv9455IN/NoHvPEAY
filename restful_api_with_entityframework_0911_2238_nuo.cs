// 代码生成时间: 2025-09-11 22:38:54
 * It includes error handling, comments, and follows C# best practices for maintainability and scalability.
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Assuming 'DbContext' is your Entity Framework Core DbContext class
public class MyDbContext : DbContext
{
    // Define your database sets here
    public DbSet<MyEntity> MyEntities { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
}

// Entity class
public class MyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Add other properties as needed
}

[Route("api/[controller]/")]
[ApiController]
public class MyEntityController : ControllerBase
{
    private readonly MyDbContext _context;

    public MyEntityController(MyDbContext context)
    {
        _context = context;
    }

    // GET: api/MyEntity
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MyEntity>>> GetMyEntities()
    {
        return await _context.MyEntities.ToListAsync();
    }

    // GET: api/MyEntity/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MyEntity>> GetMyEntity(int id)
    {
        var myEntity = await _context.MyEntities.FindAsync(id);
        if (myEntity == null)
        {
            return NotFound();
        }
        return myEntity;
    }

    // POST: api/MyEntity
    [HttpPost]
    public async Task<ActionResult<MyEntity>> PostMyEntity(MyEntity myEntity)
    {
        _context.MyEntities.Add(myEntity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMyEntity), new { id = myEntity.Id }, myEntity);
    }

    // PUT: api/MyEntity/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMyEntity(int id, MyEntity myEntity)
    {
        if (id != myEntity.Id)
        {
            return BadRequest();
        }
        _context.Entry(myEntity).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MyEntityExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    // DELETE: api/MyEntity/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMyEntity(int id)
    {
        var myEntity = await _context.MyEntities.FindAsync(id);
        if (myEntity == null)
        {
            return NotFound();
        }
        _context.MyEntities.Remove(myEntity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool MyEntityExists(int id)
    {
        return _context.MyEntities.Any(e => e.Id == id);
    }
}