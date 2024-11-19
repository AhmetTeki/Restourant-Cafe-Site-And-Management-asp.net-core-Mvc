using Cafe.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.ViewComponents
{
    public class CategoryList: ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoryList(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var category= _context.Catagory.ToList();
            return View(category);
        }
    }
}
