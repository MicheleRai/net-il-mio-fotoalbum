using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
	public class FotoFormModel
	{
		public Foto Foto { get; set; } = new Foto { Url = "https://picsum.photos/200/300" };
		public IEnumerable<SelectListItem>? Categories { get; set; } = Enumerable.Empty<SelectListItem>();
		public List<string> CategoriesSelezionati { get; set; } = new();
	}
}
