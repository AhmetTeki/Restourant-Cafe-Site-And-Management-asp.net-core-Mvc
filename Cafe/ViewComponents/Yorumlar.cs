using Cafe.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.ViewComponents
{
	public class Yorumlar: ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public Yorumlar(ApplicationDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			var yorum= _context.Blog.ToList();
			return View(yorum);
		}
	}
}
