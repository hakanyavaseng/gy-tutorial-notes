using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

BackingFieldDbContext context = new();
Console.WriteLine();

#region BackingField nedir?
//Tablo içerisindeki kolonların entity class'ları içersinde field'larla temsil edilmesini sağlar.

/*
class Person
{
    public int Id { get; set; }

    public string name;
    public string Name { get => name; set => name = value; } //BackingField ile name field'ına değerler atanır ve okunur.
    public string Department { get; set; }

}
*/
#endregion

#region BackingField Attribute
/*
class Person
{
    public int Id { get; set; }

    public string name;

    [BackingField(nameof(name))] 
    public string Name { get; set; } //Sorgulama sırasında gelen verilerde sadece field kullanılır. Property null gelir.
    public string Department { get; set; }

}
*/
#endregion

#region HasField Fluent API
/*
class Person
{
    public int Id { get; set; }

    public string name;
    public string Name { get; set; } 
    public string Department { get; set; }

}
*/
#endregion

#region Field and Property Access
//EF Core sorgulama sürecinde entity içerisindeki propertyleri ya da fieldları kullanıp kullanmayacağını belirler.

//EF Core hiçbir ayarlama yoksa propertyleri, eğer ki backing field varsa fieldları kullanır, eğer ki backing field bildirildiği halde 
//davranış belirtilmişse o davranışa göre hareket eder.

//UsePropertyAccessMode üzerinden davranış belirtilir.

#endregion

#region Field-Only Properties
//Entity'lerde değerleri almak için propertyler yerine metodların kullanıldığı veya belirli alanların hiç gösterilmemesi gerektiği durumlarda kullanılır.
//Örneğin; primary key, şifre gibi alanlar.

/*
class Person
{
    public int Id { get; set; }
    public string name;
    public string Department { get; set; }
    public string GetName() => name; //Field-Only Property
    public string SetName(string name) => this.name = name; //Field-Only Property
}
*/
#endregion

class BackingFieldDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BackingFieldDb;Trusted_Connection=True;");
    }

    //HasField Fluent API
    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Name)
            .HasField(nameof(Person.name)); //Fluent API ile field belirtilir.
    }
    */

    //Field and Property Access

    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Name)
            .HasField(nameof(Person.name))
            .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction); //Kullanılacak davranış belirtilir.


            //Field => Veri erişim süreçlerinde sadece field kullanılır, eğer ki field kullanılamıyorsa exception fırlatılır.
            //FieldDuringConstruction => Veri erişim süreçlerinde ilgili entity'den bir instance oluşturulurken field kullanılır.
            //Property => Veri erişim süreçlerinde sadece property kullanılır eğer property kullanılamıyorsa (read-only,write-only) exception fırlatılır.
            //PreferField => Veri erişim süreçlerinde öncelikle field kullanılır, eğer ki field kullanılamıyorsa property kullanılır.
            //PreferFieldDuringConstruction => Veri erişim süreçlerinde ilgili entity'den bir instance oluşturulurken öncelikle field kullanılır, eğer ki field kullanılamıyorsa property kullanılır.
            //PreferProperty => Veri erişim süreçlerinde öncelikle property kullanılır, eğer ki property kullanılamıyorsa field kullanılır.
    }
    */

    //Field-Only Properties

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
             .Property(nameof(Person.name)); //Name field'ını name property'si gibi kullanır.
    }

}

