using System.ComponentModel.DataAnnotations;

namespace DinhVanHoangDuy_2180609183_Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
