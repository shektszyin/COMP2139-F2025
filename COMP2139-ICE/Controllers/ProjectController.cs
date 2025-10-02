using COMP2139_ICE.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2139_ICE.Controllers;

public class ProjectController : Controller

{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }
        
        
    [HttpGet]
    public IActionResult Index()
    {
       // var projects = new List<Project>()
        //{
           // new Project {ProjectId = 1, Name = "Project 1", Description = "First Project"}
        //};

        var projects = _context.Projects.ToList();
        if (projects == null)
        {
            return NotFound();
        }
        return View(projects);
    }



    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(Project project)

    {
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult Details(int id)

    {
        var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
        //var project = new Project { ProjectId = id, Name = "Project " + id, Description = "Detail of project " + id };
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }


}