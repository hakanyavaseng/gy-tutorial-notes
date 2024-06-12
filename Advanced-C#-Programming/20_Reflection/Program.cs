using System.Reflection;

#region Reflection with Assembly type

Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
var types = assembly.GetTypes(); // Get all types in the assembly

#endregion

#region Reflection with Type type

MyClass m1 = new MyClass();
Type type = m1.GetType(); // Get the type of the object
Type type2 = typeof(MyClass); // Get the type of the class

#endregion

Console.WriteLine();



class MyClass
{
    public int MyProperty { get; set; }
    public int MyProperty2 { get; set; }
    public string MyProperty3 { get; set; }
    int MyProperty4 { get; set; }
    int MyField;
    public void MyMethod() { }
}