using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
	public class Foto
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Per piacere fornire un nome per la pizza.")]
		[StringLength(100, ErrorMessage = "Il nome deve contenere meno di 100 characters.")]
		public string Titolo { get; set; } = string.Empty;

		[Required(ErrorMessage = "Per piacere fornire una descrizione per la Pizza.")]
		[Column(TypeName = "text")]
		public string Description { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public bool Visibile { get; set; }
		public List<Category>? Categories { get; set; }
	}
}
