using DinhVanHoangDuy_2180609183.Models;
using Microsoft.EntityFrameworkCore;

namespace DinhVanHoangDuy_2180609183.Repositories
{
    public class EFCourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Hàm lấy toàn bộ thông tin Course
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.Include(p => p.Subject).ToListAsync();
        }

        //Hàm lấy thông tin Course theo id
        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.Include(p => p.Subject).FirstOrDefaultAsync(p => p.Id == id);
        }

        //Hàm tạo mới Course
        public async Task CreateAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        //Hàm cập nhật thông tin Course
        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        //Hàm xóa Course theo Id
        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
