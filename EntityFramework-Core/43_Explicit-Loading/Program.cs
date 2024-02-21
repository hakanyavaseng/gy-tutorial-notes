using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();

#region Explicit Loading
//Oluşturulan sorguya eklenecek verilerin şartlara bağlı bir şekilde yüklenmesini sağlayan bir yaklaşımdır.

/*
var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
if(employee.Name == "XXX")
{
	///
}
*/

#region Reference
//Explicit Loading sürecinde, ilişkisel olarak sorguya eklenmek istenen tablonun navigation property'si
//tekil bir türse bu tablo Reference ile sorguya eklenebilir.
/*
var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//... Yapılan işlemler
//... 
await context.Entry(employee).Reference(e => e.Region).LoadAsync(); // => Burada ihtiyaçlara istinaden Employee nesnesine Region verileri de ekleniyor.
*/
#endregion

#region Collection
//Explicit Loading sürecinde, ilişkisel olarak sorguya eklenmek istenen tablonun navigation property'si
//koleksiyonel bir türse bu tablo Collection ile sorguya eklenebilir.

/*
var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//... Yapılan işlemler
//... 
await context.Entry(employee).Collection(e => e.Orders).LoadAsync();
*/


#endregion

#region Collection'larda Aggregate Operatör Uygulamak
//Query fonksiyonu çağrıldıktan sonra aggreagate operasyonlar gerçekleştirilebilir.

/*
var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//... Yapılan işlemler
//... 
var count = await context.Entry(employee).Collection(e => e.Orders).Query().CountAsync();
*/
#endregion

#region Collection'larda Filtreleme 
/*
var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//... Yapılan işlemler
//... 
await context.Entry(employee).Collection(e => e.Orders).Query().Where(q => q.OrderDate.Day == DateTime.Now.Day).ToListAsync();
*/
#endregion


#endregion

public class Employee
{
	public int Id { get; set; }
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
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Region> Regions { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS;Database = ExplicitLoadingDb;Trusted_Connection = True;");
	}
}