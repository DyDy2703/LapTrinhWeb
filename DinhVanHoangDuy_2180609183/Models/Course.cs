using System.ComponentModel.DataAnnotations;

namespace DinhVanHoangDuy_2180609183.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Range(1000, 1000000)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<CourseImage>? Images { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

    }
}
