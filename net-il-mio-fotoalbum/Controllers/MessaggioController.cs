﻿using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class MessaggioController : Controller
    {
        private readonly AlbumContext _context;
        public MessaggioController(AlbumContext context) 
        {
            _context = context;
        }
        public IActionResult MessaggiIndex()
        {
            var messaggi = _context.Messagi.ToArray();
            return View(messaggi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var DeleteMessaggio = _context.Messagi.FirstOrDefault(c => c.Id == id);
            if (DeleteMessaggio == null)
            {
                return NotFound();
            }
            _context.Messagi.Remove(DeleteMessaggio);
            _context.SaveChanges();
            return RedirectToAction("MessaggiIndex");
        }

    }
}