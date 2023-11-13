using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementWebApp.Models;
using TaskManagementWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectTasksApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TasksApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks()
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        // POST: api/ProjectTasksApi
        [HttpPost]
        public async Task<ActionResult<ProjectTask>> PostTask(ProjectTask projectTask)
        {
            _context.ProjectTasks.Add(projectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = projectTask.ProjectTaskId }, projectTask);
        }

        // GET by ID: api/ProjectTasksApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetTask(int id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);

            if (projectTask == null)
            {
                return NotFound();
            }

            return projectTask;
        }

        // PUT: api/ProjectTasksApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, ProjectTask projectTask)
        {
            if (id != projectTask.ProjectTaskId)
            {
                return BadRequest();
            }

            _context.Entry(projectTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.ProjectTaskId == id);
        }

        // DELETE: api/ProjectTasksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            _context.ProjectTasks.Remove(projectTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
