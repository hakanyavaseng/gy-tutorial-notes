using System.Reflection;

Console.WriteLine();

//Getting all the types that have the CustomAttribute
Assembly assembly = Assembly.GetExecutingAssembly();
var types = assembly
    .GetTypes()
    .Where(t => t.GetCustomAttribute<CustomAttribute>() is not null)
    .ToList();

foreach (var type in types)
{
    var myAttribute = type.GetCustomAttribute<CustomAttribute>();
    Console.WriteLine($"{type.Name} - {myAttribute.MyProperty}");
}
Console.WriteLine();
#region Creating a custom attribute

[AttributeUsage(AttributeTargets.Class)]
class CustomAttribute : Attribute
{
    //Only properties can be accesed in the attribute
    public int MyProperty { get; set; }
    public int MyProperty2 { get; set; }

    public CustomAttribute(int i)
    {
        
    }
}

[CustomAttribute(5, MyProperty = MyField, MyProperty2 =50)] // OK, there is no need to write the word Attribute, compiler will add it automatically 
class CustomClass
{
    private const int MyField = 5; // Only constant values can be assigned to the attribute's properties 
    //[Custom] // Gives error because CustomAttribute only can be applied to classes
    public void Method()
    {
        Console.WriteLine("Method");
    }
}

[Custom(5, MyProperty = 5)]
public class MyClass
{
    
}

[Custom(5, MyProperty =10)]
public class MyClass2
{
    
}

public class MyClass3
{
    
}
#endregion

#region Generic Attributes

[AttributeUsage(AttributeTargets.All)]
class MyAttribute<T> : Attribute
{
    public T Value { get; set; }

}

[MyAttribute<int>(Value = 5)]
class MyClass4
{

}

#endregion
