using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UniversityAPI.Controllers;

[Route("api/grades")]
[ApiController]
public class GradeController : ControllerBase
{
    private readonly University _db;

    public GradeController(University context)
    {
        _db = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Grade>>> Get()
    {
        return await _db.Grades.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Grade>> Get(int id)
    {
        var grade = await _db.Grades.FindAsync(id);
        if (grade is null)
        {
            return NotFound();
        }

        return grade;
    }
    
    [HttpPost]
    public async Task<ActionResult<Grade>> Post(Grade grade)
    {
        if (grade.StudentsId is 0)
        {
            return BadRequest();
        }

        _db.Grades.Add(grade);
        await _db.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult<Grade>> Put(Grade grade)
    {
        if (grade.StudentsId is 0)
        {
            return BadRequest();
        }

        if (!_db.Grades.Any(g => g.GradeId == grade.GradeId))
        {
            return NotFound();
        }

        _db.Grades.Update(grade);
        await _db.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Grade>> Delete(int id)
    {
        var grade = await _db.Grades.FindAsync(id);
        if (grade is null)
        {
            return NotFound();
        }
        
        _db.Grades.Remove(grade);
        await _db.SaveChangesAsync();
        return Ok();
    }
}