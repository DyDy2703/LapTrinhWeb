namespace DinhVanHoangDuy_2180609183.Models
{
    public class CourseImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
