using System.ComponentModel;
using System.Reflection;
using System.Threading.Channels;

#region Enumeration nedir? Neden Kullanılır?
/*
 Enumeration belirli sayısal değerleri sabitleştirilmiş metinsel karşılıklarıyla temsil etmek için kullanılan bir veri türüdür.
 Kodun geliştiriciler için daha anlaşılır ve okunabilir olmasını ve belirlenen değerleri mantıklı bir şekilde gruplamayı sağlar.  
*/
#endregion

#region Enumeration Nasıl Tanımlanır?
/*
enum OrderStatus
{
    //Compiler otomatik olarak 0,1,2 şeklinde sayılar atar.
    Completed, // 0
    Failed,    // 1
    Suspended  // 2 
}
*/
#endregion

#region Enumeration'a Değer Atama 
/*
enum DatabaseType
{
    MySql,      //0
    MsSql=4     //4
    Oracle,     //5
    PostgreSQL, //6
    MongoDB     //7
}

//Conflicts!
enum DatabaseType
{
    MySql = 4   //4
    MsSql,      //5
    Oracle = 2  //2
    PostgreSQL, //3
    MongoDB     //4 
}
*/
#endregion

#region Enumeration Elemanlarına Metinsel Temsiller Ekleme

/*
OrderStatus status = OrderStatus.Completed;
var description = status.GetType()
                    .GetField(status.ToString())
                    .GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;

Console.WriteLine(description); //"Sipariş başarılı.

status.GetType()
    .GetFields()
    .Select(f =>
    {
        var descriptionAttribute = f.GetCustomAttribute<DescriptionAttribute>();
        return descriptionAttribute?.Description;
    })
    .ToList()
    .ForEach(d => Console.WriteLine(d));
                                           //Siparis basarılı.
                                           // Siparis basarısız.
                                           // Siparis askıda.
   


enum OrderStatus
{
    [Description("Sipariş başarılı.")]
    Completed,

    [Description("Sipariş başarısız.")]
    Failed,

    [Description("Sipariş askıda.")]
    Suspended,
}
*/

#endregion

#region Flags Attribute
/*
Console.WriteLine(Permissions.X | Permissions.Y | Permissions.Z);  // X, Y, Z => Toplamları 7 'yi getiren degerleri yazdirir.

[Flags]
enum Permissions
{
    X = 1,
    Y = 2,
    Z = 4,
    D = 8,
}
*/
#endregion

#region Örnek

//1
DatabaseType db = DatabaseType.MsSql;
Console.WriteLine(DatabaseType.PostgreSQL); // Prints PostgreSQL
Console.WriteLine(db); // Prints MsSql


//2
string database = "Oracle";
DatabaseType databaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), database);
Console.WriteLine();


//3
var types = Enum.GetValues(typeof(DatabaseType));
foreach (var type in types)
    Console.WriteLine(type);


enum DatabaseType
{
    MySql,
    MsSql,
    Oracle,
    PostgreSQL,
    MongoDB
}
#endregion


