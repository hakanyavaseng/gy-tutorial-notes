using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();


#region EF Core 7 Yenilikleri

//IQueryable üzerinde çalışıp, verileri in-memory'e almaya gerek kalmaksızın sorguyu işletmeyi sağlar.
#region ExecuteUpdate
//await context.Persons.Where(p => p.PersonId > 5).ExecuteUpdateAsync(p => p.SetProperty(p => p.Name, v => v.Name + " YENI"));
#endregion

#region ExecuteDelete
//await context.Persons.Where(p => p.PersonId > 5).ExecuteDeleteAsync(); //
#endregion

#region Entity'lerdeki Service Injection Yapılanması
#endregion

#region Entity Splitting
//Birden fazla fiziksel tabloyu EF Core kısmında tek bir entity ile temsil etmemizi sağlayan özelliktir.

#region Veri Ekleme
Person person = new()
{
    Name = "Hakan",
    Surname = "Yavaş",
    City = "Eskisehir",
    Country = "Turkiye",
    PostCode = "26000",
    Street = "XXX",
    PhoneNumber = "545642313216"
};

await context.AddAsync(person);
await context.SaveChangesAsync();
#endregion



#endregion

#endregion



/* Execute Update 
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
}*/
public class Person
{
    #region Persons Tablosu
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    #endregion
    #region PhoneNumbers Tablosu
    public string? PhoneNumber { get; set; }
    #endregion
    #region Addresses Tablosu
    public string Street { get; set; }
    public string City { get; set; }
    public string? PostCode { get; set; }
    public string Country { get; set; }
    #endregion
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entityBuilder =>
        {
            entityBuilder.ToTable("Persons")
                .SplitToTable("PhoneNumbers", tableBuilder =>
                {
                    tableBuilder.Property(person => person.Id).HasColumnName("PersonId");
                    tableBuilder.Property(person => person.PhoneNumber);
                })
                .SplitToTable("Addresses", tableBuilder =>
                {
                    tableBuilder.Property(person => person.Id).HasColumnName("PersonId");
                    tableBuilder.Property(person => person.Street);
                    tableBuilder.Property(person => person.City);
                    tableBuilder.Property(person => person.PostCode);
                    tableBuilder.Property(person => person.Country);
                });
        });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EfCore7Db;Trusted_Connection=true;TrustServerCertificate=True;");
    }
}