using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    public class Messaggio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Per piacere inserire la propria mail.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Per piacere inserire un messaggio.")]
        public string Testo { get; set; }= string.Empty;

    }
}
