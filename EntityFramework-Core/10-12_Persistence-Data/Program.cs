using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
Console.WriteLine("10");
ETicaretDbContext context = new ETicaretDbContext();

#region Veri Ekleme
/*
    Urun urun = new()
    {
        UrunAdi = "Araba",
        Fiyat = 10
    };
*/
    #region EF Core bir verinin eklenmesi gerektiğini nasıl anlar?
    /*
        Console.WriteLine(context.Entry(urun).State); // Detached

        await context.AddAsync(urun); 

        Console.WriteLine(context.Entry(urun).State); // Added

        await context.SaveChangesAsync();

        Console.WriteLine(context.Entry(urun).State); // Unchanged
    */
    #endregion
    #region context.AddAsync Fonksiyonu
    //await context.AddAsync(urun);
    #endregion
    #region context.DbSet.AddAsync Fonksiyonu
    //await context.Urunler.AddAsync(urun);
    #endregion
    #region SaveChangesAsync Fonksiyonu
    //Insert, update ve delete işlemlerinde SaveChangesAsync fonksiyonu kullanılır.
    //Veritabanının sorgularını oluşturup veritabanına gönderir. (Transaction)
    //Oluşturulan sorgulardan herhangi birinde hata oluşursa tüm sorgular geri alınır. (Rollback)
    //await context.SaveChangesAsync();
    #endregion
    #region Birden fazla veri ekleme
    //Birden fazla veri eklenecekse AddRangeAsync fonksiyonu kullanılabilir.
    //Bu fonksiyon kullanılmadığında dahi her bir veriden sonra SaveChangesAsync fonksiyonunu çağırmak yerine tüm değişiklikler yapıldıktan sonra SaveChangesAsync fonksiyonu çağırılabilir.
    //SaveChanges fonksiyonu verimli kullanılmaz ise performans sorunlarına yol açabilir.
    //Çünkü her çağırıldığında veritabanında bir transaction oluşturulur.

    /*
        Urun urun1 = new()
        {
            UrunAdi = "U1",
            Fiyat = 11
        };

        Urun urun2 = new()
        {
            UrunAdi = "U2",
            Fiyat = 12
        };

        Urun urun3 = new()
        {
            UrunAdi = "U3",
            Fiyat = 13
        };


        await context.AddAsync(urun1);
        await context.AddAsync(urun2);
        await context.AddAsync(urun3);
        await context.SaveChangesAsync();

        await context.AddRangeAsync(urun1, urun2, urun3);
        await context.SaveChangesAsync();
    */

    #endregion
    #region Eklenen Verinin Generate edilen Id değerini almak
    /*
       Urun idAlinacakUrun = new()
        {
            UrunAdi = "IdAlınacakUrun",
            Fiyat = 30
        };
        await context.AddAsync(idAlinacakUrun);
        await context.SaveChangesAsync();
        Console.WriteLine(idAlinacakUrun.Id);
    */
    #endregion
#endregion
#region Veri Güncelleme
    #region FirstOrDefaultAsync Fonksiyonu
    /*
        Urun guncellenecekUrun = await context.Urunler.FirstOrDefaultAsync(x => x.Id == 1); // Verilen sarta uyan ilk veriyi getirir.

        if(guncellenecekUrun?.Id == null)
        {
            Console.WriteLine("Güncellenecek ürün bulunamadı.");
        }

        else
        {
            Console.WriteLine(guncellenecekUrun.UrunAdi + ' ' + guncellenecekUrun.Fiyat.ToString());

            guncellenecekUrun.UrunAdi = "Güncellenen Ürün";
            guncellenecekUrun.Fiyat = 100;

            await context.SaveChangesAsync();

        }
    */
    #endregion
    #region ChangeTracker, Temel Bilgi
    //ChangeTracker context üzerinden gelen verilerin değişikliklerini takip eder.
    //Bu takip mekanizması sayesinde verilerin değişiklikleri tespit edilir.
    //Context üzerinden gelen verilerle ilgili işlemler neticesinde update veya delete işlemleri için sorgu oluşturulur.
    #endregion
    #region Takip Edilmeyen Nesnelerin Güncellenmesi & Update Fonksiyonu
    //Bu sekilde context üzerinden gelmeyen nesneler ChangeTracker tarafından takip edilmez.
    //Bu nedenle bu nesnelerin güncellenmesi için Update fonksiyonu kullanılmalıdır.
    //Update fonksiyonunu kullanabilmek için nesnenin içinde Id değerinin bulunması gerekmektedir.
    /*
        Urun u = new()
        {
            Id = 3,
            UrunAdi = "Guncelenen Urun Id 3",
            Fiyat = 1010
        };

        //context.Urunler.Update(u);
        context.Update(u);
        await context.SaveChangesAsync();
    */
    #endregion
    #region EntityState Nedir?
    //Bir entity instance'ının durumunu belirtir.
    //Entity instance'ı context üzerinden gelmişse EntityState.Added, EntityState.Modified, EntityState.Deleted, EntityState.Unchanged olabilir.
    /*
        Urun u = new();
        Console.WriteLine(context.Entry(u).State); // Detached (Tarafsız)
    */
    #endregion
    #region EF Core bir verinin güncellenmesi gerektiğini nasıl anlar?
    /*
        Urun u = await context.Urunler.FirstOrDefaultAsync(x => x.Id == 3);
        Console.WriteLine(context.Entry(u).State); // Unchanged

        u.UrunAdi = "Güncellenen Urun Id 3-ES";
        u.Fiyat = 1200;

        Console.WriteLine(context.Entry(u).State); // Modified

        await context.SaveChangesAsync();

        Console.WriteLine(context.Entry(u).State); // Unchanged
    */
    #endregion
    #region Birden fazla veri güncelleme
    /*
        var urunler = await context.Urunler.ToListAsync(); // ToListAsync fonksiyonu Select * from Urunler sorgusunu çalıştırır.

        foreach (var urun in urunler)
        {
            urun.UrunAdi = "Güncellenen Urun Id " + urun.Id + "-ES";
            urun.Fiyat = urun.Fiyat + 20;
        }

        await context.SaveChangesAsync();
    */







    #endregion
#endregion
#region Veri Silme
    #region Remove Fonksiyonu
    /*
        Urun u = await context.Urunler.FirstOrDefaultAsync(x => x.Id == 3);
        if(u?.Id == null)
        {
            Console.WriteLine("Silinecek ürün bulunamadı.");
        }
        else
        {
            context.Remove(u);
            await context.SaveChangesAsync();
        }
    */
    #endregion
    #region Context üzerinden gelmeyen nesnelerin silinmesi
    //ChangeTracker, EntityState gibi durumlar diğer işlemlerde olduğu gibidir.
    /*
        Urun u = new()
        {
            Id = 4
        };
        context.Urunler.Remove(u);
        await context.SaveChangesAsync();
    */
    #endregion
    #region EntityState ile silme
    /*
        Urun u = new()
        {
            Id = 5
        };
        context.Entry(u).State = EntityState.Deleted;
        await context.SaveChangesAsync();
    */
    #endregion
    #region Birden fazla veri silme & RemoveRangeAsync Fonksiyonu
    //Diğer işlemlerde olduğu gibi SaveChanges kullanımına dikkat edilmelidir.
        /*
        var urunler = await context.Urunler.ToListAsync();
        context.RemoveRange(urunler);
        await context.SaveChangesAsync();
        */
    #endregion
#endregion
#region DbContext
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
#region Entities
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public decimal Fiyat { get; set; }
}
#endregion