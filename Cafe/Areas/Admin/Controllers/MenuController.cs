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
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _he;   

        public MenuController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Admin/Menu
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Menu.Include(m => m.Catagory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Menu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .Include(m => m.Catagory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/Menu/Create
        public IActionResult Create()
        {
            ViewData["CatagoryId"] = new SelectList(_context.Catagory, "Id", "Id");
            return View();
        }

        // POST: Admin/Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Menu menu)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count>0)
            {
                var fileName=Guid.NewGuid().ToString();
                var upload = Path.Combine(_he.WebRootPath, @"Site\Menu");
                var ext=Path.GetExtension(files[0].FileName);
                if (menu.Image != null)
                {
                    var imgPath=Path.Combine(_he.WebRootPath,menu.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                }
                using (var filesStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                {
                    files[0].CopyTo(filesStreams);
                }
            menu.Image = @"\Site\Menu\" + fileName + ext;
            }
           
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           
            return View(menu);
        }

        // GET: Admin/Menu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["CatagoryId"] = new SelectList(_context.Catagory, "Id", "Id", menu.CatagoryId);
            return View(menu);
        }

        // POST: Admin/Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(_he.WebRootPath, @"Site\Menu");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (menu.Image != null)
                    {
                        var imgPath = Path.Combine(_he.WebRootPath, menu.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imgPath))
                        {
                            System.IO.File.Delete(imgPath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    menu.Image = @"\Site\Menu\" + fileName + ext;
                }
                _context.Update(menu);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }
       

        // GET: Admin/Menu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .Include(m => m.Catagory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menu == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Menu'  is null.");
            }
            var menu = await _context.Menu.FindAsync(id);
            var imgPath = Path.Combine(_he.WebRootPath, menu.Image.TrimStart('\\'));
            if (System.IO.File.Exists(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }
            if (menu != null)
            {
                _context.Menu.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return (_context.Menu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
