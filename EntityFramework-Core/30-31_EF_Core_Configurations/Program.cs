
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

Console.WriteLine();

#region 30 - EF Core - Configurations

#region EF Core'da Neden Yapılandırmalara İhtiyacımız Olur?
//Default davranışları yeri geldiğinde geçersiz kılmak ve özelleştirmek isteyebiliriz. Bundan dolayı yapılandırmalara ihtiyacımız olacaktır.
#endregion

#region OnModelCreating Metodu
//EF Core'da yapılandırma deyince akla ilk gelen metot OnModelCreating metodudur.
//Bu metot, DbContext sınıfı içerisinde virtual olarak ayarlanmış bir metottur.
//Bizler bu metodu kullanarak model'larımızla ilgili temel konfigürasyonel davranışları(Fluent API) sergileyeibliriz.
//Bir model'ın yaratılışıyla ilgili tüm konfigürasyonları burada gerçekleştirebilmekteyiz.

#region GetEntityTypes
//EF Core'da kullanılan entity'leri elde etmek, programatik olarak öğrenmek istiyorsak eğer GetEntityTypes fonksiyonunu kullanabiliriz.
#endregion

#endregion

#region Configurations | Data Annotations & Fluent API

#region Table - ToTable
//Generate edilecek tablonun ismini belirlememizi sağlayan yapılandırmadır.
//EF Core normal şartlarda generate edeceği tablonun adını DbSet property'sinden almaktadır.
//Bizler eğer ki bunu özelleştirmek istiyorsak Table attribute'unu yahut ToTable api'ını kullanabilriiz.
#endregion

#region Column - HasColumnName, HasColumnType, HasColumnOrder
//EF Core'da tabloların kolonları entity sınıfları içerisindeki property'lere karşılık gelmektedir. 
//Default olarak property'lerin adı kolon adıyken, türleri/tipleri kolon türleridir.
//Eğer ki generate edilecek kolon isimlerine ve türlerine müdahale etmek isteniyorsa  bu konfigürasyon kullanılır.
#endregion

#region ForeignKey - HasForeignKey
//İlişkisel tablo tasarımlarında, bağımlı tabloda esas tabloya karşılık gelecek verilerin tutulduğu kolonu foreign key olarak temsil etmekteyiz.
//EF Core'da foreign key kolonu genellikle Entity Tanımlama kuralları gereği default yapılanmalarla oluşturulur.
//ForeignKey Data Annotations Attribute'unu direkt kullanabilirsiniz.
//Lakin Fluent api ile bu konfigürasyonu yapacaksanız iki entity arasındaki ilişkiyide modellemeniz gerekmektedir.
//Aksi taktirde fluent api üzerinde HasForeignKey fonksiyonunu kullanamazsınız!
#endregion

#region NotMapped - Ignore
//EF Core, entity sınıfları içerisindeki tüm propertyleri default olarak modellenen tabloya kolon şeklinde migrate eder.
//Entity sınıfları içerisinde tabloda bir kolona karşılık gelmeyen propertyler tanımlamak mecburiyetinde kalınabilir.
//Bu property'lerin EF Core tarafından kolon olarak map edilmesini istemediğimizi bildirebilmek için NotMapped ya da Ignore kullanabiliriz.
#endregion

#region Key - HasKey
//EF Core'da, default convention olarak bir entity'nin içerisinde Id, ID, EntityId, EntityID vs. şeklinde tanımlanan tüm proeprtylere varsayılan olarak primary key constraint uygulanır.
//Key ya da HasKey yapılanmalarıyla istediğinmiz her hangi bir property'e default convention dışında pk uygulayabiliriz.
//EF Core'da bir entity içerisinde kesinlikle PK'i temsil edecek olan property bulunmalıdır.
//Aksi taktirde EF Core migration olutşurken hata verecektir. Eğer ki tablonun PK'i yoksa bunun bildirilmesi gerekir. 
#endregion

#region Timestamp - IsRowVersion
//İleride/sonraki derlerde veri tutarlılığı ile ilgili bir ders yapacağız.
//Bu derste bir satırdaki verinin bütünsel olarak değişikliğini takip etmemizi sağlayacak olan versiyon mantığını konuşuyor olacağız.
//İşte bir verinin verisyonunu oluşturmamızı sağlayan yapılanma bu konfigürasyonlardır.
#endregion

#region Required - IsRequired
//Bir kolonun nullable ya da not null olup olmamasını bu konfigürasyonla belirleyebiliriz.
//EF Core'da bir property default olarak not null şeklinde tanımlanır.
//Eğer ki property'si nullable yapmak istyorsak türü üzerinde ?(nullable) operatörü ile bbildirimde bulunmamız gerekmektedir.
#endregion

#region MaxLength | StringLength - HasMaxLength
//Bir kolonun max karakter sayısını belirlememizi sağlar.
#endregion

#region Precision - HasPrecision
//Küsüratlı sayılarda bir kesinlik belirtmemizi ve noktanın hanesini bildirmemizi sağlayan bir yapılandırmadır.
#endregion

#region Unicode - IsUnicode
//Kolon içerisinde unicode karakterler kullanılacaksa bu yapılandırmadan istifade edilebilir.
#endregion

#region Comment - HasComment
//EF Core üzerinden oluşturulmuş olan veritabanı nesneleri üzerinde bir açıkalama/yorum yapmak istiyorsanız Comment'i kullanblirsiniz.
#endregion

#region ConcurrencyCheck - IsConcurrencyToken
//İleride/sonraki derlerde veri tutarlılığı ile ilgili bir ders yapacağız.
//Bu derste bir satırdaki verinin bütünsel olarak tutarlılığını sağlayacak bir concurrency token yapılanmasından bahsedeceğiz.
#endregion

#region InverseProperty
//İki entity arasında birden fazla ilişki varsa eğer bu ilişkilerin hangi navigation property üzerinden olacağını ayarlamamızı sağlayan bir konfigrasyondur.
#endregion

#endregion
#endregion

#region 31 - Configurations | Fluent API

#region Composite Key
//Tablolarda birden fazla kolonu kümülatif olarak primary key yapmak istiyorsak buna composite key denir.
#endregion

#region HasDefaultSchema
//EF Core üzerinden inşa edilen herhangi bir veritabanı nesnesi default olarak dbo şemasına sahiptir. Bunu özelleştirebilmek için kullanılan bir yapılandırmadır.
#endregion

#region Property

#region HasDefaultValue
//Tablodaki herhangi bir kolonun değer gönderilmediği durumlarda default olarak hangi değeri alacağını belirler.
#endregion

#region HasDefaultValueSql
//Tablodaki herhangi bir kolonun değer gönderilmediği durumlarda default olarak hangi sql cümleciğinden değeri alacağını belirler.
#endregion

#endregion

#region HasComputedColumnSql
//Tablolarda birden fazla kolondaki veirleri işleyerek değerini oluşturan kolonlara Computed Column denmektedir.
//EF Core üzerinden bu tarz computed column oluşturabilmek için kullanılan bir yapılandırmadır.
#endregion

#region HasConstraintName
//EF Core üzerinden oluşturulkan constraint'lere default isim yerine özelleştirilmiş bir isim verebilmek için kullanılan yapılandırmadır.
#endregion

#region HasData
//Sonraki derslerimizde Seed Data isimli bir konuyu incleyeceğiz.
//Bu konuda migrate sürecinde veritabanını inşa ederken bir yandan da yazılım üzerinden hazır veriler oluşturmak istiyorsak,
//Bunun yöntemini usulünü inceliyor olacağız. İşte HasData konfigürasyonu bu operasyonun yapılandırma ayağıdır.
//!HasData ile migrate sürecinde oluşturulacak olan verilerin pk olan id kolonlarına iradeli bir şekilde değerlerin girilmesi zorunludur!
#endregion

#region HasDiscriminator
//İleride entityler arasında kalıtımsal ilişkilerin olduğu TPT ve TPH isminde konuları inceliyor olacağız.
//İşte bu konularla ilgili yapılandırmalarımız HasDiscriminator ve HasValue fonksiyonlarıdır.
#region HasValue

#endregion

#endregion

#region HasField
//Backing Field özelliğini kullanmamızı sağlayan bir yapılandırmadır.
#endregion

#region HasNoKey
//Normal şartlarda EF Core'da tüm entitylerin bir PK kolonu olmak zorundadır. Eğer ki entity'de pk kolonu olmayacaksa bunun bildirilmesi gerekmektedir! İşte bunun için kullanuılan fonksiyondur.
#endregion

#region HasIndex
//Ibdex yapılanmasına dair konfigürasyonlarımız HasIndex ve Index attribute'dur.
#endregion

#region HasQueryFilter
//Global Query Filter yapılandırmasıdır.
//Temeldeki görevi bir entitye karşılık uygulama bazında global bir filtre koymaktır.
#endregion
#endregion


//[Table("Kisiler")] // Oluşturulacak tablonun ismi.
class Person
{
	//[Key]
	public int Id { get; set; }


	[ForeignKey("Department")] 
	public int DId { get; set; }

	//[Column("Adi", TypeName = "metin"), Order = 1]
	public string Name { get; set; }

	//[MaxLength(13)], [StringLength(13)], [HasMaxLength(13)]
	public string? Surname { get; set; } // [Required] durumu false olur. Çünkü string? nullable bir türdür.

	[Precision(5, 3)] // 5 basamaklı, 3 basamak ondalıklı sayı.
	public decimal Salary { get; set; }

	[NotMapped]
	public string NotMappedProp { get; set; }

	[Timestamp] // Versiyon mantığını oluşturmak için kullanılan yapılandırma.
	[Comment("Bu kolon şuna yaramaktadır.")]
	public byte[] RowVersion { get; set; }


	public DateTime CreatedDate { get; set; }
	public Department Department { get; set; }
}
class Department
{
	public int Id { get; set; }
	public string Name { get; set; }

	public ICollection<Person> Persons { get; set; }
}
public class Flight
{
	public int FlightID { get; set; }
	public int DepartureAirportId { get; set; }
	public int ArrivalAirportId { get; set; }
	public string Name { get; set; }

	public Airport DepartureAirport { get; set; }
	public Airport ArrivalAirport { get; set; }
}
public class Airport
{
	public int AirportID { get; set; }
	public string Name { get; set; }
	
	[InverseProperty(nameof(Flight.DepartureAirport))]
	public virtual ICollection<Flight> DepartingFlights { get; set; }

	[InverseProperty(nameof(Flight.ArrivalAirport))]
	public virtual ICollection<Flight> ArrivingFlights { get; set; }
}

 class Entity
{
	public int Id { get; set; }
    public string X { get; set; }
}

class A : Entity
{
	public string Y { get; set; }
}	

class B : Entity
{
	public string Z { get; set; }
}

class ApplicationDbContext : DbContext
{
	//public DbSet<Entity> Entities { get; set; }
	//public DbSet<A> As { get; set; }
	//public DbSet<B> Bs { get; set; }
	public DbSet<Person> Persons { get; set; }
	public DbSet<Department> Departments { get; set; }

	//public DbSet<Flight> Flights { get; set; }
	//public DbSet<Airport> Airports { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//Ders 30
		#region GetEntityTypes
		/*
		var entities = modelBuilder.Model.GetEntityTypes();
		foreach (var entity in entities)
		{
			Console.WriteLine(entity.Name);
		}
		*/
		#endregion

		#region ToTable
		//modelBuilder.Entity<Person>().ToTable("Kisiler"); // Tablo ismi
		#endregion

		#region Column
		/*
		modelBuilder.Entity<Person>()
			.Property(P => P.Name)
			.HasColumnName("Adi")
			.HasColumnType("metin")
			.HasColumnOrder(1); // 1. sırada olacak
		*/
		#endregion

		#region ForeignKey
		/*
		modelBuilder.Entity<Person>()
			.HasOne(p => p.Department)
			.WithMany(d => d.Persons)
			.HasForeignKey(p => p.DId);
		*/
		#endregion

		#region Ignore
		/*
		modelBuilder.Entity<Person>()
			.Ignore(p => p.NotMappedProp);
		*/
		#endregion

		#region Primary Key
		//modelBuilder.Entity<Person>()
		//    .HasKey(p => p.Id);
		#endregion

		#region IsRowVersion
		//modelBuilder.Entity<Person>()
		//    .Property(p => p.RowVersion)
		//    .IsRowVersion();
		#endregion

		#region Required
		//modelBuilder.Entity<Person>()
		//    .Property(p => p.Surname).IsRequired();
		#endregion

		#region MaxLength
		//modelBuilder.Entity<Person>()
		//    .Property(p => p.Surname)
		//    .HasMaxLength(13);
		#endregion

		#region Precision
		//modelBuilder.Entity<Person>()
		//    .Property(p => p.Salary)
		//    .HasPrecision(5, 3);
		#endregion

		#region Unicode
		//modelBuilder.Entity<Person>()
		//    .Property(p => p.Surname)
		//    .IsUnicode();
		#endregion

		#region Comment
		//modelBuilder.Entity<Person>()
		//        .HasComment("Bu tablo şuna yaramaktadır...")
		//    .Property(p => p.Surname)
		//        .HasComment("Bu kolon şuna yaramaktadır.");
		#endregion

		#region ConcurrencyCheck
		//modelBuilder.Entity<Person>()
		//    .Property(p => p.ConcurrencyCheck)
		//    .IsConcurrencyToken();
		#endregion

		//Ders 31

		#region CompositeKey
		//modelBuilder.Entity<Person>().HasKey("Id", "Id2");
		//modelBuilder.Entity<Person>().HasKey(p => new { p.Id, p.Id2 });
		#endregion

		#region HasDefaultSchema
		//modelBuilder.HasDefaultSchema("DefaultSchema");
		#endregion

		#region Property

		#region HasDefaultValue
		//modelBuilder.Entity<Person>()
		// .Property(p => p.Salary)
		// .HasDefaultValue(100);
		#endregion

		#region HasDefaultValueSql
		//modelBuilder.Entity<Person>()
		//    .Property(p => p.CreatedDate)
		//    .HasDefaultValueSql("GETDATE()");
		#endregion

		#endregion

		#region HasComputedColumnSql
		//modelBuilder.Entity<Example>()
		//    .Property(p => p.Computed)
		//    .HasComputedColumnSql("[X] + [Y]");
		#endregion

		#region HasConstraintName
		//modelBuilder.Entity<Person>()
		//    .HasOne(p => p.Department)
		//    .WithMany(d => d.Persons)
		//    .HasForeignKey(p => p.DepartmentId)
		//    .HasConstraintName("ExampleConstraint");
		#endregion

		#region HasData
		/*
		modelBuilder.Entity<Department>().HasData(
			new Department { 
				Id = 1,
				Name = "IT" },
			new Department { 
				Id = 2, 
				Name = "RE" }
			);
		
		modelBuilder.Entity<Person>()
			.HasData(
			new Person
			{
				Id = 1, // Id'yi kendimiz vermek zorundayız.
				Department = 1,
				Name = "Hakan",
				Surname = "Yavaş",
				Salary = 1000,
				CreatedDate = DateTime.Now,
			},
			new Person
			{
				Id = 2,
				Department = 2,
				Name = "Alperen",
				Surname = "Günes",
				Salary = 1000,
				CreatedDate = DateTime.Now,
			}

			);
		*/
		#endregion

		#region HasDiscriminator
		//modelBuilder.Entity<Entity>()
		//    .HasDiscriminator<int>("Ayirici")
		//    .HasValue<A>(1)
		//    .HasValue<B>(2)
		//    .HasValue<Entity>(3);

		#endregion

		#region HasField
		//modelBuilder.Entity<Person>()
		//    .Property(p => p.Name)
		//    .HasField(nameof(Person._name));
		#endregion

		#region HasNoKey
		//modelBuilder.Entity<Example>()
		//    .HasNoKey();
		#endregion

		#region HasIndex
		//modelBuilder.Entity<Person>()
		//    .HasIndex(p => new { p.Name, p.Surname });
		#endregion

		#region HasQueryFilter
		//modelBuilder.Entity<Person>()
		//    .HasQueryFilter(p => p.CreatedDate.Year == DateTime.Now.Year);
		#endregion
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
	}
}