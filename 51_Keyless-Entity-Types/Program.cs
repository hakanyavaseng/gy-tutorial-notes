using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();
Console.WriteLine();

#region Keyless Entity Types 
//Normal entity type'lara ek olarak primary key içermeyen querylere karşı veritabanı sorguları yürütmek için kullanılan bir özelliktir.
//Genellikle aggreate operasyonların yapıldığı; group by veya pivot table gibi işlemler neticesinde elde edilen istatistiksel sonuçlar primary key kolonu barındırmazlar. Bu sonuçları primary key'i olmayan KET ile tutulabilir.
#endregion

#region Keyless Entity Types Tanımlama
//1 => Hangi sorgu olursa olsun, EF Core üzerinde bu sorgunun bir entity'e karşılık geliyormuş gibi bir işleme tabi tutulabilmesi için yine de bir entity tasarlanmalıdır.
//2 => Bu entity'nin DbSet property'si olarak DbContext nesnesine eklenmesi gerekmektedir. (ToView ile bildirildiği için tablo olarak oluşmayacaktır.)
//3 => Tanımlanmış olan entity'e OnModelConfiguring fonksiyonu içerisinde, primary key'i olmadığı HasNoKey ile bildirilmelidir ve hangi sorgunun çalıştırılacağı da belirtilmelidir. (View, SP, Function)

#region Keyless Attribute
//[Keyless]
#endregion

#region HasNoKey - Fluent API
//.HasNoKey();
#endregion
#endregion

public class PersonOrderCount
{
    public string Name { get; set; }
    public int Count { get; set; }
}

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

    public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrderCount> PersonOrderCounts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);

        modelBuilder.Entity<PersonOrderCount>()
            .HasNoKey()
            .ToView("vm_PersonOrderCount");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=KETDb;Trusted_Connection=true;TrustServerCertificate=True
;");
    }
}