Console.WriteLine();

#region Extension Methods 

MyClass m = new();
m.Print();

static class MyExtensions
{
    public static void Print(this MyClass myClass)
    {
        Console.WriteLine("Print() method called");
    }
}


#endregion

class MyClass
{

}