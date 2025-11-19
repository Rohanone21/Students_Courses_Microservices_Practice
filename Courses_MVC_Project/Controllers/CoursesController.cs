using Courses_MVC_Project.Models;
using Courses_MVC_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Courses_MVC_Project.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var courses = await _courseService.GetCourseByIdAsync(id);

            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                var courses = await _courseService.CreateCourseAsync(course);
                if (course != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error creating course. Please try again.");
            }


            return View(course);

        }
        public async Task<IActionResult> Edit(int id)
        {
            var Course = await _courseService.GetCourseByIdAsync(id);
            if (Course == null)
            {
                return NotFound();
            }
            return View(Course);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var success = await _courseService.UpdateCourseAsync(id, course);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error updating course. Please try again.");
            }
            return View(course);
        }
        

    public async Task<IActionResult> Delete(int id)
        {
            var Courses = await _courseService.GetCourseByIdAsync(id);
            if(Courses== null)
            {
                return NotFound();
            }
            return View(Courses);

        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success= await _courseService.DeleteCourseAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            var courses = await _courseService.GetCourseByIdAsync(id);
            return View("Delete",courses);
        }

    }
}
