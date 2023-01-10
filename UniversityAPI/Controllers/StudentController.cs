using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UniversityAPI.Controllers;

[Route("api/students")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly University _db;

    public StudentController(University context)
    {
        _db = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> Get()
    {
        return await _db.Students.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> Get(int id)
    {
        var student = await _db.Students.FindAsync(id);
        if (student is null)
        {
            return NotFound();
        }

        return student;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Post(Student student)
    {
        if (student.StudentName is null)
        {
            return BadRequest();
        }

        _db.Students.Add(student);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<Student>> Put(Student student)
    {
        if (student.StudentName is null)
        {
            return BadRequest();
        }

        if (!_db.Students.Any(s => s.StudentId == student.StudentId))
        {
            return NotFound();
        }

        var stud = await _db.Grades.FindAsync(student.StudentId);
        _db.Entry(stud).CurrentValues.SetValues(student);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Student>> Delete(int id)
    {
        var student = await _db.Students.FindAsync(id);
        if (student is null)
        {
            return NotFound();
        }

        _db.Grades.RemoveRange(_db.Grades.Where(g => g.StudentsId == student.StudentId));
        _db.Students.Remove(student);
        await _db.SaveChangesAsync();
        return Ok();
    }
}