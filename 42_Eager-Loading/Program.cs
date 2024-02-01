using Microsoft.EntityFrameworkCore;
using System.Reflection;

Console.WriteLine();

ApplicationDbContext context = new();

#region Eager Loading 
//Eager loading, generate edilen bir sorguya ilişkisel verilerin parça parça eklenmesini sağlayan ve bunu yaparken iradeli/istekli bir şekilde yapmamızı sağlar.

#region Include
//Eager loading operasyonunu gerçekleştirir.
//Üretilen bir sorguya diğer ilişkisel tabloların dahil edilmesini sağlayan bir işleve sahiptir.
/*
var employees = await context.Employees
	.Include(e => e.Orders)
	.Where(o => o.Orders.Count > 2)
	.Include(e=> e.Region)
	.ToListAsync();
*/
#endregion

#region ThenInclude
//ThenInclude, üretilen sorguda Include edilen tabloların ilişkili olduğu diğer tabloları sorguya eklemek için kullanılan bir fonksiyondur.
//Eğer ki, üretilen sorguya Include edilen navigation property koleksiyonel bir property ise bu property üzerinden diğer ilişkisel tabloya erişim gösterilememektedir.
//Böyle bir durumda koleksiyonel property'lerin türlerine erişip, o tür ile ilişkili diğer tabloları da sorguya eklememizi sağlar.

/*
var orders = context.Orders 
	//.Include(o => o.Employee) => Bu satır olmasa da olur.
	.Include(o=>o.Employee.Region) // => Bu satır üsttekini de dahil eden bir sorgu oluşturuyor.
	.ToListAsync();
*/
/*
var regions = await context.Regions
	.Include(r => r.Employees) // => Çoğul nesneden tekile gidilemediği durumda üstteki gibi r.Employees.Orders gibi gidilemiyor.
	.ThenInclude(e => e.Orders) // => Burayı da 
	.ToListAsync();
*/
#endregion

#region Filtered Include 
//Sorgulama süreçlerinde Include yaparken sonuçlar üzerinde filtreleme ve sıralama gerçekleştirmeyi sağlar.
/*
var regions = await context.Regions
	.Include(r=> r.Employees.Where(e => e.Name.Contains("a")))
	.ToListAsync();
*/

// Bu fonksiyonlar ile filtere include yapılabilir. => Where, OrderBy, OrderByDescending, ThenBy, ThenByDescending, Skip, Take

/*
Change Tracker'ın aktif olduğu durumlarda; Include edilmiş sorgular üzerindeki filtreleme sonuçları beklenmeyen şekilde gelebilir.
Bu durum, daha önce sorgulanmış ve Change Tracker tarafından takip edilmiş veriler arasında filtrenin gereksinimi dışında kalan veriler için söz konusu olacaktır.
Bundan dolayı sağlıklı bir filtered include operasyonu için change tracker'ın kullanılmadığı sorgular tercih edilebilir.
*/
#endregion

#region Eager Loading için Kritik Bir Bilgi
//EF Core, önceden üretilmiş ve execute edilerek verileri belleğe alınmış sorguların verilerini, sonraki sorgularda kullanır.
/*
var orders = await context.Orders.ToListAsync();
var employees = await context.Employees.ToListAsync(); // => Burada sadece Employee'lerin bilgilerinin gelmesi beklenirken
													   // Önceki sorguda Order'lar belleğe alındığı için otomatik olarak erişilebilir vaziyette sunar.
*/
#endregion

#region AutoInclude - IgnoreAutoIncludes
/*
Uygulama seviyesinde bir entity'e karşılık yapılan tüm sorgulamalarda "kesinlikle" bir tabloya Include işlemi gerçekleştirilecekse;
bunu her bir sorgu için tek tek yapmaktansa merkezi bir hale getirmemizi sağlar.
Fluent API üzerinden sağlanır.

AutoInclude istenilmeyen sorgularda IgnoreAutoIncludes() fonksiyonu kullanılabilir.

=> var employees = await context.Employees.IgnoreAutoIncludes().ToListAsync();
*/
#endregion

#region Birbirlerinden Türetilmiş Entity'ler Arasında Include

//Cast operatörü
var persons1 = context.Persons
	.Include(p => ((Employee)p).Orders).ToListAsync();

//as operatönrü
var persons2 = context.Persons
	.Include(p => (p as Employee).Orders).ToListAsync();

//2. overload ile Include
var persons3 = context.Persons
	.Include("Orders");
#endregion
Console.WriteLine();
#endregion

public class Person
{
	public int Id { get; set; }

}
public class Employee : Person
{
	public int RegionId { get; set; }
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public int Salary { get; set; }

	public List<Order> Orders { get; set; }
	public Region Region { get; set; }
}
public class Region
{
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<Employee> Employees { get; set; }
}
public class Order
{
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public DateTime OrderDate { get; set; }

	public Employee Employee { get; set; }
}


class ApplicationDbContext : DbContext
{
	public DbSet<Person> Persons { get; set; }
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Region> Regions { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		// AutoInclude
		modelBuilder.Entity<Employee>()
			.Navigation(e => e.Region).AutoInclude();




	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS;Database = LoadingDataDb;Trusted_Connection = True;");
	}
}