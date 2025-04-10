namespace DinhVanHoangDuy_2180609183_Web.Models
{
    public class CourseImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
