using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

ApplicationDbContext context = new();

#region Value Conversions Nedir?
//EF Core üzerinden veritabanı ile yapılan sorgulama süreçlerinde veriler üzerinde dönüşümler yapmamızı sağlayan bir özelliktir.
//SELECT sorguları sürecinde gelecek veriler üzerinde dönüşüm yapılabilir ya da UPDATE veya INSERT sorgularında yazılım üzerinden veritabanına gönderilen verileri üzerinde de dönüşümler yapılabilir.
#endregion

#region Value Converter Kullanımı
#region HasConversion
//EF Core üzerinden en sade haliyle value converter özelliği gören bir fonksiyondur.

//var persons = await context.Persons.ToListAsync();
//Console.WriteLine();
#endregion
#endregion

#region Enum Değerler ile Value Conversion
//Default olarak enum değerler veritabanına int olarak gönderilir. Value conversion ile istenilen türe çevirilerek veritabanında da istenilen türde kaydetme işlemi yapılmış olur. 
/*
var person = new Person() { Name = "Ayşe", Gender2 = Gender.Female, Gender = "F" };

await context.Persons.AddAsync(person);
await context.SaveChangesAsync();
var ayse = await context.Persons.FindAsync(person.Id);
Console.WriteLine();
*/
#endregion

#region ValueConverter Sınıfı
//Value Converter sınıfı verisel dönüşümlerdeki sorumlulukları üstlenebilecek bir sınıftır. Bu sınıfın instance'ı ile HasConversion fonksiyonda yapılan çalışmalar gerçekleştirilebilir, direkt olarak bu instance ilgili fonksiyona verilerek operasyon sağlanabilir.

/*
var person = new Person() { Name = "Ayşe", Gender2 = Gender.Female, Gender = "F" };

await context.Persons.AddAsync(person);
await context.SaveChangesAsync();
var ayse = await context.Persons.FindAsync(person.Id);
Console.WriteLine();
*/
#endregion

#region Custom ValueConverter Sınıfı
//EF Core'da custom value converter sınıfları oluşturulabilir. Bunun için custom sınıfın ValueConverter sınıfından miras alması gerekmektedir.
#endregion

#region Built-in Converters Yapıları
//EF Core basit dönüşümler için kendi bünyesinde yerleşik convert sınıfları barındırmaktadır.

#region BoolToZeroOneConverter
#endregion
#region BoolToStringConverter
#endregion
#region BoolToTwoValuesConverter
#endregion
#endregion

#region İlkel Koleksiyonların Serilizasyonu
//İçerisinde ilkel türlerden olyuşturulmuş koleksiyonları barındıran modelleri migrate etmeye çalıştığımızda hata ile karşılaşmaktayız. By hatadan kurtuılmak ve ilgili veriye koleksiyondaki verileri serilize ederek işleyebilmek için bu koleksiyonu normal metinsel değerlere dönüştürmemize fırsat veren bir conversion operasyonu gerçekleştireibliriz. 

/*
var person = new Person() { Name = "Ceyda", Gender = "M", Gender2 = Gender.Male, Married = true, Titles = new() { "A", "B", "C" } };
await context.Persons.AddAsync(person);

await context.SaveChangesAsync();

var _person = await context.Persons.FindAsync(person.Id);
Console.WriteLine();
*/
#endregion
#region .NET 6 - Value Converter For Nullable Fields
//.NET 6'dan önce value converter'lar null değerlerin dönüşümü desteklenmiyordu. .NET 6 ile artık null değerler de dönüştürülebilmektedir.
#endregion



//Custom ValueConverter 
public class GenderConverter : ValueConverter<Gender, string>
{
    public GenderConverter() : base(g => g.ToString(), g => (Gender)Enum.Parse(typeof(Gender), g))
    {
    }

}
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public Gender Gender2 { get; set; }
    public bool Married { get; set; }
    public List<string>? Titles { get; set; }

}

public enum Gender
{
    Male,
    Female
}

public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        #region Value Converter Kullanımı
        /*
        modelBuilder.Entity<Person>()
            .Property(p => p.Gender) // String olan property
            .HasConversion(
                //INSERT-UPDATE durumlarındaki davranışlar
                g => g.ToUpper()
            ,
                //SELECT sorgusundaki davranışlar
                g => g == "M" ? "Male" : "Female"
            );
        */
        #endregion

        #region Enum Değerler ile Value Conversion
        
        modelBuilder.Entity<Person>()
           .Property(p => p.Gender2) // String olan property
           .HasConversion(
               //INSERT-UPDATE durumlarındaki davranışlar
               g => g.ToString()
               //g => (int)g
           ,
               //SELECT sorgusundaki davranışlar
               g => (Gender)Enum.Parse(typeof(Gender), g)
           );

        #endregion

        #region ValueConverter Sınıfı
        /*
        ValueConverter<Gender, string> converter = new(
               g => g.ToString()
               //g => (int)g
                ,
               //SELECT sorgusundaki davranışlar
               g => (Gender)Enum.Parse(typeof(Gender), g)
            );

        modelBuilder.Entity<Person>()
            .Property(p => p.Gender2)
            .HasConversion(converter);
        */
        #endregion

        #region Custom ValueConverter Sınıfı
        /*
        modelBuilder.Entity<Person>()
            .Property(p => p.Gender2)
            .HasConversion<GenderConverter>();
        */
        #endregion

        #region BoolToZeroOneConverter
        /*
        //Ikisi de aynı işi yapar
        modelBuilder.Entity<Person>()
            .Property(p => p.Married)
            .HasConversion<BoolToZeroOneConverter<int>>();


        modelBuilder.Entity<Person>()
          .Property(p => p.Married)
          .HasConversion<int>();
        */
        #endregion

        #region BoolToStringConverter
        /*
        BoolToStringConverter converter = new("Bekar", "Evli");

        modelBuilder.Entity<Person>()
          .Property(p => p.Married)
          .HasConversion(converter);
        */
        #endregion

        #region BoolToTwoValuesConverter
        /*
        BoolToTwoValuesConverter<char> converter = new('B', 'E');

        modelBuilder.Entity<Person>()
          .Property(p => p.Married)
          .HasConversion(converter);
        */
        #endregion

        #region İlkel Koleksiyonların Serilizasyonu
        modelBuilder.Entity<Person>()
            .Property(p => p.Titles)
            .HasConversion(
            //INSERT - UPDATE
            t => JsonSerializer.Serialize(t, (JsonSerializerOptions)null)
            ,
            //SELECT
            t => JsonSerializer.Deserialize<List<string>>(t, (JsonSerializerOptions)null)
            );
        #endregion


    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ValueConversionsDb;Trusted_Connection=true;TrustServerCertificate=True;");
    }
}