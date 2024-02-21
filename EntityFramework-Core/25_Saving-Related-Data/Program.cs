
using Microsoft.EntityFrameworkCore;

Console.WriteLine();
ApplicationDbContext context = new();

#region One to One İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem -> Principal Entity Üzerinden Dependent Entity Verisi Ekleme
/*
Person person = new();
person.Name = "Hakan";
person.Address = new() { PersonAddress = "Odunpazarı/ESKİŞEHİR" };

await context.AddAsync(person);
await context.SaveChangesAsync();
*/
#endregion

//Eğer ki principal entity üzerinden ekleme gerçekleştiriliyorsa dependent entity nesnesi verilmek zorunda değildir!
//Dependent entity üzerinden ekleme işlemi gerçekleştiriliyorsa eğer burada principal entitynin nesnesine ihtiyacımız zaruridir.

#region 2. Yöntem -> Dependent Entity Üzerinden Principal Entity Verisi Ekleme
/*
Address address = new()
{
    PersonAddress = "Manavgat / ANTALYA",
    Person = new() { Name = "Alperen" }
};

await context.AddAsync(address);
await context.SaveChangesAsync();
*/
#endregion

/*
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
}


class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }
    public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ApplicationDb;Trusted_Connection=True;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
    }
}
*/
#endregion

#region One to Many İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem -> Principal Entity Üzerinden Dependent Entity Verisi Ekleme
#region Nesne Referansı Üzerinden Ekleme

/*

Blog blog = new() { Name = "A Blog" };

//Burada blog nesnesinde post nesnesi olmaz ise hata alırız. (NullReferenceException). Çünkü post nesnesi nullable değildir. 
//Bu durumun önüne geçmek için koleksiyonel propertylerin constructorları içerisinde newlenmesi gerekmektedir.

blog.Posts.Add(new() { Title = "Post 1" });
blog.Posts.Add(new() { Title = "Post 2" });
blog.Posts.Add(new() { Title = "Post 3" });

await context.AddAsync(blog);
await context.SaveChangesAsync();
*/
#endregion
#region Object Initializer Üzerinden Ekleme
//Newlenmemiş nesne referansı üzerinden ekleme yapılacaksa eğer object initializer kullanılmalıdır.

/*
Blog blog2 = new()
{
    Name = "a blog",
    Posts = new HashSet<Post>() { new() { Title = "post 4" }, new() { Title = "post 5" } }
};

await context.AddAsync(blog2);
await context.SaveChangesAsync();
*/
#endregion
#endregion
#region 2. Yöntem -> Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Mümkün olsa dahi sadece bir tane veri eklenebilir. Her seferinde yeni bir dependent entity nesnesi oluşturulmalıdır.
/*
Post post = new()
{
    Title = "Post 6",
    Blog = new() { Name = "B Blog" }
};

await context.AddAsync(post);
await context.SaveChangesAsync();
*/
#endregion
#region 3. Yöntem -> Foreign Key Kolonu Üzerinden Veri Ekleme

//1.ve 2.yöntemler hiç olmayan verilerin ilişkisel olarak eklenmesini sağlarken
//3. yöntem önceden eklenmiş olan bir principal entity verisiyle yeni dependent entitylerin
//ilişkisel olarak eşleştirilmesini sağlamaktadır.

/*
Post post = new()
            {
                BlogId = 1, //BlogId kolonu foreign key olduğu için bu kolona değer vermek zorundayız.
                Title = "Post 7"
            };

await context.AddAsync(post);
await context.SaveChangesAsync();
*/
#endregion

/*
class Blog
{
    //Nesne referansı üzerinden ekleme yapılacaksa eğer koleksiyonel propertylerin constructorları içerisinde newlenmesi gerekmektedir.
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }

    public Blog Blog { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ApplicationDb;Trusted_Connection=True;");
    }
}
*/
#endregion

#region Many to Many İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem
// n t n eğer ki default convention ile tasarlanmışsa kullanılan bir yöntemdir.

/*
Book book = new() {
    BookName = "A Kitap", 
    Authors = new HashSet<Author>() {
        new Author() { AuthorName= "Hakan" },
        new Author() { AuthorName = "Alperen" }
        }
};

await context.AddAsync(book);
await context.SaveChangesAsync();
*/

/*
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}

class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}
*/
#endregion
#region 2. Yöntem
//n t n ilişkisi eğer ki fluent api ile tasarlanmışsa kullanılan bir yöntemdir.
//Burada cross-table'a erişebildiğimiz için hem var olan hem de olmayan verileri ilişkilendirebiliriz.


Author author = new()
{
AuthorName = "Emre",
Books = new HashSet<AuthorBook>() {
        new(){ BookId = 1}, //Var olan bir veriye erişim sağlanıyor.
        new(){ Book = new () { BookName = "B Kitap" } } //Yeni bir veri ekleniyor.
    }
}; //Yazar eklerken aynı zamanda kitap eklemiş oluyoruz ve bu kitap yazar ile ilişkilendiriliyor.

await context.AddAsync(author);
await context.SaveChangesAsync();

class Book
{
    public Book()
    {
        Authors = new HashSet<AuthorBook>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<AuthorBook> Authors { get; set; }
}

class AuthorBook
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}

class Author
{
    public Author()
    {
        Books = new HashSet<AuthorBook>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<AuthorBook> Books { get; set; }
}
#endregion



class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ApplicationDb;Trusted_Connection=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorBook>()
            .HasKey(ba => new { ba.AuthorId, ba.BookId });

        modelBuilder.Entity<AuthorBook>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<AuthorBook>()
            .HasOne(ba => ba.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(ba => ba.AuthorId);
    }
}
#endregion

