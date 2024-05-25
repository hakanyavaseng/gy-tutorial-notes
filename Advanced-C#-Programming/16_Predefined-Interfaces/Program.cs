
using System.ComponentModel;

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

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
*/

#endregion


#region IComparable Interface
/*
//IComparable interface is used to compare two objects.

Person p1 = new Person { Name = "Hakan", Age = 22 };
Person p2 = new Person { Name = "Alperen", Age = 25 };

int result = p1.CompareTo(p2);
Console.WriteLine(result); // -1, 0 if p1 is equal, 1 if p1 is greater
public class Person : IComparable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public int CompareTo(Person? other)
    {
        return Age.CompareTo(other?.Age);
       
    }
}
*/
#endregion

#region ICloneable Interface
//ICloneable interface is used to clone a instance. Cloning is the process of creating a new object that is a copy of the current instance.
// For example if constructor takes lots of parameters, it is better to use ICloneable interface.
// Because it is not good to pass all parameters to the constructor again while creating a new instance. 

/*
Person p1 = new Person { Name = "Hakan", Age = 22 };
Person p2 = (Person)p1.Clone();

foreach (Person person in new List<Person> { p1, p2 })
{
    Console.WriteLine($"{person.Name} - {person.Age}"); // Hakan - 22, Hakan - 22
} 

// Even thoguh it's powerful, it is not recommended to use ICloneable interface. More secure and better way is to use prototype pattern.


public class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
*/
#endregion
