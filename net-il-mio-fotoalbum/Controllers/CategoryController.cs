using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

            if (foto is null)
            {
                return View("NotFound");
            }
            var formModel = new FotoFormModel
            {
                Foto = foto,
                Categories = _context.Categories.ToArray().Select(i => new SelectListItem(
                    i.Name,
                    i.Id.ToString(),
                    foto.Categories!.Any(_i => _i.Id == i.Id))
                ).ToArray()
            };

            formModel.CategoriesSelezionati = formModel.Categories.Where(i => i.Selected).Select(i => i.Value).ToList();

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, FotoFormModel form)
        {
            if (!ModelState.IsValid)
            {
                form.Categories = _context.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();


                return View(form);
            }
            var savedFoto = _context.Fotos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

            if (savedFoto is null)
            {
                return View("NotFound");
            }
            savedFoto.Titolo = form.Foto.Titolo;
            savedFoto.Description = form.Foto.Description;
            savedFoto.Url = form.Foto.Url;
            savedFoto.Visibile = form.Foto.Visibile;
            savedFoto.Categories = form.CategoriesSelezionati.Select(st => _context.Categories.First(t => t.Id == Convert.ToInt32(st))).ToList();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
