//Namespaces are used to organize code into logical groups and to prevent name collisions.

//You can not create a namespace with file scoped namespace declaration.

//Namespaces can be initialized in a namespace or .cs file

//Namespaces can be used with same name in different files
//At the end of the day, compiler will merge all the namespaces with same name into one namespace, but the namespaces must have same degree of nesting.


namespace oop.step2
{



namespace oop.step2
{
    class Program
    {
        static void Main(string[] args)
        {
            ns1.A.f();
            ns2.A.f();
        }
    }
}
//Nested namespaces
namespace ns1
{
    class A
    {
        public static void f() { System.Console.WriteLine("ns1.A.f()"); }
    }



}

namespace ns2
{
    class A
    {
        public static void f() { System.Console.WriteLine("ns2.A.f()"); }
    }
}

}