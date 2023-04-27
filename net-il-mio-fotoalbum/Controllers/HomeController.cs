using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Diagnostics;

namespace net_il_mio_fotoalbum.Controllers
{
    
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly AlbumContext _context;

		public HomeController(ILogger<HomeController> logger, AlbumContext context)
		{
			_logger = logger;
			_context = context;
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
		{
			var fotos = _context.Fotos.ToArray();

			return View(fotos);
		}
        public IActionResult ApiIndex()
        {


            return View();
        }
        public IActionResult ApiDettagli()
        {


            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Dettagli(int id)
		{
			var foto = _context.Fotos.Include(p =>p.Categories).FirstOrDefault(p => p.Id == id);

			if (foto is null)
			{
				return NotFound($"Pizza with id {id} not found.");
			}

			return View(foto);
		}
        [Authorize(Roles = "Admin")]
        [HttpGet]
		public IActionResult Create()
		{
			var formModel = new FotoFormModel()
			{
				Categories = _context.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray(),
			};

			return View(formModel);
		}
        [Authorize(Roles = "Admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(FotoFormModel form)
		{
			if (!ModelState.IsValid)
			{
				form.Categories = _context.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();

				return View(form);
			}

			form.Foto.Categories = form.CategoriesSelezionati.Select(st => _context.Categories.First(t => t.Id == Convert.ToInt32(st))).ToList();

			_context.Fotos.Add(form.Foto);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
		{
			var foto = _context.Fotos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			var FotosDaCancellare = _context.Fotos.FirstOrDefault(p => p.Id == id);

			if (FotosDaCancellare is null)
			{
				return View("NotFound");
			}

			_context.Fotos.Remove(FotosDaCancellare);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}