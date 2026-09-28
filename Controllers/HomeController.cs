using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RazorViewDrill.Models;

namespace RazorViewDrill.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var names = new List<string> { "Rakibul Hasan Pranto", "S. Rahman", "Zaman", "Solaiman Shakil" };
        return View(names);
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
