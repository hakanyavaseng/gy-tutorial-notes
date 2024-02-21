using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

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

//await context.Persons.ToListAsync();
/*
     info: Microsoft.EntityFrameworkCore.Database.Command[20101]
          Executed DbCommand (30ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
          SELECT [p].[PersonId], [p].[Name]
          FROM [Persons] AS [p]
*/
/*
await context.Persons
    .Include(p => p.Orders)
    .Where(p => p.Name.Contains("a"))
    .Select(p => new { p.Name, p.PersonId })
    .ToListAsync();

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

#region Ders 54 - Query Tag

#region Query Tag Nedir?
//EF Core ile generate edilen sorgulara açıklama eklememizi sağlayarak; SQL Profiler, Query Log vb yapılarda bu açıklamalar
//eşliğinde sorguları gözlemlememizi sağlayan bir özelliktir.
#endregion

#region TagWith methodu & Multiple TagWith
await context.Persons.TagWith("Gets all persons").ToListAsync();

/*
     info: Microsoft.EntityFrameworkCore.Database.Command[20101]
          Executed DbCommand (46ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
          -- Gets all persons

          SELECT [p].[PersonId], [p].[Name]
          FROM [Persons] AS [p]
*/

await context.Persons
    .TagWith("Gets all persons")
    .Include(p=>p.Orders)
    .TagWith("Add orders")
    .ToListAsync();
/*
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
          Executed DbCommand (10ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
          -- Gets all persons
          -- Add orders

          SELECT [p].[PersonId], [p].[Name], [o].[OrderId], [o].[Description], [o].[PersonId], [o].[Price]
          FROM [Persons] AS [p]
          LEFT JOIN [Orders] AS [o] ON [p].[PersonId] = [o].[PersonId]
          ORDER BY [p].[PersonId]

*/
#endregion

#region TagWithCallSite Methodu
//Oluşturulan sorguya açıklama satırı ekler ve bu sorgunun hangi satırda oluşturulduğu bilgisini verir.

await context.Persons.TagWithCallSite("Gets all persons").ToListAsync();
/*
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand(1ms) [Parameters = [], CommandType = 'Text', CommandTimeout = '30']
      -- File: Gets all persons: 134

      SELECT[p].[PersonId], [p].[Name]
      FROM[Persons] AS[p]
*/
#endregion


#endregion

#region Ders 55 - Global Query Filters 

#region Global Query Filters Nedir?
//Bir entity'e özel, uygulama seviyesinde şartlar oluşturmamızı ve böylece verileri global şekilde filtrelemeyi sağlayan bir özelliktir.
//Böylece belirtilen entity üzerinden yapılan tüm sorgularda ekstradan şart ifadesine gerek kalmaksınız filtreleri otomatik uygulayarak hızlı sorgulama yapmayı sağlar.
//Örneğin IsActive, IsDeleted gibi özellikler her seferinde yazılmaktansa global olarak tanımlanabilir.
//MultiTenancy uygulamalarda TenantId vs. tanımlarken kullanılabilir.
#endregion

#region Global Query Filters Nasıl Uygulanır?
//=> OnConfiguring 
//modelBuilder.Entity<Person>()
//.HasQueryFilter(p => p.IsActive);
#endregion

#region Navigation Property Üzerinden Kullanım 
//modelBuilder.Entity<Person>()
//       .HasQueryFilter(p => p.Orders.Count > 0);
#endregion

#region Global Query Filters Nasıl Ignore Edilir?
//await context.Persons.IgnoreQueryFilters().ToListAsync(); => Bu sorguda global filter ignore edilir.
#endregion

#region Dikkat!
//Global Query Filter uygulanmış bir kolona tekrardan şart uygulanabilir, buna dikkat edilmelidir. Maliyet artabilir.

//await context.Persons.Where(p=>p.IsActive).ToListAsync();
#endregion


#endregion
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
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

        modelBuilder.Entity<Person>()
            .HasQueryFilter(p => p.IsActive);

        modelBuilder.Entity<Person>()
        .HasQueryFilter(p => p.Orders.Count > 0);




    }

    //Ders 52
    //StreamWriter _log = new("logs.txt", append: true);

    /*Ders 53 
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
    */
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
        
        
        //optionsBuilder.UseLoggerFactory(loggerFactory); => Ders 53

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