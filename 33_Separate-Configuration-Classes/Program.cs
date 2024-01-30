using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

Console.WriteLine();


#region OnModelCreating 
//Genel anlamda veritabanı ile ilgili konfigürasyonel operasyonların dışında,
// Entityler üzerinde konfigürasyonel çalışmalar yapmamızı sağlayan bir fonksiyondur.
#endregion

#region IEntityTypeConfiguration<T> Arayüzü
//Entity bazlı yapılacak olan konfigürasyonları, o entitye özel harici bir soya üzerinde yapmamızı sağlayan bir arayüzdür.

//Harici bir dosyada konfigürasyonların yürütülmesi, merkezi bir yönetim noktası oluşturulmasını sağlar.
#endregion

#region ApplyConfiguration
//Bu metot, harici konfigürasyonel sınıflarımızı EF Core'a bildirmek için kullanılan bir metottur.
#endregion

#region ApplyConfigurationsFromAssembly
//Bu metot, belirtilen bir assembly içerisindeki tüm konfigürasyonel sınıfların EF Core'a bildirilmesini sağlayan bir metottur.

#endregion

class Order
{
	public int OrderId { get; set; }
    public string Description { get; set; }
	public DateTime OrderDate { get; set; }	
}

class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		//Buradan sonrası Fluent API ile ilgili konfigürasyonel çalışmalarımızı yapabileceğimiz alandır.
		builder.HasKey(x => x.OrderId);
		builder.Property(x => x.Description)
			.HasMaxLength(100);
		builder.Property(x => x.OrderDate)
			.HasDefaultValue(DateTime.Now);
	}
}

class ApplicationDbContext : DbContext
{
	public DbSet<Order> Orders { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@".\SQLEXPRESS;Database=SeparateConfigurations;Trusted_Connection=True;");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//modelBuilder.ApplyConfiguration(new OrderConfiguration()); // Order entity'sine ait tüm konfigürasyonları yürütür.

		modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()); // Assembly içerisindeki tüm konfigürasyonları yürütür.	
		//Bu metot sayesinde, konfigürasyonel sınıflarımızı DbContext sınıfı içerisindeki OnModelCreating metodu içerisinde tek tek çağırmamıza gerek kalmaz.


	}
}