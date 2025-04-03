using DinhVanHoangDuy_2180609183.Models;
using DinhVanHoangDuy_2180609183.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DinhVanHoangDuy_WebLab.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        //private readonly ApplicationDbContext _context;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            //_courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            //_context = context;
        }

        //Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            return View(subjects);
        }

        //Hiển thị form thêm danh mục sản phẩm
        public async Task<IActionResult> Add()
        {
            return View();
        }

        //Thêm sản phẩm
        [HttpPost]
        public async Task<IActionResult> Add(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.CreateAsync(subject);
                return RedirectToAction(nameof(Index));

            }

            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            return View(subject);
        }

        
        // Hiển thị thông tin chi tiết danh mục
        public async Task<IActionResult> Display(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // Hiển thị form cập nhật danh mục sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _subjectRepository.UpdateAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            var subjects = await _subjectRepository.GetAllAsync();
            return View(subject);
        }

        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject != null)
            {
                _subjectRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
