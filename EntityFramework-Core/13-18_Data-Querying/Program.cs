using Microsoft.EntityFrameworkCore;

ETicaretContext context = new ETicaretContext();

#region Bir sorguyu execute etmek için ne yapılır?
#region ToListAsync()
#region Method Syntax
/*
var urunler = await context.Urunler.ToListAsync();
*/
#endregion
#region Query Syntax, daha detaylı islenebilir.
/*
var urunler2 = await (from urun in context.Urunler
               select urun).ToListAsync();
*/
#endregion
#endregion
#region Foreach

/*
var urunler = from urun in context.Urunler
                   select urun;

 foreach(var urun in urunler)
 {
     Console.WriteLine(urun.UrunAdi);
 }
*/
#endregion
#endregion

#region IQueryable & IEnumerable nedir?
/*
var urunler = from urun in context.Urunler // IQueryable
              select urun;

var urunler = (from urun in context.Urunler // IEnumerable
              select urun).ToListAsync();
*/
#region IQueryable
//EF Core üzerinden yapılan sorguların execute edilmemiş halidir.
#endregion
#region IEnumerable
//Sorguların execute edilip verilerin in-memory'e alındığı halidir.
#endregion
#endregion

#region Deferred Execution
//IQueryable çalışmalarında ilgili kod yazıldığı noktada çalıştırılmaz. Çalıştırıldığı noktada tetiklenir. Bu işleme Deferred Execution denir.
/*
int urunId = 1;

var urunler = from urun in context.Urunler
              where urun.Id == urunId
              select urun;

urunId = 2;

foreach(var urun in urunler) //Sorgu öncelikle 1 olan Id'ye göre oluşturulur. Id 2'ye çevrilirse 1 Id'li ürün yerine 2 Id'li ürün gelir.
                             //Çünkü sorgu foreach kısmına geldiğinde oluşturulur.
{
    Console.WriteLine(urun.UrunAdi);
}
*/
#endregion

#region Çoğul veri getiren sorgular

#region Where
/*
    Oluşturulan sorguya where şartı eklemek için kullanılır.

    //Query Syntax
    var urunler = from urun in context.Urunler
                  where urun.Fiyat > 1000
                  select urun;

    var urunler = from urun in context.Urunler
                  where urun.Fiyat > 1000 && urun.UrunAdi.StartsWith("A")
                  select urun;

    var urunler = from urun in context.Urunler
                  where urun.Fiyat > 1000 || urun.UrunAdi.EndsWith("B")
                  select urun;

    //Method Syntax
    var urunler = await context.Urunler.Where(x => x.Fiyat > 1000).ToListAsync();
    var urunler = await context.Urunler.Where(x => x.Fiyat > 1000).Where(x => x.UrunAdi.Contains("Bilgisayar")).ToListAsync();
    var urunler = await context.Urunler.Where(x => x.Fiyat > 1000 && x.UrunAdi.StartsWith("A")).ToListAsync();
    var urunler = await context.Urunler.Where(x => x.Fiyat > 1000 || x.UrunAdi.EndsWith("B")).ToListAsync();
*/
#endregion

#region OrderBy
/* 
  Sorgu sonucunda gelen verileri istenilen kritere göre sıralamak için kullanılır.

  //Method Syntax
  var urunler = await context.Urunler.OrderBy(x => x.Fiyat).ToListAsync(); //Artan sıralama
  var urunler = await context.Urunler.OrderByDescending(x => x.Fiyat).ToListAsync(); //Azalan sıralama

  //Query Syntax
  var urunler = from urun in context.Urunler
                orderby urun.Fiyat
                select urun;

  var urunler  from urun in context.Urunler
                orderby urun.Fiyat descending
                select urun;
*/

#region ThenBy & ThenByDescending
/*
    ThenBy: OrderBy üzerinde yapılan sıralama işlemini farklı kolonlara da uygulamak için kullanılır.
    ThenByDescending: OrderByDescending üzerinde yapılan sıralama işlemini farklı kolonlara da uygulamak için kullanılır.

    var urunler = context.Urunler.OrderBy(x => x.Fiyat).ThenBy(x => x.UrunAdi).ToListAsync();
*/
#endregion
#endregion

#endregion

#region Tekil veri getiren sorgular

#region SingleAsync
/*
    Yapılan sorgu sonucunda tek bir veri döndürülmesi isteniyorsa kullanılır.
    Eğer sorgu sonucunda birden fazla veri geliyorsa ya da hiç veri gelmiyorsa "exception" fırlatır.

    var urun = await context.Urunler.SingleAsync(x => x.Id == 1); // Hata vermez.
    var urun = await context.Urunler.SingleAsync(x => x.Id == 100); // Hata verir, 100 Id'li ürün yok.
    var urun = await context.Urunler.SingleAsync(x => x.Fiyat > 1000); // Hata verir, birden fazla ürün var.
*/
#endregion

#region SingleOrDefaultAsync
//Eğer sorgu sonucunda birden fazla veri geliyorsa "exception" fırlatır. Eğer hiç veri gelmiyorsa null döner.
/*
     var urun = await context.Urunler.SingleOrDefaultAsync(x => x.Id == 1); // Hata vermez.
     var urun = await context.Urunler.SingleOrDefaultAsync(x => x.Id == 100); // Hata vermez, null döner. 100 Id'li ürün yok.
     var urun = await context.Urunler.SingleOrDefaultAsync(x => x.Fiyat > 1000); // Hata verir, birden fazla ürün var.
*/
#endregion

#region FirstAsync
/*
    Yapılan sorguda elde edilen verilerden ilkini döndürür. Eğer sorgu sonucunda hiç veri gelmiyorsa "exception" fırlatır.

    var urun = await context.Urunler.FirstAsync(x => x.Id == 1); // Hata vermez.
    var urun = await context.Urunler.FirstAsync(x => x.Id == 100); // Hata verir, 100 Id'li ürün yok.
    var urun = await context.Urunler.FirstAsync(x => x.Fiyat > 2); // Hata vermez, birden fazla ürün var bunlardan koşula uyan ilk ürün döner.

*/
#endregion

#region FirstOrDefaultAsync
/*
    Yapılan sorguda elde edilen verilerden ilkini döndürür. Eğer sorgu sonucunda hiç veri gelmiyorsa null döner.

    var urun = await context.Urunler.FirstOrDefaultAsync(x => x.Id == 1); // Hata vermez.   
    var urun = await context.Urunler.FirstOrDefaultAsync(x => x.Id == 100); // Hata vermez, null döner. 100 Id'li ürün yok.
    var urun = await context.Urunler.FirstOrDefaultAsync(x => x.Fiyat > 2); // Hata vermez, birden fazla ürün var bunlardan koşula uyan ilk ürün döner.
*/
#endregion

#region FindAsync
/*
    //Primary Key üzerinden bir sorgulama yapılacaksa kullanılabilir.
    //Sorgu sonucunda veri bulunursa veriyi döndürür. Eğer veri bulunmazsa null döner.
    //Sorgulama sürecinde önce context üzerinde arama yapılır. Eğer veri bulunmazsa veritabanına gidilir. Bu işlem performans açısından önemlidir.

    var urun = await context.Urunler.FindAsync(1); // Hata vermez.
    var urun = await context.Urunler.FindAsync(100); // Hata vermez, null döner. 100 Id'li ürün yok.

    //Composite Key durumu
    var urunParca = await context.UrunParcalar.FindAsync(1, 1); 

*/
#endregion

#region LastAsync
/*
     //Sorgu sonucunda elde edilen verilerden sonuncusunu döndürür. Eğer sorgu sonucunda hiç veri gelmiyorsa "exception" fırlatır.
     //Kullanabilmek için sorgu sonucunda elde edilen verilerin sıralanmış olması gerekir. (OrderBy, OrderByDescending, ThenBy, ThenByDescending)

     var urun = await context.Urunler.OrderBy(x => x.Fiyat).LastAsync();
     var urun = await context.Urunler.OrderBy(x => x.Fiyat).LastAsync(x => x.Fiyat > 1000);
*/

#endregion

#region LastOrDefaultAsync
/*
    //Sorgu sonucunda elde edilen verilerden sonuncusunu döndürür. Eğer sorgu sonucunda hiç veri gelmiyorsa null döner.
    //Kullanabilmek için sorgu sonucunda elde edilen verilerin sıralanmış olması gerekir. (OrderBy, OrderByDescending, ThenBy, ThenByDescending)

    var urun = await context.Urunler.OrderBy(x => x.Fiyat).LastOrDefaultAsync();
    var urun = await context.Urunler.OrderBy(x => x.Fiyat).LastOrDefaultAsync(x => x.Fiyat > 1000); 
*/
#endregion
#endregion

#region Diğer sorgulama fonksiyonları

#region CountAsync
/*
    //Sorgu sonucunda elde edilen verilerin sayısını döndürür.
    //Int tipinde bir değer döndürür.

    var urunSayisi = await context.Urunler.ToListAsync().Count(); //Verileri in-memory'e alır ve sayar. Performans açısından maliyetlidir.
    var urunSayisi = await context.Urunler.CountAsync();  // Veritabanında sorgu çalıştırır ve sayar. Performans açısından daha iyidir.
    var urunSayisi = await context.Urunler.CountAsync(x => x.Fiyat > 1000);
*/
#endregion

#region LongCountAsync
/*
    //Sorgu sonucunda elde edilen verilerin sayısını döndürür.
    //Long tipinde bir değer döndürür 

    var urunSayisi = await context.Urunler.ToListAsync().LongCount(); //Verileri in-memory'e alır ve sayar. Performans açısından maliyetlidir.
    var urunSayisi = await context.Urunler.LongCountAsync();  // Veritabanında sorgu çalıştırır ve sayar. Performans açısından daha iyidir.
    var urunSayisi = await context.Urunler.LongCountAsync(x => x.Fiyat > 1000);
*/
#endregion

#region AnyAsync
/*
    //Sorgu sonucunda elde edilen verilerden herhangi biri var mı yok mu kontrol eder.
    //Bool tipinde bir değer döndürür.
    //SQL'deki "EXISTS" operatörüne karşılık gelir.

    var isExist = await context.Urunler.AnyAsync(); 
    var isExist = await context.Urunler.AnyAsync(x => x.Fiyat > 1000);
*/
#endregion

#region MaxAsync
/*
    //Sorgu sonucunda elde edilen verilerden en yüksek olanı getirir.
    var maxFiyat = await context.Urunler.MaxAsync(x => x.Fiyat);
*/
#endregion

#region MinAsync
/*
    //En düşük olanı getirir.
    var minFiyat = await context.Urunler.MinAsync(x => x.Fiyat);
*/
#endregion

#region Distinct
/*
    //Sorgu sonucunda elde edilen verilerden tekrar edenleri tekilleştirir ve döndürür.
    var urunler = await context.Urunler.Distinct().ToListAsync();
*/
#endregion

#region AllAsync
/*
    //Sorgu sonucunda elde edilen verilerin hepsinin koşula uyup uymadığını kontrol eder.
    //Bool tipinde bir değer döndürür    
    
    var isAllGreaterThan1000 = await context.Urunler.AllAsync(u => u.Fiyat > 1000);
*/
#endregion

#region SumAsync
/*
    //Verilen sayısal prop'un toplamını alır.
    var toplamFiyat = await context.Urunler.SumAsync(x => x.Fiyat);
*/

#endregion

#region AverageAsync
/*
    //Verilen sayısal prop'un ortalamasını alır.
    var ortalamaFiyat = await context.Urunler.AverageAsync(x => x.Fiyat);
    var ortalamadanBuyukUrunler = await context.Urunler.Where(x => x.Fiyat > context.Urunler.AverageAsync(x => x.Fiyat)).ToListAsync();
*/
#endregion

#region ContainsAsync
/*
    //Like '%%' operatörüne karşılık gelir.
    var urunler = await context.Urunler.Where(x => x.UrunAdi.Contains("Bilgisayar")).ToListAsync();
*/
#endregion

#region StartsWith & EndsWith
/*
    var urunler = await context.Urunler.Where(x => x.UrunAdi.StartsWith("A")).ToListAsync();
    var urunler = await context.Urunler.Where(x => x.UrunAdi.EndsWith("B")).ToListAsync();
*/
#endregion

#endregion

#region Sorgu Sonucu Dönüşüm Fonksiyonları

#region ToDictionaryAsync
/*
    //Sorgu sonucunda elde edilen verileri dictionary'e çevirir.
    //ToList ile aynı amaca hizmet eder.
    //ToList verileri entity türünde bir koleksiyona List<TEntity> çevirirken ToDictionary verileri Dictionary<TKey, TValue> çevirir.

    var urunler = await context.Urunler.ToDictionaryAsync(u => u.UrunAdi, u => u.Fiyat);

*/
#endregion

#region ToArrayAsync
/*
    //Sorgu sonucunda elde edilen verileri array'e çevirir.
    //ToList ile aynı amaca hizmet eder.
    //ToList verileri entity türünde bir koleksiyona List<TEntity> çevirirken ToArray verileri TEntity[] çevirir.

    var urunler = await context.Urunler.ToArrayAsync();
 
*/
#endregion

#region Select
/*
    //Sorgu sonucunda elde edilen verilerden sadece istenilen kolonların alınmasını sağlar.
    //Select ile sorgu sonucunda elde edilen verilerin sadece istenilen kolonları alınır. 
    //Bu sayede veritabanından daha az veri çekilir ve performans artar.
  
    var urunler = await context.Urunler.Select(u => new Urun { u.UrunAdi, u.Fiyat }).ToListAsync();

    //Select fonksiyonu gelen verileri farklı türlerde karşılamamızı da sağlar. T, anonim

    var urunler = await context.Urunler.Select(u => new { u.UrunAdi, u.Fiyat }).ToListAsync();
*/
#endregion

#region SelectMany
/* 
    //Select ile aynı amaca hizmet eder. Lakin, ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon etmeyi sağlar.

    var urunler = await context.Urunler.Include(u => u.Parcalar).SelectMany(u=> u.Parcalar, (u, p) => new 
    { u.UrunAdi, 
      p.ParcaAdi 
    }).ToListAsync();
*/

#endregion

#region GroupBy
/*
    //Verileri üzerinde gruplama yapmayı sağlar.

    //Method Syntax
    var datas = await context.Urunler.GroupBy(u => u.Fiyat).Select(group => new
    {
        Fiyat = group.Key,
        Urunler = group.ToList()
    }).ToListAsync();

    //Query Syntax
    var datas = await (from urun in context.Urunler
                       group urun by urun.Fiyat into g
                       select new
                       {
                           Fiyat = g.Key,
                           Urunler = g.ToList()
                       }).ToListAsync();
 
*/
#endregion

#endregion




#region DbContext
public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }

    public DbSet<UrunParca> UrunParcalar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ETicaret;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(x => new { x.UrunId, x.ParcaId });
    }

}
#endregion
#region Entity
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public ICollection<Parca> Parcalar { get; set; }
}

public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}

public class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }
    public Urun Urun { get; set; }
    public Parca Parca { get; set; }
}

#endregion

