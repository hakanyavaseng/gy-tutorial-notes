using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using System.Transactions;

Console.WriteLine();
ApplicationDbContext context = new();

#region Transaction Nedir?
//Veritabanındaki kümülatif işlemleri atomik bir şekilde gerçekleştirmemizi sağlar.
//Bir transaction içerisindeki tüm işlemler commit edildiği taktirde veritabanına yansıtılır. Rollback edilirse tüm işlemler geri alınır ve veritabanında değişiklik olmaz.
#endregion

#region Default Transaction Davranışı
//EF Core'da varsayılan olarak, yapılan tüm işlemler SaveChanges fonksiyonu ile veritabanına fiziksel olarak uygulanır. Çünkü SaveChanges default olarak bir transaction'a sahiptir.
//Eğer ki bu süreçte bir hata olursa tüm işlemler geri alınır ve veritabanına uygulanmaz.
#endregion

#region Transaction Kontrolünü Manuel Sağlama
/*
IDbContextTransaction transaction =  context.Database.BeginTransaction();

Person p = new() { Name = "Hakan" };

await context.Persons.AddAsync(p);
await context.SaveChangesAsync();

await transaction.CommitAsync();
*/
#endregion

#region Savepoints
//EF Core 5.0 sürümüyle gelmiştir.
//Veritabanı işlemleri sürecinde bir hata oluşursa veya başka bir nedenle yapılan işlemlerin geri alınması gerekiyorsa transaction içerisinde dönüş yapılabilecek noktaları ifade eden bir özelliktir.

#region CreateSavepoint
//Transaction içerisinde geri dönüş noktası oluşturmamızı sağlar.
#endregion

#region RollbackToSavepoint
//Transaction içerisinde herhangi bir savepoint'e rollback yapmamızı sağlayan fonksiyondur.
#endregion
/*
IDbContextTransaction transaction = context.Database.BeginTransaction();

Person? p4 = await context.Persons.FindAsync(4);
Person? p3 = await context.Persons.FindAsync(3);

context.Persons.RemoveRange(p4, p3);
await context.SaveChangesAsync();


await transaction.CreateSavepointAsync("t1");

Person? p2 = await context.Persons.FindAsync(2);
context.Persons.Remove(p2);
await context.SaveChangesAsync();

await transaction.RollbackToSavepointAsync("t1");
await transaction.CommitAsync();
*/
#endregion

#region TransactionScope
//Veritabanı işlemlerini bir grup olarak yapmamızı sağlayan bir sınıftır.

using TransactionScope transactionScope = new();
/// <summary>
/// Veritabanı islemleri
/// </summary>

transactionScope.Complete(); // Complete fonksiyonu yapılan veritabanı işlemlerinin commit edilmesini sağlar.
//Eğer ki rollback yapılmak istenirse, complete fonksiyonunun tetiklenmemesi yeterlidir.

#endregion
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

    public Person Person { get; set; }
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
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TransactionsDb;Trusted_Connection=true;TrustServerCertificate=True;");
    }
}