using COMP2139_ICE.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProjectCommentController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectCommentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> LoadComments(int projectId)
    {
        var comments = await _context.ProjectComments
            .Where(c => c.ProjectId == projectId)
            .OrderByDescending(c => c.CreatedDate)
            .ToListAsync();

        return PartialView("~/Views/ProjectComment/_CommentList.cshtml", comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(int projectId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return BadRequest("Comment required.");

        var comment = new ProjectComment
        {
            ProjectId = projectId,
            Content = content,
            CreatedDate = DateTime.UtcNow
        };

        _context.ProjectComments.Add(comment);
        await _context.SaveChangesAsync();

        var comments = await _context.ProjectComments
            .Where(c => c.ProjectId == projectId)
            .OrderByDescending(c => c.CreatedDate)
            .ToListAsync();

        return PartialView("~/Views/ProjectComment/_CommentList.cshtml", comments);
    }
}
