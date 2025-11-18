using Microsoft.AspNetCore.Mvc;
using COMP2139_ICE.Models;

[Route("projects/{projectId:int:min(1)}/tasks")]
public class ProjectTaskController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectTaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("search")]
    public IActionResult Search(int projectId, [FromQuery] string term)
    {
        bool searched = true;

        var results = _context.ProjectTasks
            .Where(t => t.ProjectId == projectId &&
                       (t.Title.Contains(term) || t.Description.Contains(term)))
            .ToList();

        ViewBag.SearchTerm = term;
        ViewBag.Searched = searched;
        ViewBag.ProjectId = projectId;

        return View("Index", results);
    }

    [HttpGet]
    public IActionResult Index(int projectId)
    {
        var tasks = _context.ProjectTasks
            .Where(t => t.ProjectId == projectId)
            .ToList();

        ViewBag.ProjectId = projectId;
        return View(tasks);
    }

    [HttpGet("{id:int:min(1)}")]
    public IActionResult Details(int id)
    {
        var task = _context.ProjectTasks.FirstOrDefault(t => t.ProjectTaskId == id);
        if (task == null)
            return NotFound();
        return View(task);
    }

    [HttpGet("create")]
    public IActionResult Create(int projectId)
    {
        ViewBag.ProjectId = projectId;
        return View();
    }

   [HttpPost("create")]
[ValidateAntiForgeryToken]
public IActionResult Create(int projectId, ProjectTask task)
{
    task.ProjectId = projectId;

    if (ModelState.IsValid)
    {
        _context.ProjectTasks.Add(task);
        _context.SaveChanges();

        return RedirectToAction("Details", "Project", new { id = projectId });
    }

    ViewBag.ProjectId = projectId;
    return View(task);
}


    [HttpGet("edit/{id:int:min(1)}")]
    public IActionResult Edit(int id)
    {
        var task = _context.ProjectTasks.Find(id);
        if (task == null)
            return NotFound();
        return View(task);
    }

    [HttpPost("edit/{id:int:min(1)}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProjectTask task)
    {
        if (ModelState.IsValid)
        {
            _context.ProjectTasks.Update(task);
            _context.SaveChanges();
            return RedirectToAction("Details", "Project", new { id = task.ProjectId });
        }

        return View(task);
    }

    [HttpGet("delete/{id:int:min(1)}")]
    public IActionResult Delete(int id)
    {
        var task = _context.ProjectTasks.Find(id);
        if (task == null)
            return NotFound();
        return View(task);
    }

    [HttpPost("delete/{id:int:min(1)}")]
    [ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var task = _context.ProjectTasks.Find(id);
        if (task == null)
            return NotFound();

        int projectId = task.ProjectId;

        _context.ProjectTasks.Remove(task);
        _context.SaveChanges();

        return RedirectToAction("Details", "Project", new { id = projectId });
    }
}
