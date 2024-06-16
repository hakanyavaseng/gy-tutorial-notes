Console.WriteLine();

/*
interface IX
{
    abstract void B();  // By default compiler will add abstract keyword
    virtual void A() { } // By default compiler will add virtual keyword to default implementation
}

class X : IX
{
    public void B()
    {
        throw new NotImplementedException();
    }
}
*/
#region Static Abstract Members in Interface
/*
IX.C(); // C Default Implementation.
//IX.B(); // A static virtual or abstract interface member can be accesed only on a type parameter.
X.A(); // X.A
X.B(); // X.B

interface IX
{
    static abstract void A(); // Normal implementation can be static abstract
    static abstract void B();
    static void C() // static keyword can be used in default implementation
    {
        Console.WriteLine("C Default Implementation.");

    }
}

class X : IX
{
    public static void A()
    {
        Console.WriteLine("X.A");
        
    }

    public static void B()
    {
        Console.WriteLine("X.B");
       
    }
}
*/
#endregion

#region Static Abstract Members in Interface with Generic Types
/*
class X<T> where T : IX
{
    public X()
    {
        T.A();
        T.B();
        //T.C(); // Gives error, because with generic type only static abstract members can be accessed.
    }
}


interface IX
{
    static abstract void A();
    static abstract void B();
    static void C()
    {
        Console.WriteLine("C Default Implementation.");

    }
}
*/
#endregion

