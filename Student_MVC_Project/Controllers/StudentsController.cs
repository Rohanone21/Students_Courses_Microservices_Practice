using Microsoft.AspNetCore.Mvc;
using Student_MVC_Project.Service;
using Student_MVC_Project.Models;
namespace Student_MVC_Project.Controllers
{
    public class StudentsController : Controller
    {

        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students= await _studentService.GetAllStudentsAsync();
            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var students= await _studentService.GetStudentByIdAsync(id);

            if(students==null)
            {
                return NotFound();
            }
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await  _studentService.CreateStudentAsync(student);
                    TempData["SuccessMessage"] = "Student created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (ApplicationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(student);

        }

        public async Task<IActionResult> Edit(int id)
        {

            var students = await _studentService.GetStudentByIdAsync(id);
            if(students == null)
            {
                return NotFound();
            }
            return View(students);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    await _studentService.UpdateStudentAsync(id, student);
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (ApplicationException ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var students = await _studentService.GetStudentByIdAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {
                var success = await _studentService.DeleteStudentAsync(id);
                if (success)
                {
                    TempData["SuccessMessage"] = "Student deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error deleting student!";
                }
            }
            catch (ApplicationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
          return RedirectToAction(nameof(Index));
        }
    }
}
