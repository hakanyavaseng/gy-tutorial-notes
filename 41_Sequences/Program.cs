
using Microsoft.EntityFrameworkCore;

Console.WriteLine();
ApplicationDbContext context = new();

#region Sequence Nedir ?
//Veritabanında benzersiz ve ardışık sayısal değerler üreten veritabanı nesnesidir.
//Sequence, herhangi bir tablonun özelliği değildir, tek başına bir veritabanı nesnesidir. Birden fazla tabloda kullanılabilir.
#endregion

#region Sequence Tanımlama
//Sequence'ler üzerinden değere oluştururken veritabanına göre çalışma yapılması zaruridir.
//Sequence çağrılması veritabanına göre fark edecektir.

#region HasSequence

#endregion

#region HasDefaultValueSql
//NEXT VALUE FOR seq_name => MSSQL
#endregion

#endregion

/*VeriEkleme
await context.Employees.AddAsync(new() { Name = "Hakan", Surname = "Yavaş", Salary = 1000 });
await context.Employees.AddAsync(new() { Name = "Alperen", Surname = "Güneş", Salary = 1000 });
await context.Employees.AddAsync(new() { Name = "Emre", Surname = "Kart", Salary = 1000 });

await context.Customers.AddAsync(new() { Name = "Ceyda" });
await context.SaveChangesAsync();
*/
#region Sequence Yapılandırması
#region StartsAt
//Sequence'in kaçtan başlayacağı
#endregion
#region IncrementsBy
//Sequence'in artma miktarı
#endregion
#endregion

#region !Sequence ile Identity Farkı!
//Sequence, bir veritabanı nesnesiyken, Identity ise tabloların özellikleridir.
//Yani sequence herhangi bir tabloya bağlı değildir.
//Identity bir sonraki değeri diskten alırken, Sequence ise RAM'den alır. Bu yüzden Identity'e göre daha performanslı ve az maliyetlidir.
#endregion

#region Entities & DbContext
class Employee
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public int Salary { get; set; }
}
class Customer
{
	public int Id { get; set; }
	public string? Name { get; set; }
}
class ApplicationDbContext : DbContext
{
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Customer> Customers { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//modelBuilder.HasSequence("EC_Sequence");
		modelBuilder.HasSequence("EC_Sequence")
			.StartsAt(100)
			.IncrementsBy(5);
		
		modelBuilder.Entity<Employee>()
			.Property(e => e.Id)
			.HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");

		modelBuilder.Entity<Customer>()
			.Property(e => e.Id)
			.HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");

	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS; Database = SeqDb; Trusted_Connection = True; ");
	}
}
#endregion
