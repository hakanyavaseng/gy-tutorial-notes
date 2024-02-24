using Microsoft.EntityFrameworkCore;


#region Data Concurrency Nedir?

#endregion

#region Stale & Dirty Data Nedir?

#endregion

#region Last In Wins

#endregion

#region Pessimistic Lock

#endregion

#region Optimistic Lock

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
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DataConcurrencyDb;Trusted_Connection=true;TrustServerCertificate=True;");
    }
}

