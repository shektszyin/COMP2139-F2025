using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Areas.ProjectManagement.Models;

[Area("ProjectManagement")]
[Route("ProjectManagement/ProjectTask")]
public class ProjectTaskController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectTaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Index
    [HttpGet("Index")]
    public async Task<IActionResult> Index(int projectId)
    {
        var tasks = await _context.ProjectTasks
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();

        ViewBag.ProjectId = projectId;
        return View(tasks);
    }

    // GET: Details
    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var task = await _context.ProjectTasks.FindAsync(id);
        if (task == null) return NotFound();

        return View(task);
    }

    // GET: Create
    [HttpGet("Create")]
    public IActionResult Create(int projectId)
    {
        ViewBag.ProjectId = projectId;
        return View();
    }

    // POST: Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int projectId, ProjectTask task)
    {
        task.ProjectId = projectId;

        if (!ModelState.IsValid)
        {
            ViewBag.ProjectId = projectId;
            return View(task);
        }

        await _context.ProjectTasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { projectId });
    }

    // GET: Edit
    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _context.ProjectTasks.FindAsync(id);
        if (task == null) return NotFound();

        return View(task);
    }

    // POST: Edit
    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProjectTask task)
    {
        if (!ModelState.IsValid) return View(task);

        _context.ProjectTasks.Update(task);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { projectId = task.ProjectId });
    }

    // GET: Delete
    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.ProjectTasks.FindAsync(id);
        if (task == null) return NotFound();

        return View(task);
    }

    // POST: Delete
    [HttpPost("Delete/{id:int}")]
    [ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var task = await _context.ProjectTasks.FindAsync(id);
        if (task == null) return NotFound();

        int projectId = task.ProjectId;

        _context.ProjectTasks.Remove(task);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { projectId });
    }
}
