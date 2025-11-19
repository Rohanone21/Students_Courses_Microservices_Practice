using System.ComponentModel.DataAnnotations;

namespace Courses_MVC_Project.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [Range(1,10)]
        public int Credits { get; set; }
    }
}
