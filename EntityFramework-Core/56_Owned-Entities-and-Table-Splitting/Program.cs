using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region Owned Entity Types Nedir?
//EF Core, entity sınıflarını parçalayarak, property'lerini kümesel olarak farklı sınıflarda barındırılmasına ve tüm sınıfları ilgili entity'de birleştirip bütünsel olarak çalışmayı sağlar.
//Böylece bir entity, sahip olunan (owned entity) birden fazla alt sınıfın birleşmesiyle meydana gelebilmektedir

//https://www.gencayyildiz.com/blog/wp-content/uploads/2020/12/Entity-Framework-Core-Owned-Entities-and-Table-Splitting.png
#endregion

#region Owned Entity Types'ı Neden Kullanırız?
//Domain Drive Design (DDD) yaklaşımında Value Object'lere karşılık olarak kullanılır.
#endregion

#region Owned Entity Types Nasıl Uygulanır?
//Normal bir entity'de farklı sınıfların referans edilmesi primary key vb hatalara sebebiyet verecektir. Çünkü bir sınıfın referans olarak alınması
//EF Core tarafından ilişkisel tablo gibi algılanacaktır. Bu yüzden owned entity'ler EF Core'a OnModelCreating üzerinden bildirilmelidir.

#region OwnsOne Method
#endregion
#region Owned Attribute
#endregion
#region IEntityTypeConfiguration<T> Interface 
#endregion
#region OwnsMany
//OwnsMany metodu, entity'nin farklı özelliklerine başka bir sınıftan ICollection türünde navigation property aracılığıyla ilişkisel olarak erişilmesini sağlayan bir işleve sahiptir.
//Normalde Has ilişkisi olarak kurulabilecek ilişkiden farkı, Has ilişkisi için DbSet property'si gerektirirken, OwnsMany metodu DbSet'e ihtiyaç duyulmadan gerçekleştirilmesini sağlar
#endregion
#region Sınırlılıklar
/*
    =>Owned entity type için DbSet property'sine ihtiyaç yoktur.
    =>OnModelCreating üzerinde Owned Entity Type class'ı üzerinde konfigurasyon gerçekleştirilemez.
    =>Owned Entity Type'ların kalıtımsal hiyerarşi desteği yoktur.
*/
#endregion

#endregion

#region Entities
class Employee
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public EmployeeName EmployeeName { get; set; }
    public EmployeeAddress Address { get; set; }
    public ICollection<Order> Orders { get; set; }
}

class Order
{
    public string OrderDate { get; set; }
    public int Price { get; set; }
}

//[Owned]
class EmployeeName
{
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
}

//[Owned]
class EmployeeAddress
{
    public string StreetAddress { get; set; }
    public string Location { get; set; }
}
#endregion
#region IEntityTypeConfiguration
/*
class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.OwnsOne(e => e.EmployeeName, builder =>
            {
                builder.Property(e => e.Name).HasColumnName("Name"); // Isimler ozellestirmek istenirse
            });
        builder.OwnsOne(e => e.Address);
    }
}
*/
#endregion
#region DbContext 
class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region OwnsOne
        modelBuilder.Entity<Employee>()
            .OwnsOne(e => e.EmployeeName, builder =>
            {
                builder.Property(e => e.Name).HasColumnName("Name"); // Isimler ozellestirmek istenirse
            });
        modelBuilder.Entity<Employee>()
            .OwnsOne(e => e.Address);
        #endregion
        #region OwnsMany 
        modelBuilder.Entity<Employee>()
            .OwnsMany(e => e.Orders, builder =>
            {
                builder.WithOwner().HasForeignKey("OwnedEmployeeId");
                builder.Property<int>("Id");
                builder.HasKey("Id");
            });

        #endregion
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=OwnedEntityDb;Trusted_Connection=true;TrustServerCertificate=True;");

    }
}
#endregion