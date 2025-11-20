using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Areas.ProjectManagement.Models;

namespace COMP2139_ICE.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("ProjectManagement/Project")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Index
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        // GET: Search
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string term)
        {
            var results = await _context.Projects
                .Where(p => p.Name.Contains(term) || p.Description.Contains(term))
                .ToListAsync();

            ViewBag.SearchTerm = term;
            ViewBag.Searched = true;

            return View("Index", results);
        }

        // GET: Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (!ModelState.IsValid)
                return View(project);

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Edit
        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            return View(project);
        }

        // POST: Edit
        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.ProjectId) return NotFound();
            if (!ModelState.IsValid) return View(project);

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Details
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectTasks)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null) return NotFound();
            return View(project);
        }

        // GET: Delete
        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            return View(project);
        }

        // POST: Delete
        [HttpPost("Delete/{id:int}")]
        [ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
