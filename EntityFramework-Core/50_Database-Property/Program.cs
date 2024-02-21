using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

ApplicationDbContext context = new();

#region Database Property'si
//Veritabanını temsil eden ve EF Core'un bazı işlevlerinin detaylarına erişmemizi sağlayan property'dir.
#endregion

#region BeginTransaction
//EF Core transaction yönetimini kendisi gerçekleştirmektedir. Manuel olarak "anlık" ele almak istiyorsak bu fonksiyonu kullanabiliriz.

//IDbContextTransaction transaction =  context.Database.BeginTransaction();
#endregion

#region CommitTransaction
//EF Core üzerinde yapılan çalışmaların manuel olarak commit edilebilmesi için kullanılabilir.

//context.Database.CommitTransaction();
#endregion

#region RollbackTransaction
//EF Core üzerinde yapılan çalışmaların geri alınabilmesi için kullanılır.

//context.Database.RollbackTransaction();
#endregion

#region CanConnect
//Verilen connection string'e karşılık bağlantı kurulabilir bir veritabanı olup olmadığını bool olarak verir.

//bool connect = context.Database.CanConnect();
//Console.WriteLine(connect); // True
#endregion

#region EnsureCreated -- !Önemli!
//EF Core'da tasarlanan veritabanını migration kullanmaksızın, runtime'da, yani kod üzerinde veritabanı sunucusuna inşa edebilmek için kullanılan bir fonksiyondur.

//context.Database.EnsureCreated();
#endregion

#region EnsureDeleted -- !Önemli!
//İnşa edilmiş veritabanını runtime'da silmemizi sağlayan bir fonksiyondur.

//context.Database.EnsureDeleted();
#endregion

#region GenerateCreateScript
//Context nesnesinde yapılmış olan veritabanı tasarımı her ne ise ona uygun bir SQL Script'ini string olarak veren metottur.

//string script = context.Database.GenerateCreateScript();
//Console.WriteLine(script);
#endregion 

#region ExecuteSql
//Veritabanına yapılacak INSERT,UPDATE,DELETE sorgularının yazıldığı bir metottur. Bu metot işlevsel olarak alacağı parametreleri
//SQL Injection saldırılarına karşı korumaktadır. (String interpolation)

//var result = context.Database.ExecuteSql($"INSERT INTO Persons Values('Hakan')");
#endregion

#region ExecuteSqlRaw
//Veritabanına yapılacak INSERT,UPDATE,DELETE sorgularının yazıldığı bir metottur. Bu metot işlevsel olarak alacağı parametreleri
//SQL Injection saldırılarına karşı KORUMAMAKTADIR. Bu geliştiricinin sorumluluğundadır.

//var result = context.Database.ExecuteSqlRaw($"INSERT INTO Persons Values('Hakan')");
#endregion

#region SqlQuery
//Deprecated, bu fonksiyon yerine DbSet property'si üzerinden erişilebilen FromSql fonksiyonu gelmiştir.
#endregion

#region SqlQueryRaw
//Deprecated, bu fonksiyon yerine DbSet property'si üzerinden erişilebilen FromSqlRaw fonksiyonu gelmiştir.
#endregion

#region GetMigrations
//Uygulamada üretilmiş olan tüm migration'ları runtime'da programatik olarak elde etmemizi sağlar.

//var migs = context.Database.GetMigrations();
//Console.WriteLine();
#endregion

#region GetAppliedMigrations
//Uygulamada migrate edilmiş tüm migration'ları elde etmemizi sağlayan bir fonksiyondur.

//var migs = context.Database.GetAppliedMigrations(); // mig-1 => Çünkü update edilen sadece bu migration.
//Console.WriteLine();
#endregion

#region GetPendingMigrations
//Uygulanmayan migration'ları getirir. (Update edilmemiş)

//context.Database.GetPendingMigrations();
//Console.WriteLine();
#endregion

#region Migrate
//Migration'ları programatik olarak runtime'da migrate etmek için kullanılan bir fonksiyondur. Ne kadar migration varsa hepsini migrate eder.

//EnsureCreated fonksiyonu migration'ları kapsamamaktadır. O yüzden migration içerisinde yapılan çalışmalar ilgili fonksiyon içerisinde geçerli olmayacaktır.

//context.Database.Migrate();
#endregion

#region OpenConnection - CloseConnection
//Veritabanı bağlantısını manuel olarak açıp kapamayı sağlarlar.

//context.Database.OpenConnection();
//context.Database.CloseConnection();
#endregion

#region GetConnectionString
//İlgili context nesnesinin o anda kullandığı Connection String değerini elde etmeyi sağlar.

//string cs = context.Database.GetConnectionString();
//Console.WriteLine();
#endregion

#region GetDbConnection
//EF Core'un kullanmış olduğu ADO.NET altyapısının kullandığı DbConnection nesnesini elde etmemizi sağlayan fonksiyondur.
//Uygulamayı ADO.NET kanadıda götürür.

//SqlConnection connection = (SqlConnection)context.Database.GetDbConnection();
//Console.WriteLine();
#endregion

#region SetDbConnection 
//Özelleştirilmiş connection nesnelerini EF Core mimarisine dahil etmeyi sağlar.
#endregion

#region ProviderName Property'si
//EF Core'un kullanmış olduğu provider bilgisini verir.

//Console.WriteLine(context.Database.ProviderName); // Microsoft.EntityFrameworkCore.SqlServer
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DatabasePropDb;Trusted_Connection=true;TrustServerCertificate=True
;");
    }
}
