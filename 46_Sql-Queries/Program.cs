using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();

/* Eğer ki sorgularımızı LINQ ile ifade edemiyorsak, veya LINQ sorgusuna göre daha optimize bir sorguyu manuel olarak geliştirmek 
	ve EF Core üzerinden execute etmek isteniyorsa manuel işlemler de yapılabilir 
 
	Manuel şekilde oluşturulmuş sorguların EF Core tarafından execute edebilmek için o sorgunun sonucunu karşılayacak bir entity model'ın tasarlanması
	ve bunun DbSet olarak context nesnesine tanımlanmış olması gerekmektedir.
*/

#region FromSqlInterpolated 
//EF Core 7.0 öncesinde ham sorguların execute edilebildiği fonksiyondur.

/*
var persons = await context.Persons.FromSqlInterpolated($"SELECT * FROM Persons") //Parametre olarak formattable string alır bu yüzden string interpolation kullanılır.
					.ToListAsync();
*/
#endregion

#region FromSql - EF Core 7.0 

#region Query Execute
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons").ToListAsync();
#endregion

#region Stored Procedure Execute
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_getAllPersons NULL").ToListAsync();
#endregion

#region Parametreli Sorgu Oluşturma 
//int personId = 3;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons WHERE PersonId = {personId}").ToListAsync();

//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_getAllPersons {personId}").ToListAsync();

//Burada sorguya geçirilen personId değişkeni arkaplanda bir DbParameter türüne dönüştürülerek sorguya dahil edilmektedir. (SqlParameter)

#region SqlParameter (DbParameter base'inden)
//Manuel olarak optimize ediyoruz. Mikro düzeyde daha performanslı.
/*
SqlParameter personId = new("PersonId", 3);
personId.DbType = System.Data.DbType.Int32;
personId.Direction = System.Data.ParameterDirection.Input;

var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_getAllPersons {personId} ").ToListAsync();
*/
#endregion

//Stored procedure'un beklemiş olduğu parametreyi yazarak birden çok parametreli durumlarda ayrım yapabiliriz.
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_getAllPersons @PersonId = {personId} ").ToListAsync();

#endregion

#endregion

#region Dynamic SQL Oluşturma ve Parametre Verme - FromSqlRaw
/*
string columnName = "PersonId", value = "3";
var persons = await context.Persons.FromSql($"SELECT * FROM Persons WHERE {columnName} = {value}")
	.ToListAsync();

//Sorgu'nun string olarak oluşturduğu değerde bir sorun olmamasına rağmen,
//Dynamic olarak oluşturulan sorgularda columName bir parametreden alındığı için EF Core bu sorguyu execute etmez!
*/

//--------------
string columnName = "PersonId";
SqlParameter value = new("PersonId", "3");
var persons = await context.Persons.FromSqlRaw($"SELECT * FROM Persons WHERE {columnName} = @PersonId", value)
	.ToListAsync();

//FromSql ve FromSqlInterpolated metotlarında SQL Injection vb. güvenlik önlemleri alınmış vaziyettedir. Lakin dinamik olarak oluşturulan 
//sorgularda burada güvenlikten geliştirici sorumludur. Yani gelen sorguda yorumlar, noktalı virgüller veya SQL'e özel karakterlerin algılanması
//ve bunların temizlenmesi geliştirici tarafından gerçekleştirilir.
#endregion

#region SqlQuery - Entity Olmayan Scalar Sorguların Çalıştırılması - Non Entity - EF Core 7.0
/*
var data = await context.Database.SqlQuery<int>($"SELECT PersonId From Persons")
	.ToListAsync();
*/

/*
//Runtime error!
var persons = await context.Persons.FromSql($"SELECT PersonId FROM Persons")
	.Where(x => x > 5)
	.ToListAsync();

//=> Arkaplanda subquery olarak üretildiği için bu sorgunun çalışabilmesi için aşağıdaki gibi olmalıdır.

var persons = await context.Persons.SqlQuery<int>($"SELECT PersonId Value FROM Persons")
	.Where(x => x > 5) // Default olarak Value'ya göre çalışır.
	.ToListAsync();
*/
#endregion

#region ExecuteSql
//Insert, update, delete
//context.Database.ExecuteSqlAsync($"UPDATE Persons SET Name = 'Hakan' WHERE PersonId = 1"); //Burada SaveChanges çağrılmamaktadır. 
#endregion

#region Sınırlılıklar 
/*
//Query'ler entity türünün tüm özellikleri için kolonlarda değer döndürmelidir. 
var persons = await context.Persons.FromSql($"SELECT Name FROM Persons") //Tek bir kolona göre değer döndürüldüğü için runtime error!
	.ToListAsync();

//Sütun isimleri property isimleriyle aynı olmalıdır.
//SQL Sorgusu JOIN yapısı içeremez, bu ihtiyaç durumunda Include fonksiyonu kullanılmalıdır.
Ancak şu şekilde kullanılır =>

var persons = await context.Persons.FromSql($"SELECT * FROM Persons")
	.Include(p=>p.Orders)
	.ToListAsync();
*/
#endregion

public class Person
{
	public int PersonId { get; set; }
	public string Name { get; set; }

	public ICollection<Order> Orders { get; set; }
}
public class Order
{
	public int OrderId { get; set; }
	public int PersonId { get; set; }
	public string Description { get; set; }
	public Person Person { get; set; }
}

class ApplicationDbContext : DbContext
{
	public DbSet<Person> Persons { get; set; }
	public DbSet<Order> Orders { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS;Database =SqlQueriesDb;Trusted_Connection = True;");
	}
}