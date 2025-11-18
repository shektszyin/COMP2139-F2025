using Microsoft.AspNetCore.Mvc;
using COMP2139_ICE.Models;

public class ProjectTaskController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectTaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int projectId)
    {
        var tasks = _context.ProjectTasks
            .Where(t => t.ProjectId == projectId)
            .ToList();

        ViewBag.ProjectId = projectId;
        return View(tasks);
    }

 
    public IActionResult Details(int id)
    {
        var task = _context.ProjectTasks.FirstOrDefault(t => t.ProjectTaskId == id);

        if (task == null)
            return NotFound();

        return View(task);
    }


    public IActionResult Create(int projectId)
    {
        ViewBag.ProjectId = projectId;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProjectTask task)
    {
        if (ModelState.IsValid)
        {
            _context.ProjectTasks.Add(task);
            _context.SaveChanges();

            return RedirectToAction("Details", "Project", new { id = task.ProjectId });
        }

        ViewBag.ProjectId = task.ProjectId;
        return View(task);
    }


    public IActionResult Edit(int id)
    {
        var task = _context.ProjectTasks.Find(id);

        if (task == null)
            return NotFound();

        return View(task);
    }

   
    [HttpPost]
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

  
    public IActionResult Delete(int id)
    {
        var task = _context.ProjectTasks.Find(id);

        if (task == null)
            return NotFound();

        return View(task);
    }

 
    [HttpPost, ActionName("DeleteConfirmed")]
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
