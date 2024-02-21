using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

ApplicationDbContext context = new();
Console.WriteLine();
#region Data Seeding Nedir?
//Seed Data özelliği ile EF Core üzerinden migrationlarda veriler oluşturabilir ve bu verileri veritabanına kaydedebiliriz.

//Seed Data'lar, migrate süreçlerinde hazır verileri veritabanına ekleyebilmek için bunları yazılım kısmında tutmamızı gerektirirler.

//Bu sayede veritabanı seviyesinde istenilen manipülasyonlar gerçekleştirilebilmektedir.

//Data Seeding şu noktalarda kullanılabilir:
//Test için geçici verilere ihtiyaç varsa,
//ASP.Net Core'daki Identity yapılanmasındaki roller static olarak tutuluyorsa,
//Yazılım için gerekli konfigürasyonel değerler varsa 
#endregion

#region Seed Data Ekleme
//OnModelCreating metodu içerisinde Entity fonksiyonundan sonra HasData fonksiyonu ile verileri ekleyebiliriz.

//Seed data oluştururken, primary key değerlerinin manuel olarak verilmesi gerekmektedir. Çünkü, ilişkisel verileri de Seed Datalar ile üretebilmek için
//primary key değerlerinin veritabanında olması gerekmektedir.
#endregion

#region Seed Data'nın Primary Key'ini Değiştirme 
//Seed Datalar bir kez migrate edilir ve veritabanına kaydedilir.
#endregion



class Post
{
	public int Id { get; set; }
	public int BlogId { get; set; }
	public string Title { get; set; } 
	public string Content { get; set; } 

	public Blog Blog { get; set; }
}
class Blog
{
	public int Id { get; set; }
	public string Url { get; set; }

	public ICollection<Post> Posts { get; set; }
}
class ApplicationDbContext : DbContext
{
	public DbSet<Post> Posts { get; set; }
	public DbSet<Blog> Blogs { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Blog>()
			.HasData(
				new Blog { Id = 1, Url = "http://sample.com" },
				new Blog { Id = 2, Url = "http://sample2.com" }
			);

		modelBuilder.Entity<Post>()
			.HasData(
				new Post { Id = 1, BlogId = 1, Title = "First post", Content = "Test 1" },
				new Post { Id = 2, BlogId = 2, Title = "Second post", Content = "Test 2" }
			);



	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DataSeeding;Trusted_Connection=True;");
	}
}