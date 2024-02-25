using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

ApplicationDbContext context = new();


//https://www.gencayyildiz.com/blog/entity-framework-core-data-concurrency/
#region Data Concurrency Nedir?
/*
    =>Birden fazla uygulamanın veya client'ın aynı veritabanı üzerinde eşzamanlı olarak çalıştığı senaryolarda, clientlar ve uygulamalar arasında veri tutarsızlıkları meydana gelebilir.

    =>Data Concurrency kavramı, uygulamalardaki veri tutarsızlığı durumlarına karşılık yönetilebilirliği sağlayacak olan davranışları kapsayan bir kavramdır.

    =>Bir uygulamada veri tutarsızlığı = uygulamayı kullanan kullanıcıları yanıltmak

    =>Veri tutarsızlığının olduğu uygulamalarda istatistiksel olarak yanlış sonuçlar elde edilebilir.
*/
#endregion

#region Stale & Dirty Data Nedir?
//Stale Data : Veri tutarsızlığına sebebiyet verebilecek, güncellenmemiş veya zamanı geçmiş olan verileri ifade eder. => Bir ürünün stok durumu sıfırlanmasına rağmen arayüzde güncelleme olmaması durumu.
//Dirty Data: Veri tutarsızlığına sebebiyet verebilecek verinin hatalı veya yanlış olduğunu ifade etmektedir. => Adı "Ahmet" olan bir kullanıcının veritabanında "Mehmet" olarak tutulması.
#endregion

#region Last In Wins
//Bir veri yapısında son yapılan aksiyona göre en güncel verinin en üstte bulunmasını ifade eden bir deyimsel terimdir.
#endregion

#region Pessimistic Lock
//Bir transaction sürecinde elde edilen veriler üzerinde farklı sorgularla değişiklik yapılmasını engellemek için ilgili verilerin kilitlenmesini (locking) sağlayarak değişikliğe karşı direnç oluşturulmasını ifade eden bir yöntemdir.

//Bu verilerin kilitlenmesi commit ya da rollback edilmesi süreciyle sınırlıdır.

#region Deadlock Nedir?
//Kilitlenmiş olan bir verinin veritabanı seviyesinde meydana gelen sistemsel bir hatadan dolayı kilidinin çözülememesi veya döngüsel olarak kilitlenme durumunun meydana gelmesini ifade eden bir terimdir.

//Pessimistic Lock yönteminde gerçekleşme ihtimali olan ve değerlendirilmesi gereken bir durumdur.
#endregion

#region WITH (XLOCK)
/*
var transaction = context.Database.BeginTransaction();

var person = await context.Persons.FromSql($"SELECT * FROM PERSONS WITH (XLOCK) WHERE PersonId = 5").ToListAsync(); ; // Gelen veriyi veritabanı seviyesinde kilitler ve kilit açılana dek işlem sağlanamaz.
Console.WriteLine();
transaction.Commit();
*/
#endregion

#endregion

#region Optimistic Lock
/*
    =>Bir verinin stale olup olmadığını, locking işlemi olmaksızın, versiyon mantığı ile çalışılmasını sağlar.
    =>Pessimistic Lock'ta olduğu gibi veriler üzerinde tutarsızlığa neden olabilecek değişiklikler fiziksel olarak engellenmemektedir. Veriler değiştirilebilir ama optimistic lock bu tutarsızlığı takip edebilmek için versiyon bilgisi sunar. 
    =>Her bir veriye karşılık bir versiyon bilgisi üretilir. Bu bilgi veri üzerindeki her güncellemede değiştirilecektir. 
    =>Daha sonrasında değişiklikler commit edilirken gelen verinin versiyon numarası ile veritabanındaki güncel versiyon numarası farklı olursa veriler commit edilmeden hata fırlatılacaktır.
    =>EF Core Optimistic Lock yaklaşımı için bir özellik barındırmaktadır.
*/

#region Property Based Configuration (ConcurrencyCheck Attribute)
/*
    =>Veri tutarlılığının kontrol edilmek istenildiği property'ler ConcurrencyCheck attribute'u ile işaretlenir. 
    =>Bu işaretleme neticesinde in-memory'de her instance için bir token üretilir. 
    =>Üretilen bu token değeri alınan aksiyon süreçlerinde EF Core tarafından doğrulanırsa aksiyon başarılı, doğrulanmıyorsa başarısız olarak hata verecektir.
*/

/* 
[ConcurrencyCheck]
veya
   modelBuilder.Entity<Person>()
            .Property(p => p.Name)
            .IsConcurrencyToken();
*/

Person? person = await context.Persons.FindAsync(3);
context.Entry(person).State = EntityState.Modified; 
await context.SaveChangesAsync(); // Breakpoint was here and data changed manually in database, then the error below occured. 
/*
    The database operation was expected to affect 1 row(s), but actually affected 0 row(s); 
    data may have been modified or deleted since entities were loaded. 
*/
#endregion

#region RowVersion Column
//Bu yaklaşımda ise veritabanındaki her bir satıra karşılık gelen versiyon bilgisi fiziksel olarak veritabanında tutulur.

#endregion


#endregion


public class Person
{
    public int PersonId { get; set; }

    //[ConcurrencyCheck]
    public string Name { get; set; }

    [Timestamp] //  After this attribute, EF Core understands that this is for data concurrency operation
    public byte[] RowVersion { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DataConcurrencyDb;Trusted_Connection=true;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Name)
            .IsConcurrencyToken();
    }
}

