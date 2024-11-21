using Cafe.Data;
using Cafe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cafe.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _he;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment he)
        {
            _logger = logger;
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            var menus=_db.Menu.ToList();
            return View(menus);
        }
        public IActionResult CategoryDetay(int id)
        {
            var menu= _db.Menu.Where(i=>i.Catagory.Id==id).ToList();
            ViewBag.KategoriId = id;
            return View(menu);
        }
		// GET: Admin/Contacts/Create
		public IActionResult Contact()
		{
			return View();
		}

		// POST: Admin/Contacts/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Contact( Contact contact)
		{
			if (ModelState.IsValid)
			{
                contact.Tarih=DateTime.Now;
				_db.Add(contact);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(contact);
		}
		//***************************************************************************************************
		public IActionResult Blog()
        {
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blog(Blog blog)
        {
            blog.Tarih=DateTime.Now;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(_he.WebRootPath, @"Site\Menu");
                var ext = Path.GetExtension(files[0].FileName);
                if (blog.Image != null)
                {
                    var imgPath = Path.Combine(_he.WebRootPath, blog.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                }
                using (var filesStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                {
                    files[0].CopyTo(filesStreams);
                }
                blog.Image = @"\Site\Menu\" + fileName + ext;
            }

            _db.Add(blog);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(blog);
        }
        //**************************************************************************************************
        public IActionResult About()
        {
            var about=_db.About.ToList();
            return View(about);
        }
        public IActionResult Galeri()
        {
            var galeri=_db.Galeri.ToList();
            return View(galeri);
        }
        //**************************************************************************************
        public IActionResult Rezervasyon()
        {
            return View();
        }

        // POST: Admin/Rezervasyons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rezervasyon([Bind("Id,Name,Email,TelefonNo,Sayı,Saat,Tarih")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                _db.Add(rezervasyon);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezervasyon);
        }
        //**************************************************************************************
        public IActionResult Menu()
        {
            var menu = _db.Menu.ToList();
            return View(menu);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}