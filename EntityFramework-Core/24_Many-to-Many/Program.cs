using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello World!");

#region Default Convention
//İki entity arasında ilişkiyi navigation property'ler üzerinden çoğul olarak tanımlanır.
//Default convention'da cross-table manuel olarak oluşturulmak zorunda değildir. EF Core bunu otomatik olarak yapar.
//Ayrıca cross-table içindeki composite key'ler de otomatik olarak oluşturulur.

/*
class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }

    public ICollection<Yazar> Yazarlar { get; set; }


}
class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<Kitap> Kitaplar { get; set; }
}
*/
#endregion


#region Data Annotation
//Data Annotation yönteminde many-to-many ilişkisinde cross-table manuel olarak oluşturulur.
//Burada foreign key'leri composite key olarak tanımlamamız gerekiyor.
//Fakat bunun yapılması için Fluent API kullanılması gerekiyor.

//Cross table'a karşılık bir entity modeli oluşturuluyorsa, bu entity modeli için DbSet oluşturmaya gerek yoktur.

/*
class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }

    public ICollection<KitapYazar> Yazarlar { get; set; }

}

//Cross Table
class KitapYazar
{
    [ForeignKey(nameof(Kitap))]
    public int KId { get; set; }

    [ForeignKey(nameof(Yazar))]
    public int YId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}

class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }
}

*/
#endregion

#region Fluent API
//Cross table manuel oluşturulmalı.
//DbSet eklemeye gerek yoktur.
//Composite PK, HasKey fonksiyonu ile kurulur.

/*
class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }

    public  ICollection<KitapYazar> Yazarlar { get; set; }

}

//Cross Table
class KitapYazar
{
    public int KitapId { get; set; }
    public int YazarId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}

class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }
}

*/

#endregion


#region DbContext

class EKitapDbContext : DbContext
{
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EKitapDb;Trusted_Connection=True;");
    }

    //Fluent API, Data Annotation için
    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KId, ky.YId });
    } 
    */

    //Fluent API

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KitapId, ky.YazarId });

        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Kitap)
            .WithMany(k => k.Yazarlar)
            .HasForeignKey(ky => ky.KitapId);

        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Yazar)
            .WithMany(y => y.Kitaplar)
            .HasForeignKey(ky => ky.YazarId);

    }




}


#endregion