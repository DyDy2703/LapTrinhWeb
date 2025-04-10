using DinhVanHoangDuy_2180609183_Web.Models;

namespace DinhVanHoangDuy_2180609183_Web.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task AddAsync(Course product);
        Task UpdateAsync(Course product);
        Task DeleteAsync(int id);

    }
}
