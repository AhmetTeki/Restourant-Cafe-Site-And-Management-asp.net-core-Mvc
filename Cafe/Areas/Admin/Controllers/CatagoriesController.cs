using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cafe.Data;
using Cafe.Models;

namespace Cafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatagoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatagoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Catagories
        public async Task<IActionResult> Index()
        {
              return _context.Catagory != null ? 
                          View(await _context.Catagory.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Catagory'  is null.");
        }

        // GET: Admin/Catagories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Catagory == null)
            {
                return NotFound();
            }

            var catagory = await _context.Catagory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catagory == null)
            {
                return NotFound();
            }

            return View(catagory);
        }

        // GET: Admin/Catagories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Catagories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Catagory catagory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catagory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catagory);
        }

        // GET: Admin/Catagories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Catagory == null)
            {
                return NotFound();
            }

            var catagory = await _context.Catagory.FindAsync(id);
            if (catagory == null)
            {
                return NotFound();
            }
            return View(catagory);
        }

        // POST: Admin/Catagories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Catagory catagory)
        {
            if (id != catagory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catagory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatagoryExists(catagory.Id))
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
            return View(catagory);
        }

        // GET: Admin/Catagories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Catagory == null)
            {
                return NotFound();
            }

            var catagory = await _context.Catagory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catagory == null)
            {
                return NotFound();
            }

            return View(catagory);
        }

        // POST: Admin/Catagories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Catagory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Catagory'  is null.");
            }
            var catagory = await _context.Catagory.FindAsync(id);
            if (catagory != null)
            {
                _context.Catagory.Remove(catagory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatagoryExists(int id)
        {
          return (_context.Catagory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
