using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pbl1.Models;

namespace pbl1.Controllers
{
    public class FurnituresController : Controller
    {
        private readonly Pbl1Context _context;

        public FurnituresController(Pbl1Context context)
        {
            _context = context;
        }

        // GET: Furnitures
        public async Task<IActionResult> Index()
        {
            var pbl1Context = _context.Furniture.Include(f => f.Employee).Include(f => f.User);
            return View(await pbl1Context.ToListAsync());
        }

        // GET: Furnitures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Furniture == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furniture
                .Include(f => f.Employee)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FurnitureId == id);
            if (furniture == null)
            {
                return NotFound();
            }

            return View(furniture);
        }

        // GET: Furnitures/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "UserId", "Name");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name");
            return View();
        }

        // POST: Furnitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FurnitureId,Name,Description,Material,Color,Photo,UserId")] Furniture furniture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furniture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "UserId", "Name", furniture.EmployeeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", furniture.UserId);
            return View(furniture);
        }

        // GET: Furnitures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Furniture == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furniture.FindAsync(id);
            if (furniture == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "UserId", "Name", furniture.EmployeeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", furniture.UserId);
            return View(furniture);
        }

        // POST: Furnitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FurnitureId,Name,Description,Material,Color,Photo,StatusFurniture,UserId,EmployeeId")] Furniture furniture)
        {
            if (id != furniture.FurnitureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furniture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnitureExists(furniture.FurnitureId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "UserId", "Name", furniture.EmployeeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", furniture.UserId);
            return View(furniture);
        }

        // GET: Furnitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Furniture == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furniture
                .Include(f => f.Employee)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FurnitureId == id);
            if (furniture == null)
            {
                return NotFound();
            }

            return View(furniture);
        }

        // POST: Furnitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Furniture == null)
            {
                return Problem("Entity set 'Pbl1Context.Furniture'  is null.");
            }
            var furniture = await _context.Furniture.FindAsync(id);
            if (furniture != null)
            {
                _context.Furniture.Remove(furniture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnitureExists(int id)
        {
          return (_context.Furniture?.Any(e => e.FurnitureId == id)).GetValueOrDefault();
        }
    }
}
