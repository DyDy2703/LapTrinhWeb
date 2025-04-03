using DinhVanHoangDuy_2180609183.Models;

namespace DinhVanHoangDuy_2180609183.Repositories
{
    public interface ICourseRepository
    {
        Task <IEnumerable<Course>> GetAllAsync();
        Task <Course> GetByIdAsync(int id);
        Task CreateAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
    }
}
