using DinhVanHoangDuy_2180609183.Models;
using DinhVanHoangDuy_2180609183.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DinhVanHoangDuy_2180609183.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;

        public CourseController(ICourseRepository courseRepository, ISubjectRepository subjectRepository)
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
        }

        //Hiển thị danh sách Course
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllAsync();
            return View(courses);
        }

        //Hiển thị trang tạo mới Course
        public async Task<IActionResult> Add()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            ViewBag.Subjects = new SelectList(subjects, "Id", "Name");
            return View();
        }

        //Tạo mới Course
        [HttpPost]
        public async Task<IActionResult> Add(Course course, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    course.ImageUrl = await SaveImage(imageUrl);
                }
                await _courseRepository.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            //Nếu dữ liệu không hợp lệ
            var subjects = await _subjectRepository.GetAllAsync();
            ViewBag.Subjects = new SelectList(subjects, "Id", "Name");
            return View(course);
        }

        //Hàm lưu ảnh
        private async Task<string> SaveImage(IFormFile image)
        {
            var filePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }

        //Hiển thị thông tin chi tiết Course
        public async Task<IActionResult> Display(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        //Hiển thị trang cập nhật thông tin Course
        public async Task<IActionResult> Update(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            var subjects = await _subjectRepository.GetAllAsync();
            ViewBag.Subjects = new SelectList(subjects, "Id", "Name", course.SubjectId);
            return View(course);
        }

        //Xử lý cập nhật thông tin Course
        [HttpPost]
        public async Task<IActionResult> Add(int id, Course course, IFormFile imageUrl)
        {
            ModelState.Remove("ImageUrl");

            if (id != course.Id)
            {
                return NotFound();
            }

            //Kiểm tra dữ liệu hợp lệ
            if (ModelState.IsValid)
            {
                var existingCourse = await _courseRepository.GetByIdAsync(id);

                if (imageUrl != null)
                {
                    course.ImageUrl = await SaveImage(imageUrl);
                }
                else
                {
                    course.ImageUrl = existingCourse.ImageUrl;
                }

                existingCourse.Name = course.Name;
                existingCourse.Description = course.Description;
                existingCourse.Price = course.Price;
                existingCourse.ImageUrl = course.ImageUrl;
                existingCourse.SubjectId = course.SubjectId;

                await _courseRepository.UpdateAsync(existingCourse);
                return RedirectToAction(nameof(Index));
            }
            //Nếu dữ liệu không hợp lệ
            var subjects = await _subjectRepository.GetAllAsync();
            ViewBag.Subjects = new SelectList(subjects, "Id", "Name");
            return View(course);
        }

        //Form xác nhận xóa Course
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        //Xử lý xóa Course
        [HttpPost,ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
