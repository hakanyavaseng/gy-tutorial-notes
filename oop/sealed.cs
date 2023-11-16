namespace sealedKeyword
{
    //Sealed keyword is used to prevent inheritance
    sealed class Ac
    {

    }
    // class B : A{} //Gives error, because a class with sealed keyword cannot be inherited

    //Sealed keyword can ve used in classes and methods which is overriden.

    class A
    {
        public virtual void Method1()
        {
            System.Console.WriteLine("A");
        }
    }

    class B : A
    {
        public sealed override void Method1()
        {
            System.Console.WriteLine("B");
        }

    }

    class C : B
    {
        //Gives error, because a method with sealed keyword cannot be overriden
        // public override void Method1()
        // {
        //     System.Console.WriteLine("C");
        // }
    }
}