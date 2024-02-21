// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

ESirketDbContext context = new ESirketDbContext();

#region Default Convention
// Default Convention yönteminde bire çok ilişkiyi kurarken foreign key kolonuna karşılık gelen property tanımlanmak zorunda değildir,
// Eğer tanımlama yapılmazsa EF Core tarafından oluşturulur. Eğer tanımlanırsa tanımlanan property kullanılır.

/*
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public int DepartmanId { get; set; } // Default Convention ile DepartmanId kolonu oluşur. Bu kullanılmazsa dahi EF Core tarafından oluşturulur.
    public Departman Departman { get; set; }
}

class Departman
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}
*/
#endregion

#region Data Annotation
// Data Annotation yönteminde  foreign key kolonuna karşılık gelen prop tanımlandığında default convention kurallarına uymuyorsa 
// [ForeignKey("Departman")] şeklinde tanımlama yapılmalıdır. 

/*
class Calisan
{
    public int Id { get; set; }

    public string Adi { get; set; }

    [ForeignKey("Departman")]
    public int DId { get; set; } 
    public Departman Departman { get; set; }
}

class Departman
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}

*/

#endregion

#region Fluent API
/*
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }

    public int DId { get; set; }
    public Departman Departman { get; set; }
}

class Departman
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}
*/
#endregion


#region DbContext
class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Departman> Departmanlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ESirketDb;Trusted_Connection=True;");
    }

    // Fluent API

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Burada FK tanımlaması gerekmemektedir, çünkü one-to-many ilişkisi EF Core tarafından tanımlanır.
        //Eğer kendi tanımladığımız bir property'i kullanmak istersek HasForeignKey() method
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.Departman)
            .WithMany(d => d.Calisanlar)
            .HasForeignKey(c => c.DId);
    }

}
#endregion