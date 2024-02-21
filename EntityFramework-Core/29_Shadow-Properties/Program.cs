using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region ShadowProperty - Gölge Özellik
//Entity sınıflarında fiziksel olarak tanımlanmayan, modellenmeyen ancak EF Core tarafından ilgili entity için var olan
//ya da var olduğu kabul edilen propertylerdir.

//Tabloda gösterilmesini istemediğimiz, entity instance'ı üzerinde işlem yapılmayacak olan kolonlar için kullanılır.

#endregion

#region Foreign Key - Shadow Property
//İlişkisel senaryolarda foreign key property'si tanımlanmadığı halde, EF Core dependent entity'e bunu ekler. Bu shadow property'dir.

//var blogs = context.Blogs.Include(b => b.Posts).ToList(); // Burada gelen verilerin sıralanması shadow property ile yapılıyor. (BlogId)
#endregion

#region Shadow Property Oluşturma
//Bir entity üzerinde shadow property oluşturmak isteniyorsa, fluent API kullanılmalıdır.

/*
 modelBuilder.Entity<Blog>()
    .Property<DateTime>("CreatedDate"); 
*/
#endregion

#region Shadow Property Kullanımı

#region ChangeTracker ile Erişim
//Shadow Property'e erişim sağlayabilmek için ChangeTracker kullanılabilir.

/*
var blog = await context.Blogs.FirstAsync();

var createdDate = context.Entry(blog).Property("CreatedDate");
Console.WriteLine(createdDate.CurrentValue);
Console.WriteLine(createdDate.OriginalValue);

createdDate.CurrentValue = DateTime.Now;
await context.SaveChangesAsync();
*/
#endregion

#region EF.Property ile Erişim
//Özellikle LINQ sorgularında shadow property'lerin kullanımı için EF.Property metodu kullanılabilir.

/*
var blogs = await context.Blogs.OrderBy(b => EF.Property<DateTime>(b,"CreatedDate")).ToListAsync();
var blogs2 = await context.Blogs.Where(b=> EF.Property<DateTime>(b, "CreatedDate").Year > 2022).OrderBy(b => EF.Property<DateTime>(b,"CreatedDate")).ToListAsync();
*/
#endregion

#endregion
class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool lastUpdated { get; set; }

    public Blog Blog { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ApplicationDb;Trusted_Connection=True;");

    }

    //Shadow Property Oluşturma
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
               .Property<DateTime>("CreatedDate");


    }
}