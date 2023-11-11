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
                InitOnlyProperty = 5
            };


        }














    }


}


class MyClass
{
    public int ReadOnlyProperty { get; } = 4;
    public int InitOnlyProperty { get; init; }
}
