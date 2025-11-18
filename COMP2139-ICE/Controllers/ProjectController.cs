using COMP2139_ICE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_ICE.Controllers;

[Route("projects")]
public class ProjectController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("search")]
    public IActionResult Search(string term)
    {
        var results = _context.Projects
            .Where(p => p.Name.Contains(term) || p.Description.Contains(term))
            .ToList();

        ViewBag.SearchTerm = term;
        ViewBag.Searched = true;

        return View("Index", results);
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var projects = _context.Projects.ToList();
        return View(projects);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Project project)
    {
        if (ModelState.IsValid)
        {
            project.StartDate = DateTime.SpecifyKind(project.StartDate, DateTimeKind.Utc);
            project.EndDate = DateTime.SpecifyKind(project.EndDate, DateTimeKind.Utc);

            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(project);
    }

    [HttpGet("edit/{id:int:min(1)}")]
    public IActionResult Edit(int id)
    {
        var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
        if (project == null) return NotFound();
        return View(project);
    }

    [HttpPost("edit/{id:int:min(1)}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Project project)
    {
        if (id != project.ProjectId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Projects.Update(project);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(e => e.ProjectId == id)) return NotFound();
                throw;
            }

            return RedirectToAction("Index");
        }

        return View(project);
    }

    [HttpGet("details/{id:int:min(1)}")]
    public IActionResult Details(int id)
    {
        var project = _context.Projects
            .Include(p => p.ProjectTasks)
            .FirstOrDefault(p => p.ProjectId == id);

        if (project == null) return NotFound();
        return View(project);
    }

    [HttpGet("delete/{id:int:min(1)}")]
    public IActionResult Delete(int id)
    {
        var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
        if (project == null) return NotFound();
        return View(project);
    }

    [HttpPost("delete/{id:int:min(1)}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var project = _context.Projects.Find(id);
        if (project == null) return NotFound();

        _context.Projects.Remove(project);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
