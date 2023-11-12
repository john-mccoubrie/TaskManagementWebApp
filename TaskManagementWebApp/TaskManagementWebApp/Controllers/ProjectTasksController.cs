using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementWebApp.Data;
using TaskManagementWebApp.Models;

namespace TaskManagementWebApp.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectTasksController(ApplicationDbContext context)
        {
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

            var projectTask = await _context.ProjectTasks
                .FirstOrDefaultAsync(m => m.ProjectTaskId == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectTaskId,Title,Description,DueDate")] ProjectTask projectTask)
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
            if (id == null || _context.ProjectTasks == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId,Title,Description,DueDate")] ProjectTask projectTask)
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
