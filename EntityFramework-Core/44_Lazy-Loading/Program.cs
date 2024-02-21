
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;
using System.Runtime.CompilerServices;

ApplicationDbContext context = new();
#region Lazy Loading Nedir?
//Navigation property'ler üzerinde bir işlem yapılmaya çalışıldığı taktirde ilgili propertynin temsil ettiği ya da karşılık
//gelen tabloya özel bir sorgu oluşturulup execute edilmesini ve verilerin yüklenmesini sağlayan bir yaklaşımdır.
#endregion

//var employee = await context.Employees.findasync(2);
//Console.WriteLine(employee.Region.Name); // => Null olduğu için runtime error.

#region Proxy'lerle Lazy Loading
//Microsoft.EntityFrameworkCore.Proxies kütüphanesi yüklenmelidir.
//OnConfiguring içerisinde UseLazyLoadingProxies fonksiyonu çağrılır.
//Lazy loading ile yapılacak sorgularda lazy loading kullanabilmek için, Navigation Property'ler virtual ile işaretlenmiş olmalıdır.

/*
var employee = await context.Employees.FindAsync(2);
Console.WriteLine(employee.Region.Name); // Burada Include yapılmamasına rağmen EF Core arka planda otomatik olarak gerekli sorguyu oluşturup veriyi çeker.
*/
#endregion

#region Proxy Olmadan Lazy Loading
//Proxy'ler tüm platformlarda desteklenmeyebilir. Böyle bir durumda manuel olarak LazyLoading uygulanabilir.

//Manuel yapılan operasyonda Navigation Property'lerin virtual ile işaretlenmesine gerek yoktur.


#region ILazyLoader Interface'i İle Lazy Loading
//Microsoft.EntityFrameworkCore.Abstractions kütüphanesi gereklidir.

//var employee = await context.Employees.FindAsync(2);


#endregion

#region Delegate ile Lazy Loading
//public class Employee
//{
//	Action<object, string> _lazyLoader;
//	Region _region;
//	public Employee() { }
//	public Employee(ILazyLoader lazyLoader)
//	{

//		_lazyLoader = lazyLoader;
//	}

//	public int Id { get; set; }
//	public int RegionId { get; set; }
//	public string? Name { get; set; }
//	public string? Surname { get; set; }
//	public int Salary { get; set; }
//	public List<Order> Orders { get; set; }
//	public Region Region
//	{
//		get => _lazyLoader.Load(this, ref _region);
//		set => _region = value;
//	}
//}
//public class Region
//{
//	Action<object, string> _lazyLoader;
//	ICollection<Employee> _employees;
//	public Region() { }
//	public Region(ILazyLoader lazyLoader)
//	{
//		_lazyLoader = lazyLoader;
//	}

//	public int Id { get; set; }
//	public string Name { get; set; }
//	public virtual ICollection<Employee> Employees
//	{
//		get => _lazyLoader.Load(this, ref _employees);
//		set => _employees = value;
//	}
//}
//public class Order
//{
//	public int Id { get; set; }
//	public int EmployeeId { get; set; }
//	public DateTime OrderDate { get; set; }
//	public Employee Employee { get; set; }
//}

//static class LazyLoadingExtension
//{
//	public static TRelated Load<TRelated>(this Action<object,string> loader, object entity, 
//		ref TRelated navigation, [CallerMemberName] string navigationName = null)
//	{
//		loader.Invoke(entity, navigationName);
//		return navigation;
//	}
//}


#endregion

#endregion

#region N+1 Problemi
//Lazy Loading kullanım açısından oldukça maliyetli ve performans düşürücü etkiye sahiptir. 
/* Her bir veri için tek tek sorgu oluşturup veritabanına gönderiyor. Bu yüzden maliyetli.

var region = await context.Regions.FindAsync(1);

foreach(var employee in region.Employees)
{
	var orders = employee.Orders;
	foreach(var order in orders)
	{
        Console.WriteLine(order.OrderDate);
    }

}
*/

#endregion

public class Employee
{
	public int Id { get; set; }
	public int RegionId { get; set; }
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public int Salary { get; set; }
	public virtual List<Order> Orders { get; set; }
	public virtual Region Region { get; set; }
}
public class Region
{
	public int Id { get; set; }
	public string Name { get; set; }
	public virtual ICollection<Employee> Employees { get; set; }

}
public class Order
{
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public virtual DateTime OrderDate { get; set; }
	public virtual Employee Employee { get; set; }
}

class ApplicationDbContext : DbContext
{
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Region> Regions { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS;Database =LazyLoadingDb;Trusted_Connection = True;");

		optionsBuilder.UseLazyLoadingProxies(); // Lazy Loading with Proxies

	}
}