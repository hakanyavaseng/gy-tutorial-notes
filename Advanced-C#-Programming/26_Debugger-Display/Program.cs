using System.Diagnostics;


Person person = new()
{
    Name = "Hakan",
    Age = 23,
    City = "Eskisehir",
    Region = "Anatolia"
};

var p = person;

Console.WriteLine();

#region DebuggerDisplay Attribute
//DebuggerDisplay Attribute gives opportunity to customize the display of the object in the debugger. So, the developer can see the properties of the object in the debugger window without expanding the object.

[DebuggerDisplay("Name = {Name}, Age = {Age}, Şehir = {City}, Bölge = {Region}")]
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
}
#endregion