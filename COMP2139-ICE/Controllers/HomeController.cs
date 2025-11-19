using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using COMP2139_ICE.Models;

namespace COMP2139_ICE.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Search(string type, string term)
{
    if (string.IsNullOrWhiteSpace(term))
        return RedirectToAction("Index");

    if (type == "project")
        return RedirectToAction("Search", "Project", new { term });

    if (type == "task")
        return RedirectToAction("Index", "ProjectTask", new { projectId = 1 });

    return RedirectToAction("Index");
}


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult NotFound(int code)
{
    return View("NotFound");
}

}
