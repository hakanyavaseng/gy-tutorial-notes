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

#region Static Abstract Operators and Generic Types

class XX<T> where T : IX, new()
{
    public XX()
    {
        T t1 = new T();
        T t2 = new T();
        _ = t1 + t2; // Operators can be used with generic types.
        _ = t1 - t2;
        _ = t1 * t2;
        _ = t1 / t2;
    }
   
}

class X : IX
{
    public int Value => 5;

    static int IX.operator +(IX a) => a.Value + 1;

    static int IX.operator +(IX a, IX b) => a.Value + b.Value;
   
    static int IX.operator +(IX x, A a)
    {
        throw new NotImplementedException();
    }

    static int IX.operator +(IX x, B b)
    {
        throw new NotImplementedException();
    }

    static int IX.operator -(IX a)
    {
        throw new NotImplementedException();
    }

    static int IX.operator -(IX a, IX b)
    {
        throw new NotImplementedException();
    }

    static long IX.operator *(IX a, IX b)
    {
        throw new NotImplementedException();
    }

    static decimal IX.operator /(IX a, IX b)
    {
        throw new NotImplementedException();
    }
}

interface IX
{
    int Value { get; }
    static abstract int operator +(IX a);
    static abstract int operator -(IX a);
    static abstract int operator +(IX a, IX b);
    static abstract int operator -(IX a, IX b);
    static abstract long operator *(IX a, IX b);
    static abstract decimal operator /(IX a, IX b);
    static abstract int operator +(IX x, A a);
    static abstract int operator +(IX x, B b);

}

class A { public int a; }
class B { public int b; }

#endregion





