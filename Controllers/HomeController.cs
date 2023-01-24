using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SwapiMVC.Models;

namespace SwapiMVC.Controllers;

// The ": Controller" is what will brings in access to our View functionality later on. 
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static Random _random = new Random();
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

//Actions: what make the view work
    public IActionResult Index()
    {
        string[] names = new[] { "Autumn", "Alecx", "Tom", "Kenn", "Paul" };           
        string name = names[_random.Next(0, names.Length)];
        return View(model: name);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // the model instance is being passed to the view:
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
