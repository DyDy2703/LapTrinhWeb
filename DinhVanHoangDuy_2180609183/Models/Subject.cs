using System.ComponentModel.DataAnnotations;

namespace DinhVanHoangDuy_2180609183.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required,StringLength(100)]
        public string Name { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
