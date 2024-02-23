using Microsoft.EntityFrameworkCore;

Console.WriteLine();

ApplicationDbContext context = new();

//In-Memory database üzerinde çalışırken migration oluşturmaya ve migrate etmeye gerek yoktur!
//In-Memory'de oluşturulmuş olan database uygulama sona erdiği/kapatıldığı taktirde bellekten silinecektir.
#region EF Core'da In-Memory Database İle Çalışmanın Gereği Nedir?
#endregion

#region Avantajları Nelerdir?
//Test ve pre-prod uygulamalarda gerçek/fiziksel veritabanları oluşturmak ve yapılandıormak yerine tüm veritanını bellekte modelleyebilir ve gerekli işlemleri sanki gerçek bir veritabanında çalışıyor gibi orada gerçekleştirebiliriz.
//Bellekte çalışmak geçici bir deneyim olacağı için veritabanı serverlarında test amaçlı üretilmiş olan veritabanlarının lüzumsuz yer işgal etmesini engellemiş olacaktır.
//Bellekte veritabanını modellemek kodun hızlı bir şekilde test edilmesini sağlayacaktır.
#endregion

#region Dezavantajları Nelerdir?
//In-Memory'de yapılacak olan veritabanı işlevlerinde ilişkisel modellemeler yapılamamaktadır, bu yüzden veri tutarlılığı ve yanlış verilere karşı dikkat edilmesi gerekmektedir.
#endregion

#region Örnek
//Microsoft.EntityFrameworkCore.InMemory kütüphanesi gereklidir.

await context.Persons.AddAsync(new()
{
    Name = "Hakan",
    Surname = "Yavaş"
});

await context.SaveChangesAsync();

var persons = await context.Persons.ToListAsync();
Console.WriteLine();
#endregion
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ExampleDb");
    }
}