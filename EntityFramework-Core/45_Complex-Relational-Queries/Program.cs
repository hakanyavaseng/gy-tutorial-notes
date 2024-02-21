using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;


ApplicationDbContext context = new();

#region Complex Query Operators 

	#region Join 

	#region Query Syntax
	/*
	var query = from photo in context.Photos
				join person in context.Persons
				on photo.PersonId equals person.PersonId
				select new
				{
					person.Name,
					photo.Url
				};
	var await query.ToListAsync();
	*/
	#endregion

	#region Method Syntax 
	/*
	var query = context.Photos
		.Join(context.Persons, photo => photo.PersonId, person => person.PersonId, (photo, person) => new
		{
			person.Name,
			photo.Url
		});

	var datas = await query.ToListAsync();
	*/
	#endregion

	#region Multiple Columns Join

	/*Query Syntax
	var query = from photo in context.Photos
				join person in context.Persons
					on new { photo.PersonId, photo.Url } equals new { person.PersonId, Url = person.Name }
				select new
				{
					person.Name,
					photo.Url
				};
	var datas = await query.ToListAsync();
	*/

	/*Method Syntax, Name kullanılmasının nedeni varsayım. Bu yapı için benzer değerleri taşıması gerekmektedir.
	var query = context.Photos
		.Join(context.Persons, photo => new
		{

			photo.PersonId,
			photo.Url
		},
		person => new
		{
			person.PersonId,
			Url = person.Name
		},
		(photo, person) => new
		{
			person.Name,
			photo.Url
		}
		);
	*/

	#endregion

	#region 2'den Fazla Tablo ile Join

	/*Query Syntax
	var query = from photo in context.Photos
				join person in context.Persons
					on photo.PersonId equals person.PersonId
				join order in context.Orders
					on person.PersonId equals order.PersonId
				select new
				{
					person.Name,
					photo.Url,
					order.Description
				};
				
	var datas = await query.ToListAsync();
	*/

	/*Method Syntax
	var query = context.Photos
		.Join(context.Persons, photo => photo.PersonId, person => person.PersonId,
		(photo, person) => new
		{
			person.PersonId,
			person.Name,
			photo.Url
		}
		)
		.Join(context.Orders, personPhotos => personPhotos.PersonId, order => order.PersonId,
		(personPhotos, order) => new
		{
			personPhotos.Name,
			personPhotos.Url,
			order.Description
		}
		);

	var datas = await query.ToListAsync();
	*/


	#endregion


	#region Group Join -- X NOT GROUP BY X
	/* 
	var query = from person in context.Persons
				join order in context.Orders
					on person.PersonId equals order.PersonId into personOrders
				from order in personOrders
				select new
				{
					person.Name,
					order.Description
				};
	var datas = await query.ToListAsync();
	*/
	#endregion











	#endregion

	#region Left Join

	//DefaultIfEmpty() => Sorgulama sürecinde ilişkisel olarak karşılığı olmayan, yani LEFT JOIN sorgusunun oluşturulmasını sağlayan fonksiyondur.
	/*
	var query = from person in context.Persons
				join order in context.Orders
					on person.PersonId equals order.PersonId into personOrders
				from order in personOrders.DefaultIfEmpty() //Left Join operasyonu
				select new
				{
					person.Name,
					order.Description
				};

	var data = await query.ToListAsync();
	*/

	#endregion

	#region Right Join 
	//Direkt olarak RIGHT JOIN gerçekleştirilebilecek bir fonksiyon bulunmadığı için tabloların yerlerini değiştirerek yapılır.

	/*
	var query = from order in context.Orders
				join person in context.Persons
					on order.PersonId equals  person.PersonId into personOrders
				from person in personOrders.DefaultIfEmpty() //Left Join operasyonu ile önceki sorgunun RIGHT JOIN hali yapılıyor.
				select new
				{
					person.Name,
					order.Description
				};

	var data = await query.ToListAsync();
	*/

	#endregion

	#region Full Join
	/*
	var leftQuery = from person in context.Persons
				join order in context.Orders
					on person.PersonId equals order.PersonId into personOrders
				from order in personOrders.DefaultIfEmpty() //Left Join operasyonu
				select new
				{
					person.Name,
					order.Description
				};

	var rightQuery = from order in context.Orders
				join person in context.Persons
					on order.PersonId equals person.PersonId into personOrders
				from person in personOrders.DefaultIfEmpty() //Left Join operasyonu ile önceki sorgunun RIGHT JOIN hali yapılıyor.
				select new
				{
					person.Name,
					order.Description
				};


	var fullJoin = leftQuery.Union(rightQuery);

	var datas = await fullJoin.ToListAsync();
	*/
	#endregion

	#region Cross Join
	/*
	var query = from order in context.Orders
				from person in context.Persons
				select new
				{
					order,
					person
				};

	var datas = await query.ToListAsync();
	*/

	#endregion

	#region Collection Selecter'da Where Kullanma Durumu
	//Burada aslında cross join yapılsa dahi, where şartından dolayı inner join olarak yorumlanır.
	/*
	var query = from order in context.Orders
				from person in context.Persons.Where(p=> p.PersonId == order.PersonId)
				select new
				{
					order,
					person
				};

	var datas = await query.ToListAsync();
	*/
	#endregion

	#region Cross Apply
	/*
	var query = from person in context.Persons
				from order in context.Orders.Select(o => person.Name)
				select new
				{
					person,
					order
				};

	var datas = await query.ToListAsync();
	*/
	#endregion

	#region Outer Apply
	/*
	var query = from person in context.Persons
				from order in context.Orders.Select(o => person.Name).DefaultIfEmpty()
				select new
				{
					person,
					order
				};

	var datas = await query.ToListAsync();
	*/

	#endregion

#endregion

Console.WriteLine();

public class Photo
{
	public int PersonId { get; set; }
	public string Url { get; set; }

	public Person Person { get; set; }
}
public enum Gender { Man, Woman }
public class Person
{
	public int PersonId { get; set; }
	public string Name { get; set; }
	public Gender Gender { get; set; }

	public Photo Photo { get; set; }
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
	public DbSet<Photo> Photos { get; set; }
	public DbSet<Person> Persons { get; set; }
	public DbSet<Order> Orders { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		modelBuilder.Entity<Photo>()
			.HasKey(p => p.PersonId);

		modelBuilder.Entity<Person>()
			.HasOne(p => p.Photo)
			.WithOne(p => p.Person)
			.HasForeignKey<Photo>(p => p.PersonId);

		modelBuilder.Entity<Person>()
			.HasMany(p => p.Orders)
			.WithOne(o => o.Person)
			.HasForeignKey(o => o.PersonId);
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS;Database =CRQueriesDb;Trusted_Connection = True;");
	}
}