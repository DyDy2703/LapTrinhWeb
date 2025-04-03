using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DinhVanHoangDuy_2180609183.Models;
using DinhVanHoangDuy_2180609183.Repositories;

namespace DinhVanHoangDuy_2180609183.Controllers;

public class HomeController : Controller
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, ICourseRepository courseRepository)
    {
        _logger = logger;
        _courseRepository = courseRepository;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _courseRepository.GetAllAsync();
        return View(courses);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
