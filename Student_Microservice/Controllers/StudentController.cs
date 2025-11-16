using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Microservice.Data;
using Student_Microservice.Models;

namespace Student_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly StudentDbContext _Context;

        public StudentController(StudentDbContext context)
        {
            _Context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _Context.Students.OrderBy(s => s.Name).ToListAsync();

            return Ok(students);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var students = await _Context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpPost]

        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            var existing = await _Context.Students.FirstOrDefaultAsync(s => s.Email == student.Email);

            if (existing != null)
            {
                return BadRequest("Email already exists");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            _Context.Students.Add(student);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest("Id Does not Present");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var existing = await _Context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if (existing == null)
            {
                return NotFound("Student Not Found");
            }

            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.EnrollmentDate = student.EnrollmentDate;

            _Context.Update(existing);
            await _Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var students = await _Context.Students.FindAsync(id);

            if(students == null)
            {
                return NotFound();
            }
            _Context.Students.Remove(students);
            await _Context.SaveChangesAsync();
            return NoContent();
        }

    }
}
