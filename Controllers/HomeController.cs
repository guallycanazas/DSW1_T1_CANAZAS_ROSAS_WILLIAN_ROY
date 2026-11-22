using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DSW1_T1_CANAZAS_ROSAS_WILLIAN_ROY.Models;

namespace DSW1_T1_CANAZAS_ROSAS_WILLIAN_ROY.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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
