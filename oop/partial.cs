namespace X
{
    partial class partialClass
    {

        public void A()
        {
            System.Console.WriteLine("A");
        }

    }

    partial class partialClass
    {
        public void B()
        {
            System.Console.WriteLine("B");
        }
    }


//Partial methods is used to create a method signature that will be implemented by the partial class.
partial class partialMethod
{
    partial void A();
    partial void B();
}

//Here is the implementation of the partial methods, the signatures of which are defined in the partial class.
partial class partialMethod
{
    partial void A()
    {
        System.Console.WriteLine("A");
    }
    partial void B()
    {
        System.Console.WriteLine("B");
    }
}


/*
//Partial Records
partial record r
{
}
partial record record
{
}

//Partial abstract class
abstract partial class abstractClass
{
    public abstract void A();
}

abstract partial class abstractClass
{
    public abstract void B();
}

//Partial struct
partial struct a
{
    public int a1;
    public int a2;
}
partial struct a
{
    public int b1;
    public int b2;
}
*/



}


