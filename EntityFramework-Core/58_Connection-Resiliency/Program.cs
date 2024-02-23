using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

ApplicationDbContext context = new();

#region Connection Resiliency nedir? Nasıl Uygulanır?
//EF Core üzerinde yapılan veritabanı çalışmaları sürecinde, ister istemez veritabanı bağlantısına kopukluk, kesinti meydana gelebilmektedir.

#region EnableRetryOnFailure
//Uygulama sürecinde veritabanı bağlantısı koptuğu taktirde bu yapılandırma sayesinde bağlantıyı tekrar kurmaya çalışır.

while (true)
{
    await Task.Delay(1000);
    var persons = await context.Persons.ToListAsync();
    persons.ForEach(p => Console.WriteLine(p.Name));
    Console.WriteLine("*********************");
    break;
}

#endregion

#region MaxRetryCount 
//Yeniden bağlantı denemesinin kaç kere gerçekleştireceğini bildirir.
//Default => 6
#endregion

#region MaxRetryDelay
//Yeniden bağlantı sağlanması periyodunu bildirmektedir.
//Default => 30s
#endregion


#endregion

#region Execution Strategies 
//EF Core ile yapılan bir işlem sürecinde veritabanı bağlantısı koptugu taktirde yeniden bağlanma davranışına/aksiyona denir.

#region Default Execution Strategies
//Connection resiliency için EnableRetryOnFailure metodu kullanılıyorsa bu defaul execution strategy'e karşılık gelecektir.
//MaxRetryCount : 6
//Delay : 30s

//Parametreler değişse dahi Default Execution Strategies üzerinde çalışılmış olacaktır
#endregion

#region Custom Execution Strategies 
//CustomExecutionStrategy class'ına bakilabilir.
#endregion

#region Bağlantığı Koptuğu Anda Execute Edilmesi Gereken Tüm Çalışmaları Tekrardan İşletme
//Bağlantının koptuğu andaki çalışmaların bazen tekrardan işlenmesi gerekebilir. Bu durumda EF Core Execute-ExecuteAsync fonksiyonunu sağlamaktadır.

//Execute fonksiyonu, içerisine verilmiş olan kodları commit edilene kadar işleyecektir. Bağlantı kesilirse, bağlantının tekrardan kurulması durumunda Execute içerisindeki çalışmaları baştan işletecek ve yapılan işlemlerin tutarlılığı için gerekli çalışma sağlanmış olacaktır.
#endregion

IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

await strategy.ExecuteAsync(async () =>
{
    using var transaction = await context.Database.BeginTransactionAsync();

    await context.Persons.AddAsync(new() { Name = "Ayşe" });
    await context.SaveChangesAsync();

    await context.Persons.AddAsync(new() { Name = "Kadir" });
    await context.SaveChangesAsync();

    await transaction.CommitAsync();

});


#endregion




public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #region Default Execution Strategy 
        /*
          optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ConnectionResiliencyDb;Trusted_Connection=true;TrustServerCertificate=True;", builder =>
            builder.EnableRetryOnFailure(
                maxRetryCount: 10, 
                maxRetryDelay: TimeSpan.FromSeconds(30), 
                errorNumbersToAdd: new[] {4060}))
            .LogTo(
            filter: (eventId, level) => eventId.Id == CoreEventId.ExecutionStrategyRetrying, 
            logger: eventData =>
                    {
                        Console.WriteLine($"Retrying connecting to the server."); //Logs connection interrupts to console.
                    
        */
        #endregion

        #region Custom Execution Strategy
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ConnectionResiliencyDb;Trusted_Connection=true;TrustServerCertificate=True;", 
            builder => builder
            .ExecutionStrategy(dependencies => new CustomExecutionStrategy(this, 3, TimeSpan.FromSeconds(15))));

        #endregion



    }
}
class CustomExecutionStrategy : ExecutionStrategy
{
    public CustomExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
    {
    }

    public CustomExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) : base(dependencies, maxRetryCount, maxRetryDelay)
    {
    }

    int retryCount = 0;
    protected override bool ShouldRetryOn(Exception exception)
    {
        //Yeniden bağlantı durumunun söz konusu olduğu zaman yapılacak işlemler.
        Console.WriteLine($"Retrying connection {retryCount}...");
        return true;
    }
}