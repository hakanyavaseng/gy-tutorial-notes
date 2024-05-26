using System.Collections;

Console.WriteLine();

#region Custom Collection Initializers
//MyClass m = new() { "a", "b","c" }; // Error: Cannot initialize type 'MyClass' with a collection initializer because it does not implement 'System.Collections.IEnumerable' Without this parantehsis is used as object initializer

MyClass m = new() { 1, 2, 3 }; // Error: MyClass does not contain a definition for 'Add' and no accessible extension method 'Add' accepting a first argument of type 'MyClass' could be found (are you missing a using directive or an assembly reference?)

MyClass m2 = new()
{
    { 1, 2, true },
    { 3, 4, false}
};

foreach (var item in m)
{
    Console.Write(item + " ");
}

class MyClass : IEnumerable<int>
{
    List<int> numbers = new();
    public void Add(int number)
        => numbers.Add(number);

    public void Add(int number, int number2, bool b)
    {

    }

    public int MyProperty { get; set; }
    public int MyProperty2 { get; set; }
    public int MyProperty3 { get; set; }

    public IEnumerator<int> GetEnumerator()
    {
        return numbers.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return numbers.GetEnumerator();
    }
}
#endregion
