using DinhVanHoangDuy_2180609183_Web.Models;
using DinhVanHoangDuy_2180609183_Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DinhVanHoangDuy_2180609183_Web.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = SD.Role_Teacher)]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CourseController(ICourseRepository courseRepository, ICategoryRepository categoryRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllAsync();
            return View(courses);
        }

        // Hiển thị form thêm sản phẩm mới
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Course course, IFormFile
       imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage
                    course.ImageUrl = await SaveImage(imageUrl);
                }
                await _courseRepository.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(course);
        }


        // Hàm SaveImage
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            // Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        }

        public async Task<IActionResult> Display(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name",
            course.CategoryId);
            return View(course);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Course course, IFormFile imageUrl)
        {
            ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho ImageUrl
            if (id != course.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingCourse = await _courseRepository.GetByIdAsync(id);
                // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên
                if (imageUrl == null)
                {
                    course.ImageUrl = existingCourse.ImageUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    course.ImageUrl = await SaveImage(imageUrl);
                }

                // Cập nhật các thông tin khác của sản phẩm
                existingCourse.Name = course.Name;
                existingCourse.Price = course.Price;
                existingCourse.Description = course.Description;
                existingCourse.CategoryId = course.CategoryId;
                existingCourse.ImageUrl = course.ImageUrl;

                await _courseRepository.UpdateAsync(existingCourse);

                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(course);
        }

        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
