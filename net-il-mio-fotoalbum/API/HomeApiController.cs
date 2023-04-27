using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly AlbumContext _context;

        public HomeApiController(AlbumContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetFoto([FromQuery] string? titolo)
        {
            var fotos = _context.Fotos
               .Where(p => titolo == null || p.Titolo.ToLower().Contains(titolo.ToLower()))
               .ToList();


            return Ok(fotos);
            
        }
        [HttpGet("{id}")]
        public IActionResult GetFoto(int id)
        {
            var foto = _context.Fotos.FirstOrDefault(p => p.Id == id);

            if (foto is null)
            {
                return NotFound();
            }

            return Ok(foto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMessage(Messaggio messagio)
        {
            _context.Messagi.Add(messagio);
            _context.SaveChanges();

            return Ok(messagio);
        }
    }
}
