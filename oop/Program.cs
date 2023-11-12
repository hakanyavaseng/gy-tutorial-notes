using System;

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
            
            MyClass m1 = new MyClass()
            {
                MyProperty1 = 5,
                MyProperty2 = 10
            };

            MyClass m2 = m1.With(15);

            MyRecord mr1 = new MyRecord()
            {
                MyProperty1 = 5,
                MyProperty2 = 10
            };

            MyRecord mr2 = mr1 with { MyProperty2 = 15 };




        }




    }



}

record MyRecord
{
    public int MyProperty1 { get; init; }
    public int MyProperty2 { get; init; }





}

class MyClass
{
    public int MyProperty1 { get; init; }
    public int MyProperty2 { get; init; }

    public MyClass With(int property2)
    {
        return new MyClass() 
        {
            MyProperty1 = this.MyProperty1,
            MyProperty2 = property2
        };
        
    }

}