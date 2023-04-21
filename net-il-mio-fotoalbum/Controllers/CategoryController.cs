using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AlbumContext _context;

        public CategoryController(AlbumContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToArray();
            return View(categories);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var categoryDelete = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryDelete == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(categoryDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
