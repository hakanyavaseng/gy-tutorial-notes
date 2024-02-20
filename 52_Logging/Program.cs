using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

ApplicationDbContext context = new();
var datas = await context.Persons.ToListAsync();
Console.WriteLine();

#region Neden Loglama Yapılır?
//Çalışan bir sistemin runtimeda nasıl davranış gerçekleştirdiğini gözlemlemek için Log mekanizamaları kullanılır.
#endregion

#region Neler Loglanır?
//Yapılan sorguların çalışma süreçlerindeki davranışları, gerekirse hassas veriler de loglanabilir.
#endregion

#region Basit Düzeyde Loglama Nasıl Yapılır?
//Basit => Minimum yapılandırma gerektirmesi, herhangi bir NuGet paketine ihtiyaç duymadan loglamanın yapılabilmesi.

//var datas = await context.Persons.ToListAsync(); // Otomatik olarak console'a loglar.

#region Bir dosyaya log nasıl atılır?
//Normalde console veya debug pencerelerine atılan loglar'ın takip edilmesi zordur, logların kalıcı hale getirilmesi için dosyaya loglanabilir.
#endregion

#endregion

#region Hassas Verilerin Loglanması - EnableSensitiveDataLogging
//Default olarak EF Core log mesajlarında herhangi bir verinin değerini içermemektedir, bunun nedeni gizlilik teşkil edebilecek verilerin açığa çıkmamasıdır.

//optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();

#endregion

#region Exception Ayrıntısını Loglama - EnableDetailedErrors
//optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
#endregion

#region Log Levels
//optionsBuilder.LogTo(async message => await _log.WriteLineAsync(message), LogLevel.Error);
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
    public int Price { get; set; }

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
    StreamWriter _log = new("logs.txt", append: true);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=LoggingDb;Trusted_Connection=true;TrustServerCertificate=True;");

        //optionsBuilder.LogTo(Console.WriteLine);
        //optionsBuilder.LogTo(message => Debug.WriteLine(message));
        //optionsBuilder.LogTo(async message => await _log.WriteLineAsync(message));
        //optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
        //optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
        optionsBuilder.LogTo(async message => await _log.WriteLineAsync(message),LogLevel.Error);

    }

    public override void Dispose()
    {
        base.Dispose();
        _log.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _log.DisposeAsync();

    }
}