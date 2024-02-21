using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region Primary Key Constraint 
// Key annotation 
// HasKey fonksiyonu Fluent API
#endregion

#region Alternate Keys 
//Primary Key'e ek olarak ekstra bir key tanımlamak için kullanılır. (Unique Constraint)
#endregion

#region Foreign Key Constraint
#endregion

#region Unique Constraint 
#region HasIndex - IsUnique Fonksiyonları

#endregion

#region Index Annotation

#endregion
#endregion

#region Check Constraint

#region HasCheck Constraint

#endregion

#endregion



[Index(nameof(Blog.Url), IsUnique = true)] //Unique Constraint
class Blog
{
	public int Id { get; set; }
	public string BlogName { get; set; }
	public string Url { get; set; }

	public ICollection<Post> Posts { get; set; }
}
class Post
{
	public int Id { get; set; }
	//public int BlogId { get; set; }
	public string Title { get; set; }
	public string BlogUrl { get; set; }
	public int A { get; set; }
	public int B { get; set; }

	public Blog Blog { get; set; }
}


class ApplicationDbContext : DbContext
{
	public DbSet<Blog> Blogs { get; set; }
	public DbSet<Post> Posts { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//PK
		/*
		modelBuilder.Entity<Blog>()
			.HasKey(b => b.Id);

		modelBuilder.Entity<Blog>()
			.HasKey(b => b.Id)
			.HasName("PK_BlogId"); //PK'ya isim vermek için kullanılır.
		*/

		//Alternate Key
		/*
		modelBuilder.Entity<Blog>()
			.HasAlternateKey(b => b.BlogName); //BlogName'i unique constraint olarak ayarlamış olur.			

		modelBuilder.Entity<Blog>()
			.HasAlternateKey(b => new {b.Url,b.BlogName}); // Composite Alternate Key
		*/

		//Foreign Key
		/*
		modelBuilder.Entity<Blog>()
			.HasMany(b => b.Posts)
			.WithOne(p => p.Blog)
			.HasForeignKey(p => p.Id) //Foreign Key olarak BlogUrl kullanılacak.
			.OnDelete(DeleteBehavior.SetNull); //Cascade, Restrict, SetNull, NoAction

		//Composite Foreign Key
		modelBuilder.Entity<Blog>()
			.HasMany(b => b.Posts)
			.WithOne(p => p.Blog)
			.HasForeignKey(p => new { p.Id, p.BlogUrl });

		//Shadow Foreign Key
		modelBuilder.Entity<Blog>()
			.HasMany(b => b.Posts)
			.WithOne(p => p.Blog)
			.HasForeignKey("BlogIdFK") //Shadow Property
		*/

		//Unique Constraint
		/*
		modelBuilder.Entity<Blog>()
			.HasIndex(b => b.Url)
			.IsUnique();

		modelBuilder.Entity<Blog>()
			.HasAlternateKey(b => b.Url);
		*/

		//Check Constraint

		modelBuilder.Entity<Post>()
			.HasCheckConstraint("CK_A", "A > 0")
			.HasCheckConstraint("CK_B", "B > 0");
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ConstraintsDb;Trusted_Connection=True;");
	}
}