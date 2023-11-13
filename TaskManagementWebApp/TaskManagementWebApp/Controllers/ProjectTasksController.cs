using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementWebApp.Data;
using TaskManagementWebApp.Models;

namespace TaskManagementWebApp.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProjectTasksController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
              return _context.ProjectTasks != null ? 
                          View(await _context.ProjectTasks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProjectTasks'  is null.");
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectTasks == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks.Include(t => t.AssignedUser).FirstOrDefaultAsync(m => m.ProjectTaskId == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(ProjectTask.TaskStatus)));
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectTaskId,Title,Description,DueDate,Status")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

        var projectTask = await _context.ProjectTasks
        .Include(t => t.AssignedUser) // Access current AssignedUserId details
        .FirstOrDefaultAsync(m => m.ProjectTaskId == id);

            if (projectTask == null)
            {
                return NotFound();
            }
            ViewBag.Users = new SelectList(_userManager.Users.ToList(), "Id", "UserName", projectTask.AssignedUserId);
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(ProjectTask.TaskStatus)));

            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId,Title,Description,DueDate, AssignedUserId, Status")] ProjectTask projectTask)
        {
            if (id != projectTask.ProjectTaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(projectTask.ProjectTaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Users = new SelectList(_userManager.Users.ToList(), "Id", "UserName", projectTask.AssignedUserId);
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(ProjectTask.TaskStatus)), projectTask.Status);
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectTasks == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks
                .FirstOrDefaultAsync(m => m.ProjectTaskId == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectTasks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProjectTasks'  is null.");
            }
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask != null)
            {
                _context.ProjectTasks.Remove(projectTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskExists(int id)
        {
          return (_context.ProjectTasks?.Any(e => e.ProjectTaskId == id)).GetValueOrDefault();
        }
    }
}
