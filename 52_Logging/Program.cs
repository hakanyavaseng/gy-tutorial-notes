using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

ApplicationDbContext context = new();
Console.WriteLine();

#region Ders 52 - Logging
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
#endregion

#region Ders 53 - Query Log

#region Query Log Nedir?
//LINQ sorguları neticesinde generate edilen sorguları izleyebilmek ve olası teknik hataları ayıklayabilmek amacıyla query log mekanizması kullanılır
#endregion

#region Nasıl Konfigüre Edilir?
//Microsoft.Extensions.Logging.Console

await context.Persons.ToListAsync();
/*
     info: Microsoft.EntityFrameworkCore.Database.Command[20101]
          Executed DbCommand (30ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
          SELECT [p].[PersonId], [p].[Name]
          FROM [Persons] AS [p]
*/

await context.Persons
    .Include(p => p.Orders)
    .Where(p => p.Name.Contains("a"))
    .Select(p => new { p.Name, p.PersonId })
    .ToListAsync();
/*
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
          Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
          SELECT [p].[Name], [p].[PersonId]
          FROM [Persons] AS [p]
          WHERE [p].[Name] LIKE N'%a%'
*/

#endregion

#region Loglama Sürecinde Filtreleme Nasıl Yapılır?
//Belirli seviyelerdeki logları filtrelemek için kullanılır.
/*
  readonly ILoggerFactory loggerFactoryWithFilter = LoggerFactory.Create(builder =>
    {
        builder.AddFilter((category, level) =>
        {
           return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information;
        })
        .AddConsole(); //Microsoft.Extensions.Logging.Console ile gelmektedir.
    });
*/
#endregion


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

    //Ders 52
    //StreamWriter _log = new("logs.txt", append: true);

    //Ders 53
    readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole(); //Microsoft.Extensions.Logging.Console ile gelmektedir.
    });

    readonly ILoggerFactory loggerFactoryWithFilter = LoggerFactory.Create(builder =>
    {
        builder.AddFilter((category, level) =>
        {
           return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information;
        })
        .AddConsole(); //Microsoft.Extensions.Logging.Console ile gelmektedir.
    });
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=LoggingDb;Trusted_Connection=true;TrustServerCertificate=True;");

        /* Ders 52
        //optionsBuilder.LogTo(Console.WriteLine);
        //optionsBuilder.LogTo(message => Debug.WriteLine(message));
        //optionsBuilder.LogTo(async message => await _log.WriteLineAsync(message));
        //optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
        //optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
        //optionsBuilder.LogTo(async message => await _log.WriteLineAsync(message),LogLevel.Error);
        */

optionsBuilder.UseLoggerFactory(loggerFactory);



    }

    /*Ders 52
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
    */
}