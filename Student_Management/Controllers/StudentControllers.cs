using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _ctx;

        public StudentsController(StudentDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _ctx.Students.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student s)
        {
            _ctx.Students.Add(s);
            await _ctx.SaveChangesAsync();
            return Ok(s);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student s)
        {
            var existing = await _ctx.Students.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = s.Name;
            existing.Class = s.Class;
            existing.Section = s.Section;

            await _ctx.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var existing = await _ctx.Students.FindAsync(id);
            if (existing == null) return NotFound();

            _ctx.Students.Remove(existing);
            await _ctx.SaveChangesAsync();

            return Ok();
        }
    }
}
