using System.Dynamic;

#region What is ExpandoObject?
/*
    => ExpandoObject is a class used in C# to create dynamically expandable and shapeable objects.
    => With this class, we can shape an object at runtime, add, delete, or update properties and methods within it.
    => The ExpandoObject class is found within the System.Dynamic namespace. Thus, it utilizes the dynamic type.
    => Additionally, since it implements the IDictionary<string, object> interface, it stores dynamically defined properties.
    => It can be used for creating dynamic data structures, adding dynamic properties, temporary data storage, dynamic application development, etc.
*/
#endregion

#region Usage and Structure of ExpandoObject

dynamic expandoObject = new ExpandoObject();

expandoObject.Name = "Hakan";
expandoObject.Surname = "Yavas";
expandoObject.Age = 22;

expandoObject.GetFullName = new Func<string>(() => expandoObject.Name + " " + expandoObject.Surname);

//Console.WriteLine($"{expandoObject.Name}, {expandoObject.Surname}, {expandoObject.Age}");


IDictionary<string, object> expandoDictionary = (IDictionary<string, object>)expandoObject;
foreach (KeyValuePair<string, object> item in expandoDictionary)
{
    Console.WriteLine($"{item.Key} : {item.Value}");
}

#endregion
