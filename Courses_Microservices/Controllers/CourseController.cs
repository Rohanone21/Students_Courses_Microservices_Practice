using Courses_Microservices.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public CourseController(ApplicationDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
         var courses= await _Context.Courses.OrderBy(c => c.Title).ToListAsync();
            return Ok(courses);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var courses = await _Context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (courses == null)
            {
                return NotFound();
            }
            return Ok(courses);
        }

        [HttpPost]

        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _Context.Courses.Add(course);
            await _Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> UpdateCourse(int id,Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound("ID Not Found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCourses = await _Context.Courses.FindAsync(id);
            if (existingCourses == null)
            {
                return NotFound("Course Does not Found");
            }
            existingCourses.Title=course.Title;
            existingCourses.Description = course.Description;
            existingCourses.Credits= course.Credits;

            _Context.Courses.Update(existingCourses);
            await _Context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Course>> DeleteCourses(int id)
        {
            var courses = await _Context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound("Courses not found");
            }
            _Context.Courses.Remove(courses);
            await _Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
