using DinhVanHoangDuy_2180609183.Models;
using Microsoft.EntityFrameworkCore;

namespace DinhVanHoangDuy_2180609183.Repositories
{
    public class EFSubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;
        public EFSubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //Hàm lấy toàn bộ thông tin Subject
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.Include(p => p.Courses).ToListAsync();
        }
        //Hàm lấy thông tin Subject theo id
        public async Task<Subject> GetByIdAsync(int id)
        {
            return await _context.Subjects.Include(p => p.Courses).FirstOrDefaultAsync(p => p.Id == id);
        }
        //Hàm tạo mới Subject
        public async Task CreateAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }
        //Hàm cập nhật thông tin Subject
        public async Task UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }
        //Hàm xóa Subject theo Id
        public async Task DeleteAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
    }
}
