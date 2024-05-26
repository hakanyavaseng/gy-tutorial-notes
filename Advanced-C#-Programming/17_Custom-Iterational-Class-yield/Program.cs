using System.Collections;

Console.WriteLine();

#region Changing Collection's Elements During Iteration - Exception
/*
List<int> numbers = Enumerable.Range(0,15).ToList();
foreach (int i in numbers)
{
    Console.WriteLine(i);
    numbers.Add(100); // This will cause an exception because the collection is modified during iteration. 
    // Error: : 'Collection was modified; enumeration operation may not execute.'
}
*/
#endregion

#region How to Make a Class Iterable 
/*
Stock stock = new();
foreach (string material in stock) // Error: 'Stock' does not contain a definition for 'GetEnumerator' and no accessible extension method 'GetEnumerator' accepting a first argument of type 'Stock' could be found (are you missing a using directive or an assembly reference?)
{
    Console.WriteLine(material);
}


class Stock
{
    List<string> materials = new() { "Wood", "Steel", "Plastic", "Glass" };
    public void Add(string material) => materials.Add(material);

    public IEnumerator<string> GetEnumerator() // This method is required to make the class iterable
    {
        return materials.GetEnumerator();
    }
}
*/
#endregion

#region IEnumerable Interface
/*
// IEnumerable interface is required to make a class iterable (foreach loop), it has only one method GetEnumerator() and there is also generic version IEnumerable<T>

public class Stock : IEnumerable<string>
{
    List<string> materials = new() { "Wood", "Steel", "Plastic", "Glass" };
    public void Add(string material) => materials.Add(material);

    public IEnumerator<string> GetEnumerator()
    {
        return materials.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() // Non-generic version is implemented because object can be used in foreach loop
    {
        return materials.GetEnumerator();
    }
}
*/
#endregion

#region IEnumerator Interface
/*
class StockEnumerator : IEnumerator<string>
{
    List<string> source;
    int currentIndex = -1;
    public StockEnumerator(List<string> source) => this.source = source;


    public string Current => source[currentIndex];

    object IEnumerator.Current => source[currentIndex];

    public void Dispose() => source = null;
    public bool MoveNext() => ++currentIndex < source.Count;
    public void Reset() => source.Clear();
}
*/
#endregion

#region yield Keyword
foreach (string name in GetNames())
{
    Console.WriteLine(name);
}
IEnumerable GetNames()
{
    yield return "Hakan";
    Console.WriteLine("After Hakan");
    yield return "Alperen";
    Console.WriteLine("After Alperen");
    yield return "Mehmet";
    Console.WriteLine("After Mehmet");
    yield return "Ali";
    Console.WriteLine("After Ali");
    yield return "Veli";
    Console.WriteLine("After Veli");
}

#endregion

