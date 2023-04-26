using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);;

            if (category is null)
            {
                return View("NotFound");
            }
            var formModel = new CategoryFormModel
            {
                Category = category,
                Categories = _context.Categories.ToArray().Select(i => new SelectListItem(
                    i.Name,
                    i.Id.ToString()))
            };

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, CategoryFormModel form)
        {
            
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var catToUpdate = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (catToUpdate == null)
            {
                return NotFound();
            }

            catToUpdate.Name = form.Category.Name;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update(int id, CategoryFormModel form)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        form.Categories = _context.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();


        //        return View(form);
        //    }
        //    var savedCategory = _context.Categories;

        //    if (savedCategory is null)
        //    {
        //        return View("NotFound");
        //    }
        //    savedCategory.Name = form.Category.Name;
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}
    }
}
