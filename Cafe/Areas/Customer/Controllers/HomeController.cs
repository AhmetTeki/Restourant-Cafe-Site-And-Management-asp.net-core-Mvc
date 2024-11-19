using Cafe.Data;
using Cafe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cafe.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
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
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Galeri()
        {
            return View();
        }
        public IActionResult Rezervasyon()
        {
            return View();
        }
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