using System.Dynamic;

Console.WriteLine();

#region Object Creation with Activator Class
Type type = typeof(MyClass);
MyClass m = (MyClass)Activator.CreateInstance(type);
class MyClass
{
    public MyClass()
    {
        Console.WriteLine($"{nameof(MyClass)} instance created!");
    }
}

#endregion

#region Object Creation with Dynamic Object Class
// Dynamic Object allows a class to exhibit dynamic behavior.

dynamic my = new MyClass2();
my.DynamicProperty1 = 123;
my.DynamicProperty2 = "123";

Console.WriteLine(my.DynamicProperty1); // 123

class MyClass2 : DynamicObject
{
    public MyClass2()
    {
        Console.WriteLine($"{nameof(MyClass2)} instance created!");
    }
    private readonly Dictionary<string, object> properties = new();

    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        properties.Add(binder.Name, value);
        return true;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        return properties.TryGetValue(binder.Name, out result);
    }
}

#endregion

#region Object Creation with ExpandoObject using the dynamic keyword

// https://www.gencayyildiz.com/blog/c-expandoobject-ile-dinamik-nesne-olusturma/

dynamic instance = new ExpandoObject();
instance.DynamicProperty1 = 123;
instance.DynamicProperty2 = "124";

Console.WriteLine($"{instance.DynamicProperty1}, {instance.DynamicProperty2}");
#endregion