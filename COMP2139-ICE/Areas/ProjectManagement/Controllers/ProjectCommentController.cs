using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Areas.ProjectManagement.Models;

namespace COMP2139_ICE.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("ProjectManagement/[controller]")]
    [ApiController]   
    public class ProjectCommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectManagement/ProjectComment/GetComments/5
        [HttpGet("GetComments/{projectId:int}")]
        public async Task<IActionResult> GetComments(int projectId)
        {
            var comments = await _context.ProjectComments
                .Where(c => c.ProjectId == projectId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            return Ok(comments);
        }

        // POST: ProjectManagement/ProjectComment/AddComment
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] ProjectComment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            comment.CreatedDate = DateTime.UtcNow;

            await _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
