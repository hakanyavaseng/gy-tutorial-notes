using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();

#region EF Core Select Sorgularını Güçlendirme Teknikleri

#region IQueryable - IEnumerable Farkı
//IQueryable, bu arayüz üzerinde yapılan işlemler direkt generate edilecek olan sorguya yansıtılacaktır.
//IEnumerable, bu arayüz üzerinde yapılan işlemler temel sorgu neticesinde gelen ve in-memory'e yüklenen instance'lar üzerinde gerçekleştirilir, sorguya yansıtılmaz.

//IQueryable ile yapılan sorgulama sonucunda hedef verileri getirecek şekilde bir sorgu generate edilirken, IEnumerable ile yapılan sorgulama çalışmalarında daha geniş veriler getirilerek veriler in-memory'de ayıklanır. Bu da performans açısından fark yaratır.


IQueryable<Person> queryablePersons = context.Persons.Where(p => p.Name.Contains("a"))  // IQueryable
                                             .Take(3);
List<Person> personList = await queryablePersons.ToListAsync(); //IEnumerable


#region AsQueryable
context.Persons.AsQueryable(); // => Bu satirdan sonra IQueryable olarak devam eder, sart degildir. ENumerable islem yapana kadar default olarak boyle devam eder.
#endregion

#region AsEnumerable
IEnumerable<Person> enumerablePersons = context.Persons.Where(p => p.Name.Contains("a"))  // IQueryable
                                             .AsEnumerable()
                                             .Take(3)
                                             .Skip(3);// After this line, query goes to IEnumerable
#endregion
#endregion

#region Yalnızca Gerekli Kolonları Listeleyin
var allColumnsPersons = await context.Persons.ToListAsync();
var necessaryColumnsPersons = await context.Persons
                            .Select(p => new
                            {
                                p.PersonId,
                                p.Name
                            })
                            .ToListAsync();
#endregion

#region Result'ı limitleyin
await context.Persons.Take(20) // Sadece 20 tane değeri getir. Pagination islemlerinde yogun sekilde kullaniliyor.
    .ToListAsync();
#endregion

#region Join Sorgularında Eager Loading Sürecidne Verileri Filtreleyin
/*
await context.Persons
    .Include(p => p.Orders
    .Where(o => o.OrderId % 2 ==0)
    .OrderByDescending(o => o.OrderDate)
    .Skip(1)
    .Take(20))
    .ToListAsync();
*/
#endregion

#region Şartlara Bağlı Join Yapılacaksa Explicit Loading Kullanın

Person? person = await context.Persons.FirstOrDefaultAsync(p => p.PersonId == 1);

if(person.Name == "Hakan")
{
    await context.Entry(person).Collection(p => p.Orders).LoadAsync();
}

#endregion

#region Lazy Loading Kullanırken Dikkat
var persons = await context.Persons.ToListAsync();

//Maliyetli
/*
foreach(var p in persons)
{
    foreach(var order in p.Orders)
    {
        Console.WriteLine($"{p.Name} - {order.OrderId}");
    }
    Console.WriteLine("****************");
}
*/

//Ideal
/*
var persons = await context.Persons.Select(p => new {p.Name, p.Orders}).ToListAsync();

foreach (var p in persons)
{
    foreach (var order in p.Orders)
    {
        Console.WriteLine($"{p.Name} - {order.OrderId}");
    }
    Console.WriteLine("****************");
}
*/
#endregion

#region İhtiyaç Noktalarında Ham SQL Kullanımı - FromSql
//EF Core ile oluşturması zahmetli olabilecek bir sorguyu ham SQL olarak oluşturabiliriz. View, SP kullanımları uygulanabilir.
#endregion

#region Async Fonksiyonlar
//Asenkron fonksiyonlar performans konusunda daha avantajlıdır.
#endregion


public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public virtual Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EfficientQueryingDb;Trusted_Connection=true;TrustServerCertificate=True;");

    }
}