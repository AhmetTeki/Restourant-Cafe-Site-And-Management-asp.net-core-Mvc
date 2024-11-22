using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cafe.Data;
using Cafe.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RezervasyonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervasyonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Rezervasyons
        public async Task<IActionResult> Index()
        {
              return _context.Rezervasyon != null ? 
                          View(await _context.Rezervasyon.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Rezervasyon'  is null.");
        }

        // GET: Admin/Rezervasyons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rezervasyon == null)
            {
                return NotFound();
            }

            var rezervasyon = await _context.Rezervasyon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervasyon == null)
            {
                return NotFound();
            }

            return View(rezervasyon);
        }

        // GET: Admin/Rezervasyons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Rezervasyons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,TelefonNo,Sayı,Saat,Tarih")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervasyon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezervasyon);
        }

        // GET: Admin/Rezervasyons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rezervasyon == null)
            {
                return NotFound();
            }

            var rezervasyon = await _context.Rezervasyon.FindAsync(id);
            if (rezervasyon == null)
            {
                return NotFound();
            }
            return View(rezervasyon);
        }

        // POST: Admin/Rezervasyons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,TelefonNo,Sayı,Saat,Tarih")] Rezervasyon rezervasyon)
        {
            if (id != rezervasyon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervasyon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervasyonExists(rezervasyon.Id))
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
            return View(rezervasyon);
        }

        // GET: Admin/Rezervasyons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rezervasyon == null)
            {
                return NotFound();
            }

            var rezervasyon = await _context.Rezervasyon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervasyon == null)
            {
                return NotFound();
            }

            return View(rezervasyon);
        }

        // POST: Admin/Rezervasyons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rezervasyon == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rezervasyon'  is null.");
            }
            var rezervasyon = await _context.Rezervasyon.FindAsync(id);
            if (rezervasyon != null)
            {
                _context.Rezervasyon.Remove(rezervasyon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervasyonExists(int id)
        {
          return (_context.Rezervasyon?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
