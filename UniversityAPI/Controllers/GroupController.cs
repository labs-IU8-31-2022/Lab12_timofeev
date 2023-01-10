using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UniversityAPI.Controllers;

[Route("api/groups")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly University _db;

    public GroupController(University context)
    {
        _db = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> Get()
    {
        return await _db.Groups.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> Get(int id)
    {
        var group = await _db.Groups.FindAsync(id);
        if (group is null)
        {
            return NotFound();
        }

        return group;
    }
    
    [HttpPost]
    public async Task<ActionResult<Group>> Post(Group group)
    {
        if (group.GroupName is null)
        {
            return BadRequest();
        }

        await _db.Groups.AddAsync(group);
        await _db.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult<Group>> Put(Group group)
    {
        if (group.GroupName is null)
        {
            return BadRequest();
        }

        if (!_db.Groups.Any(g => g.GroupId == group.GroupId))
        {
            return NotFound();
        }

        var gr = await _db.Groups.FindAsync(group.GroupId);
        _db.Entry(gr!).CurrentValues.SetValues(group);
        await _db.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Group>> Delete(int id)
    {
        var group = await _db.Groups.FindAsync(id);
        if (group is null)
        {
            return NotFound();
        }

        await _db.Students.Where(s => s.GroupsId == group.GroupId).ForEachAsync(s => s.GroupsId = null);
        _db.Groups.Remove(group);
        await _db.SaveChangesAsync();
        return Ok();
    }
}