using System.ComponentModel.DataAnnotations;

namespace Courses_Microservices
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]

        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public int Credits { get; set; }


    }
}
