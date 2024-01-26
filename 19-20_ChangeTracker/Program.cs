using Microsoft.EntityFrameworkCore;

ETicaretDbContext context = new ETicaretDbContext();
Console.WriteLine();

#region Change Tracker

//Context nesnesi üzerinden gelen tüm nesneler otomatik olarak bir takip mekanizması tarafından takip edilir.
//Bu takip mekanizmasına Change Tracker denir. Change Tracker, bir nesnenin üzerinde yapılan değişiklikleri
//takip eder ve bu değişiklikleri veritabanına yansıtmak için gerekli SQL sorgularını üretir.

#endregion

#region Change Tracker Property'si
//Takip edilen nesnelere erişebilmesini sağlayan ve gerektiği taktirde işlemler yapılabilmesini sağlayan bir property'dir.
//Context sınıfının base class'ı olan DbContext sınıfı, Change Tracker'a erişebilmemizi sağlayan bir property'ye sahiptir.

/*
var urunler = await context.Urunler.ToListAsync();

urunler[6].Fiyat = 0;               // Modified
context.Urunler.Remove(urunler[7]); // Deleted
urunler[8].UrunAdi = "Klavye";      // Modified

var datas = context.ChangeTracker.Entries();

await context.SaveChangesAsync();
*/

#region DetectChanges Metodu
//EF Core, context nesnesi üzerinden gelen nesneleri otomatik olarak takip eder. Ancak bazı durumlarda bu takip işleminin
//yapılması için DetectChanges metodunu çağırmamız gerekebilir. DetectChanges metodunu çağırdığımızda, context nesnesi
//üzerinden gelen nesneleri tekrar kontrol eder ve değişiklikleri tespit eder.

//SaveChanges metodunu çağırdığımızda, EF Core, otomatik olarak DetectChanges metodunu çağırır. Bu nedenle SaveChanges
//metodunu çağırdığımızda DetectChanges metodunu çağırmamıza gerek yoktur. Fakat değişikliklerin algılandığından emin
//olmak için DetectChanges metodunu çağırabiliriz.

/*
var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id ==3);
urun.Fiyat = 0;

context.ChangeTracker.DetectChanges(); //Yapılabilse dahi DetectChanges metodunu çağırmamıza gerek yoktur.
await context.SaveChangesAsync();
*/
#endregion

#region AutoDetectChangesEnabled Property'si
//Performans optimizasyonu yapılmak istendiğinde AutoDetectChangesEnabled property'si false olarak ayarlanabilir. 
//Bu sayede gelen tüm nesneler otomatik olarak takip edilmez.
//Bu property varsayılan olarak true değerindedir.
#endregion

#region Entries Metodu
//Context'teki Entry metodunun koleksiyonel versiyonudur. 
//Change Tracker mekanizması tarafından izlenen her entity nesnesinin EntityEntry türünden elde edilmesini sağlar ve belirli işlemeler yapılmasını sağlar.
//Entries metodu çalışmadan önce DetectChanges metodunu tetikler ve bu durum da bir maliyettir. 

/*
var urunler = await context.Urunler.ToListAsync();
urunler.FirstOrDefault(u => u.Id ==7).Fiyat = 0;

context.ChangeTracker.Entries().ToList().ForEach(e =>
{
    if (e.State == EntityState.Modified)
    {
        Console.WriteLine("Modified");
    }
    else if (e.State == EntityState.Deleted)
    {
        Console.WriteLine("Deleted");
    } });
*/
#endregion

#region AcceptAllChanges Metodu
//Change Tracker mekanizması tarafından izlenen tüm nesnelerin durumlarını Unchanged olarak değiştirir.
//Bu metod SaveChanges metodundan önce çağırılırsa, SaveChanges metodunun hiçbir işlem yapmadan geri dönmesini sağlar.

/*
await context.SaveChangesAsync(); // Default olarak AcceptAllChanges metodunu çağırır. 
await context.SaveChangesAsync(true); // Burası tetiklendiğinde AcceptAllChanges metodunu çağırır. 
await context.SaveChangesAsync(false); // Burası tetiklendiğinde AcceptAllChanges metodunu çağırmaz. 
*/
#endregion

#region HasChanges Metodu
//Change Tracker mekanizması tarafından izlenen nesnelerde değişiklik olup olmadığını kontrol eder.
//Değişiklik varsa true, yoksa false değerini döndürür.
//Arka planda DetectChanges metodunu çağırır.
#endregion

#endregion

#region Entity States
//Entity nesnelerinin durumlarını ifade eder.
    /*
    Detached: Context nesnesi üzerinden gelen nesnelerin durumudur. Context nesnesi üzerinden gelen nesnelerin durumları bu şekilde başlar.
    Added: Yeni bir nesne oluşturulduğunda ve context nesnesi üzerinden bu nesne eklenmek istendiğinde durumu bu şekilde olur.
    Deleted: Context nesnesi üzerinden gelen bir nesne silinmek istendiğinde durumu bu şekilde olur.
    Modified: Context nesnesi üzerinden gelen bir nesne üzerinde değişiklik yapıldığında durumu bu şekilde olur.
    Unchanged: Context nesnesi üzerinden gelen bir nesne üzerinde herhangi bir değişiklik yapılmadığında durumu bu şekilde olur.
    */
#endregion


public class ETicaretDbContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ETicaretDatabase;Trusted_Connection=True;");
    }


}



public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public string Kategori { get; set; }
}


