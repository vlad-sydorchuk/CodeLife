using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeLife.Data;
using CodeLife.Model;

namespace CodeLife.Controllers
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
            var statusList = Enum.GetValues(typeof(ProjectTaskStatus))
                .Cast<ProjectTaskStatus>()
                .Select(x => new ProjectTaskStatusAccess
                {
                    Id = ((int)x),
                    Name = x.ToString()
                });
            ViewBag.StatusList = statusList;

            var applicationDbContext = _context.Tasks.Include(p => p.Developer).Include(p => p.Project).Include(p => p.Reviwer).Include(p => p.Tester);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.Tasks
                .Include(p => p.Developer)
                .Include(p => p.Project)
                .Include(p => p.Reviwer)
                .Include(p => p.Tester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            ViewData["DeveloperId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            ViewData["ReviwerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TesterId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descritption,Deadline,ProjectId,Price,DeveloperId,TesterId,ReviwerId,Status")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperId"] = new SelectList(_context.Users, "Id", "Id", projectTask.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectTask.ProjectId);
            ViewData["ReviwerId"] = new SelectList(_context.Users, "Id", "Id", projectTask.ReviwerId);
            ViewData["TesterId"] = new SelectList(_context.Users, "Id", "Id", projectTask.TesterId);
            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.Tasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }
            ViewData["DeveloperId"] = new SelectList(_context.Users, "Id", "Id", projectTask.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectTask.ProjectId);
            ViewData["ReviwerId"] = new SelectList(_context.Users, "Id", "Id", projectTask.ReviwerId);
            ViewData["TesterId"] = new SelectList(_context.Users, "Id", "Id", projectTask.TesterId);
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descritption,Deadline,ProjectId,Price,DeveloperId,TesterId,ReviwerId,Status")] ProjectTask projectTask)
        {
            if (id != projectTask.Id)
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
                    if (!ProjectTaskExists(projectTask.Id))
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
            ViewData["DeveloperId"] = new SelectList(_context.Users, "Id", "Id", projectTask.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectTask.ProjectId);
            ViewData["ReviwerId"] = new SelectList(_context.Users, "Id", "Id", projectTask.ReviwerId);
            ViewData["TesterId"] = new SelectList(_context.Users, "Id", "Id", projectTask.TesterId);
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.Tasks
                .Include(p => p.Developer)
                .Include(p => p.Project)
                .Include(p => p.Reviwer)
                .Include(p => p.Tester)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var projectTask = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(projectTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
