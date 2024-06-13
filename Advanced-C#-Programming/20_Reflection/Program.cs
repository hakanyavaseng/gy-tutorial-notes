using System.Reflection;
using System.Reflection.Emit;

#region Reflection with Assembly type
/*
Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
var types = assembly.GetTypes(); // Get all types in the assembly

var myClassType = assembly.GetType(nameof(MyClass)); // Get the type of the class
*/
#endregion

#region Reflection with Type type
/*
MyClass m1 = new MyClass();
Type type = m1.GetType(); // Get the type of the object
Type type2 = typeof(MyClass); // Get the type of the class
*/
#endregion

#region Accesing members with reflection
/*
Type type = typeof(MyClass);

var methods = type.GetMethods();
var fields = type.GetFields();
var properties = type.GetProperties();

foreach (var method in methods)
{
    Console.WriteLine($"Method: {method.Name}");
}

foreach (var field in fields)
{
    Console.WriteLine($"Field: {field.Name}");
}

foreach (var property in properties)
{
    Console.WriteLine($"Property: {property.Name}");
}
*/
#endregion

#region Reading and writing properties with reflection
/*
MyClass m1 = new MyClass()
{
     MyProperty = 5
};

Console.WriteLine(m1.MyProperty);

Type type = m1.GetType();
PropertyInfo property = type.GetProperty(nameof(MyClass.MyProperty));

property.SetValue(m1, 10);

Console.WriteLine(m1.MyProperty);
*/
#endregion

#region Invoking methods with reflection
/*
MyClass m = new();

Type type = typeof(MyClass);
MethodInfo methodInfo = type.GetMethod(nameof(MyClass.MyMethod));
MethodInfo methodInfo1 = type.GetMethod(nameof(MyClass.MyMethod2));

methodInfo.Invoke(m, null);
methodInfo1.Invoke(m, new object[] { 5 });
*/

#endregion

#region What is Dynamic Methods 
/*
DynamicMethod dynamicMethod = new(
    name: "Add",
    returnType: typeof(int),
    parameterTypes: new[] { typeof(int), typeof(int) },
    m: typeof(Program).Module);

ILGenerator il = dynamicMethod.GetILGenerator();
il.Emit(OpCodes.Ldarg_0);
il.Emit(OpCodes.Ldarg_1);
il.Emit(OpCodes.Add);
il.Emit(OpCodes.Ret);

Func<int, int, int> add = (Func<int, int, int>)dynamicMethod.CreateDelegate(typeof(Func<int, int, int>));

int results = add(5, 10);

Console.WriteLine(results);
*/
#endregion


Console.WriteLine();



class MyClass
{
    public int MyProperty { get; set; }
    public int MyProperty2 { get; set; }
    public string MyProperty3 { get; set; }
    int MyProperty4 { get; set; }
    int MyField;
    public void MyMethod()  => Console.WriteLine("Hello from MyMethod");

    public void MyMethod2(int a) => Console.WriteLine($"Hello from MyMethod2 with {a}");
}