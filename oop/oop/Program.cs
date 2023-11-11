using System;
using System.Threading.Tasks.Dataflow;

namespace oop
{
    class Program
    {
        static void Main(string[] args)
        {

            MyClass my = new MyClass
            {
                A = 5
            };

            Console.WriteLine(my.A);


        }














    }


}


class MyClass
{
    readonly int a;

    public int A 
    {
        get { return a; }
        // set { a = value; }  // Error
        init { a = value; }  // OK
    }
    public int ReadOnlyProperty { get; } = 4;
    public int InitOnlyProperty { get; init; }
}
