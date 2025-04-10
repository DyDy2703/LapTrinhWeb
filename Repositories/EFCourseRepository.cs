using DinhVanHoangDuy_2180609183_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DinhVanHoangDuy_2180609183_Web.Repositories
{
    public class EFCourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.Courses
            .Include(p => p.Category) // Include thông tin về category
            .ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Courses.Include(p =>
           p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
