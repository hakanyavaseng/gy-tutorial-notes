using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

ESirketDbContext context = new();

#region Default Convention
//Her iki entity'de Navigation Property olarak birbirini tekil olarak referans eder.
//One to One ilişki türünde dependent entity'nin hangisi olduğunu default olarak EF Core tarafından belirlenemez.
//Bu durumda fiziksel olarak bir foreign key'e karşılık property/kolon tanımlanarak çözüm getirilir.

/*
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; } 
    public CalisanAdresi CalisanAdresi { get; set; }
}
class CalisanAdresi
{
    public int Id { get; set; }
    public int CalisanId { get; set; } // Default Convention ile Calisan class'ının Id'si ile ilişkilendirildi.
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}
*/
#endregion

#region Data Annotation
// Navigation Propertyler tanımlanmalıdır. 
// ForeignKey kolonunun ismi default convention dışında olacaksa, ForeignKey attribute'u ile belirtilmelidir.
// One-to-One ilişkide ekstradan foreign key kolonuna ihtiyaç olmayacağından, dependent entity'deki ID kolonunu hem FK hem de PK olarak kullanabiliriz.

/*
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }
}
class CalisanAdresi
{

    [Key, ForeignKey(nameof(Calisan))] // Burada dependent entity'nin ID değeri, one to one ilişkide principal entity'nin ID değeri ile ilişkilendirildi.
                                       // Burada one-to-one ilişki garantiye alındı. 
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}
*/
#endregion

#region Fluent API
//Navigation Propertyler tanımlanmalıdır. 
//Fluent API yönteminde entityler arasındaki ilişki context sınıfında override edilen OnModelCreating fonksiyonu içinde konfigüre edilir.
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }
}

class CalisanAdresi
{
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}


#endregion

#region DbContext
class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ESirketDb;Trusted_Connection=True;");
    }

    // Model (Entity)'lerin veritabanında generate edilecek yapılarının konfigürasyonu için kullanılır.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Bu fonksiyonu override ederek tüm ilişkileri burada konfigüre edebiliriz.
        //Classlar içinde bir değişiklik yapmaya gerek kalmaz.

        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(c => c.Id); // Fluent API ile PK tanımlandı.
       
        // One-to-One ilişki Fluent API ile konfigüre edildi.
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi) 
            .WithOne(c => c.Calisan)
            .HasForeignKey<CalisanAdresi>(c=> c.Id); 
    }

}
#endregion