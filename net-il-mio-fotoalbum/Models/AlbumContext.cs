using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
	public class AlbumContext : IdentityDbContext<IdentityUser>
	{
		public AlbumContext(DbContextOptions<AlbumContext> options) : base(options) { }

		public DbSet<Foto> Fotos { get; set; }
		public DbSet<Category> Categories { get; set; }

		public void Seed() 
		{
			var fotos = new Foto[]
			{
				new Foto
				{
					Titolo = "Foto mare",
					Description ="Foto che ritrae il mare",
					Url = "https://www.touringclub.it/sites/default/files/styles/gallery_full/public/immagini_georiferite/gettyimages-649067370.jpg?itok=BgjbldQE",
					Visibile = true,
				},
				new Foto
				{
					Titolo = "Foto montagna",
					Description ="Foto che ritrae la montagna",
					Url = "https://wips.plug.it/cips/initalia.virgilio.it/cms/2020/12/montagna-inverno-valfiscalina.jpg",
					Visibile = true,
				},
				new Foto
				{
					Titolo = "Foto foresta",
					Description ="Foto che ritrae la foresta",
					Url = "https://siviaggia.it/wp-content/uploads/sites/2/2020/02/foresta-di-sherwood-3.jpg",
					Visibile = true,
				},
				new Foto
				{
					Titolo = "Foto cane",
					Description ="Foto che ritrae un cane",
					Url = "https://ecuphar.it/assets/images/ecu_interne/news_procanicare_1000x600.jpg",
					Visibile = true,
				},

			};
			if (!Fotos.Any()) 
			{
				Fotos.AddRange(fotos);
			}
			if (!Categories.Any()) 
			{
				var seed = new Category[]
				{
					new()
					{
						Name = "Paesaggi"
					},
					new()
					{
						Name = "Persone"
					},
					new()
					{
						Name = "Animali"
					},
					new()
					{
						Name = "Generale",
						Fotos = fotos
					},
				};
				Categories.AddRange(seed);
			}

            if (!Roles.Any())
            {
                var seed = new IdentityRole[]
                {
                    new("Admin")
                };

                Roles.AddRange(seed);
            }

            if (Users.Any(u => u.Email == "admin@dev.com")
                && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@dev.com");

                var adminRole = Roles.First(r => r.Name == "Admin");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                        UserId = admin.Id,
                        RoleId = adminRole.Id
                    }
                };

                UserRoles.AddRange(seed);
            }



            SaveChanges();
		}

	}
}
