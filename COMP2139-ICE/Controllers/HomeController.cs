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

    public IActionResult Search(string term)
    {
        var projects = _context.Projects
            .Where(p => p.Name.Contains(term) || p.Description.Contains(term))
            .ToList();

        var tasks = _context.ProjectTasks
            .Where(t => t.Title.Contains(term) || t.Description.Contains(term))
            .ToList();

        ViewBag.Term = term;
        ViewBag.Projects = projects;
        ViewBag.Tasks = tasks;

        return View();
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
}
