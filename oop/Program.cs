using System;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace oop
{

    class Program
    {

        static void Main(string[] args)
        {

            #region record & class
            // MyClass m1 = new MyClass()
            // {
            //     MyProperty = 1
            // };
            // MyClass m2 = new MyClass()
            // {
            //     MyProperty = 1
            // };

            // Console.WriteLine(m1.Equals(m2)); // false


            // //Recordlar degeri on plana alir
            // MyRecord m3 = new MyRecord()
            // {
            //     MyProperty = 1
            // };
            // MyRecord m4 = new MyRecord()
            // {
            //     MyProperty = 1
            // };

            // Console.WriteLine(m3.Equals(m4)); //true
            #endregion

            #region  record & class 2

            // MyClass m1 = new MyClass()
            // {
            //     MyProperty1 = 5,
            //     MyProperty2 = 10
            // };

            // MyClass m2 = m1.With(15);

            // MyRecord mr1 = new MyRecord()
            // {
            //     MyProperty1 = 5,
            //     MyProperty2 = 10
            // };

            // MyRecord mr2 = mr1 with { MyProperty2 = 15 };

            #endregion

            #region Constructor Overloading

            //    MyClass m1 = new MyClass(1,2);

            #endregion

            #region  Destrcutor

            // useGC();
            // GC.Collect();
            // Console.ReadLine();



            #endregion

            #region Deconstruct

        //     Person p1 = new Person()
        //     {
        //         Name = "Hakan",
        //         Age = 21
        //     };

        //    var (x, y) = p1;    


            #endregion

        }

        static void useGC()
        {
            MyClass m1 = new MyClass(1, 2);

        }




    }



}

/// <summary>
/// Test Class
/// </summary>
class MyClass
{

    

    //A class can contain only one destructor.
    //Destructors cannot be inherited or overloaded.	
    //Destructors cannot be called. They are invoked automatically.
    //Destructors cannot be used with structs. They are only used with classes.
    //A destructor does not take modifiers or have parameters.
    ~MyClass()
    {
        //Destructor can not be private in order to access outside class!!
        Console.WriteLine("Destructor");
    }

    public MyClass()
    {
        //Constructors can not be private in order to access outside class if there isn't any overload!!
        Console.WriteLine("Constructor");

    }

    /// <summary>
    /// Constructor with one parameter
    /// </summary>
    /// <param name="a"></param>
    public MyClass(int a) : this()
    {
        Console.WriteLine($"Constructor with parameter a = {a}");

    }


    /// <summary>
    /// Constructor with two parameter
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public MyClass(int a, int b) : this(a)
    {
        Console.WriteLine($"Constructor with 2 parameter a = {a} b = {b}");

    }
    // public int MyProperty1 { get; init; }
    // public int MyProperty2 { get; init; }

    // public MyClass With(int property2)
    // {
    //     return new MyClass()
    //     {
    //         MyProperty1 = this.MyProperty1,
    //         MyProperty2 = property2
    //     };

    // }

}


class Person 
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void Deconstruct(out string name, out int age)
    {
        name = Name;
        age = Age;
    }



}





/// <summary>
/// Test  Record Class
/// </summary>
/// 
record MyRecord
{
    public int MyProperty1 { get; init; }
    public int MyProperty2 { get; init; }

}

