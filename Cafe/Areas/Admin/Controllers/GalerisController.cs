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
    public class GalerisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _he;

        public GalerisController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Admin/Galeris
        public async Task<IActionResult> Index()
        {
              return _context.Galeri != null ? 
                          View(await _context.Galeri.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Galeri'  is null.");
        }

        // GET: Admin/Galeris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Galeri == null)
            {
                return NotFound();
            }

            var galeri = await _context.Galeri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galeri == null)
            {
                return NotFound();
            }

            return View(galeri);
        }

        // GET: Admin/Galeris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Galeris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Galeri galeri)
        {
            
            
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(_he.WebRootPath, @"Site\Menu");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (galeri.Image != null)
                    {
                        var imgPath = Path.Combine(_he.WebRootPath, galeri.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imgPath))
                        {
                            System.IO.File.Delete(imgPath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    galeri.Image = @"\Site\Menu\" + fileName + ext;
                }
                _context.Add(galeri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(galeri);
        }

        // GET: Admin/Galeris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galeri == null)
            {
                return NotFound();
            }

            var galeri = await _context.Galeri.FindAsync(id);
            if (galeri == null)
            {
                return NotFound();
            }
            return View(galeri);
        }

        // POST: Admin/Galeris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] Galeri galeri)
        {
            if (id != galeri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galeri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaleriExists(galeri.Id))
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
            return View(galeri);
        }

        // GET: Admin/Galeris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galeri == null)
            {
                return NotFound();
            }

            var galeri = await _context.Galeri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galeri == null)
            {
                return NotFound();
            }

            return View(galeri);
        }

        // POST: Admin/Galeris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galeri == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Galeri'  is null.");
            }
            var galeri = await _context.Galeri.FindAsync(id);
            if (galeri != null)
            {
                _context.Galeri.Remove(galeri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaleriExists(int id)
        {
          return (_context.Galeri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
