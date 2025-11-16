using System.ComponentModel.DataAnnotations;

namespace Student_MVC_Project.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enrollment date is required")]
        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}
