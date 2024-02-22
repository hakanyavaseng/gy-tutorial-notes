using Microsoft.EntityFrameworkCore;

Console.WriteLine();
ApplicationDbContext context = new();

//https://www.gencayyildiz.com/blog/sql-server-2016-temporal-tables/

#region Temporal Tables Nedir?
//Veri değişikliği süreçlerinde kayıtları depolayan, zaman içinde farklı noktalardaki tablo verilerinin analizi için kullanılan ve sistem tarafından yönetilen tablolardır.
//EF Core 6.0 ile desteklenmeye başlamıştır.
#endregion

#region Temporal Tables Özelliğiyle Nasıl Çalışılır?
/*
    =>EF Core'daki migration yapıları ile temporal table'lar oluşturulabilir
    =>Var olan mevcut tablolar da temporal tabloya çevrilebilmektedir.
    =>Herhangi bir tablonun verisel olarak geçmişini sorgulamak mümkündür.
    =>Herhangi bir tablodaki bir verinin geçmişteki herhangi bir andaki hali geri getirilebilmektedir.
*/
#endregion

#region Temporal Tables Nasıl Uygulanır?
#region IsTemporal
//EF Core bu yapılandırma fonksiyonu ile ilgili entity'e karşılık üretilecek tabloda temporal table oluşturacağını anlar.
#endregion
#endregion

#region Temporal Table Test 
#region Veri Ekleme
//Temporal Table'a veri ekleme süreçlerinde herhangi bir kayıt atılmaz. Çünkü temporal tables var olan veriler üzerindeki zamansal değişimleri takip etmek üzerine kuruludur.

/*
var persons = new List<Person>()
{
    new() { Name = "Hakan", Surname = "Yavaş", BirthDate = DateTime.Now },
    new() { Name = "Alperen", Surname = "Güneş", BirthDate = DateTime.Now },
    new() { Name = "Ceyda", Surname = "Kesgin", BirthDate = DateTime.Now }
};

await context.AddRangeAsync(persons);
await context.SaveChangesAsync();
*/
#endregion
#region Veri Güncelleme
/*
Person? person = await context.Persons.FindAsync(3);
person.Name = "Ceydaaa";

await context.SaveChangesAsync();
*/
#endregion
#region Veri Silme 
/*
Person? person = await context.Persons.FindAsync(2);
context.Remove(person);

await context.SaveChangesAsync();
*/
#endregion
#endregion

#region Temporal Table Üzerinden Geçmiş Verileri Sorgulama

#region TemporalAsOf
//Belirli zaman için değişikliğe uğrayan tüm ögeleri döndüren fonksiyondur.
/*
var p = await context.Persons.TemporalAsOf(DateTime.UtcNow) // Burada verilen zamandaki butunsel verileri getiriyor, tarih ve zaman ayarlamasi onemli.
    .Select( p => new
    {
        p.Id,
        p.Name,
        PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
        PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
    }).ToListAsync();
Console.WriteLine();
*/
#endregion

#region TemporalAll
//Güncellenmiş veya silinmiş olan tüm verilerin geçmiş sürümlerini ve geçerli durumlarını döndüren fonksiyondur.
/*
var p = await context.Persons.TemporalAll() 
    .Select(p => new
    {
        p.Id,
        p.Name,
        PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
        PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
    }).ToListAsync();
Console.WriteLine();
*/
#endregion

#region TemporalFromTo
//Belirli bir zaman aralığı içerisindeki verileri döndüren fonksiyondur, başlangıç ve bitiş dahil değildir.
/*
var p = await context.Persons.TemporalFromTo(D1,D2) 
    .Select(p => new
    {
        p.Id,
        p.Name,
        PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
        PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
    }).ToListAsync();
Console.WriteLine();
*/
#endregion

#region TemporalBetween
//Belirli bir zaman aralığı içerisindeki verileri döndüren fonksiyondur, başlangıç dahil değil,bitiş dahildir.
#endregion

#region TemporalContainedIn
//Belirli bir zaman aralığı içerisindeki verileri döndüren fonksiyondur, başlangıç ve bitiş dahil
#endregion

#endregion
#region SET IDENTITY_INSERT Konfigürasyonu
//Id ile veri ekleme sürecinde, ilgili verinin Id sütununa kayıt işleyebilmek için veriyi fiziksel tabloya taşıma işleminden önce veritabanı seviyesinde SET IDENTITY_INSERT komutu çalıştırılmalıdır.
#endregion
#region Silinmiş Bir Veriyi Temporal Table'dan Getirme
//Silinmiş bir veriden getirebilmek için yapılması gereken ilk adım, ilgili verinin silindiği tarihin bulunması gerekmektedir. 
//Ardından TemporalAsOf ile silinen verinin geçmiş değeri elde edilebilir ve ana tabloya bu veri geri döndürülebilir.

//Silinen tarih
DateTime dateOfDeleted = await context.Persons.TemporalAll()
                            .Where(p => p.Id == 2)
                            .OrderByDescending(p => EF.Property<DateTime>(p, "PeriodEnd"))
                            .Select(p => EF.Property<DateTime>(p, "PeriodEnd"))
                            .FirstAsync();

//O ana ait silinen kullanici bulunuyor.
var deletedPerson = await context.Persons.TemporalAsOf(dateOfDeleted.AddMilliseconds(-1)).FirstOrDefaultAsync(p=>p.Id == 2);

//Ekleniyor, ancak identity insert acik olmadigi icin hata verir. 
await context.AddAsync(deletedPerson);

await context.Database.OpenConnectionAsync();
await context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Persons ON");

await context.SaveChangesAsync();

await context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Persons OFF");
await context.Database.CloseConnectionAsync();
#endregion

#region Entities & DbContext
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .ToTable("Employees", builder => builder.IsTemporal());

        modelBuilder.Entity<Person>()
            .ToTable("Persons", builder => builder.IsTemporal());

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TemporalTablesDb;Trusted_Connection=true;TrustServerCertificate=True;");
    }
}

#endregion