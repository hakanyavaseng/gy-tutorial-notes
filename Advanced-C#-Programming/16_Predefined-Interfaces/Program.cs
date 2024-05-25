
Console.WriteLine();
#region IComparer Interface
/*
//IComparer interface is used to compare two objects of the same type.

Person p1 = new Person { Name = "Hakan", Age = 22 };
Person p2 = new Person { Name = "Alperen", Age = 22 };

AgeComparer comparer = new AgeComparer();
int result = comparer.Compare(p1, p2);
Console.WriteLine(result); // 0, 1 if p1 is greater, -1 if p2 is greater

// Sort the list of persons by age
List<Person> persons = new List<Person>
{
    new Person { Name = "Hakan", Age = 22 },
    new Person { Name = "Alperen", Age = 22 },
    new Person { Name = "Mehmet", Age = 20 },
    new Person { Name = "Ahmet", Age = 25 }
};

persons.Sort(new AgeComparer());
foreach (var person in persons)
{
    Console.WriteLine($"{person.Name} - {person.Age}");
} // Mehmet - 20, Hakan - 22, Alperen - 22, Ahmet - 25

public class AgeComparer : IComparer<Person>
{
    public int Compare(Person? x, Person? y)
    {
        return x?.Age.CompareTo(y?.Age) ?? 0;
        //return y?.Age.CompareTo(x?.Age) ?? 0;

    }
}
*/
#endregion


public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}