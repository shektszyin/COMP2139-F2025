using COMP2139_ICE.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: /ProjectManagement/Project
        [HttpGet("")]
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        // GET: /ProjectManagement/Project/Search?term=abc
        [HttpGet("Search")]
        public IActionResult Search(string term)
        {
            var results = _context.Projects
                .Where(p => p.Name.Contains(term) || p.Description.Contains(term))
                .ToList();

            ViewBag.SearchTerm = term;
            ViewBag.Searched = true;

            return View("Index", results);
        }

        // GET: /ProjectManagement/Project/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ProjectManagement/Project/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();

                return RedirectToAction("Index", new { area = "ProjectManagement" });
            }

            return View(project);
        }

        // GET: /ProjectManagement/Project/Edit/5
        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        // POST: /ProjectManagement/Project/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Project project)
        {
            if (id != project.ProjectId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Projects.Update(project);
                _context.SaveChanges();

                return RedirectToAction("Index", new { area = "ProjectManagement" });
            }

            return View(project);
        }

        // GET: /ProjectManagement/Project/Details/5
        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var project = _context.Projects
                .Include(p => p.ProjectTasks)
                .FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        // GET: /ProjectManagement/Project/Delete/5
        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        // POST: /ProjectManagement/Project/Delete/5
        [HttpPost("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return RedirectToAction("Index", new { area = "ProjectManagement" });
        }
    }
}
