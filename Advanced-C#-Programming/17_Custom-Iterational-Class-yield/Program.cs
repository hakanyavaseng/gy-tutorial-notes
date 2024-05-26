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
#endregion


