using Microsoft.EntityFrameworkCore;

Console.WriteLine("09");

#region OnConfiguring ile Konfigürasyon Ayarları Gerçekleştirme

//OnConfiguring, EF Core Tool'unu yapılandırmak için kullanılan bir metottur.
//Bu metot, DbContext sınıfından türetilen sınıflarda override edilerek kullanılır.
public class ETicaretDbContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider
        //Connection String
        //Lazy Loading vb.
        optionsBuilder.UseSqlServer("server=DESKTOP-OC4LTKM\\SQLEXPRESS;database=ETicaretDb;integrated security=true;");
    }
}
#endregion

#region Temel Entity Tanımlama
public class Urun
{
    //Boş halde migrate işlemi yapılırsa => The entity type 'Urun' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'. 
    //EF Core varsayılan olarak Primary Key alanı olmayan bir entity sınıfını tabloya dönüştürmez.
    //Primary key kullanılmayacağı durumda HasNoKey metodu ile entity sınıfının bir Primary Key alanı olmadığı belirtilmelidir.

    public int Id { get; set; } // Name convention ile EF Core Id, ID, UrunId, UrunID olan property'i otomatik olarak primary key yapar.

}
#endregion

