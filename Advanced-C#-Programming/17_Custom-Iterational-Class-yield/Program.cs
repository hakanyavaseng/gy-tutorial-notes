Console.WriteLine();

#region Changing Collection's Elements During Iteration - Exception
List<int> numbers = Enumerable.Range(0,15).ToList();
foreach (int i in numbers)
{
    Console.WriteLine(i);
    numbers.Add(100); // This will cause an exception because the collection is modified during iteration. 
    // Error: : 'Collection was modified; enumeration operation may not execute.'
}
#endregion


