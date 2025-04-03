using DinhVanHoangDuy_2180609183.Models;

namespace DinhVanHoangDuy_2180609183.Repositories
{
    public interface ISubjectRepository
    {
        Task <IEnumerable<Subject>> GetAllAsync();
        Task <Subject> GetByIdAsync(int id);
        Task CreateAsync(Subject subject);
        Task UpdateAsync(Subject subject);
        Task DeleteAsync(int id);
    }
}
