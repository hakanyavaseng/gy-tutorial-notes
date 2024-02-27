Console.WriteLine();

#region What are Generic Structures? 
/*
    => Feature that allows us to create data type-independent structures.
    => Provides performance and memory efficiency.
    => Ensures type safety to eliminate potential errors from the code.
*/
#endregion

#region Generic Classes

#region Declaration
/*
class MyClass<T>
{
    T field;
    public T Property { get; set; }
    public T Method1()
    {

    }
    public T Method2(T param)
    {

    }
}

class MyClass2<T1,T2,T3,T4> where T3 : class, new()
{
    T1 field;
    public T2 Property { get; set; }
    public T3 Method1()
    {
        return new T3();

    }
    public T3 Method2(T1 param)
    {
        return new T3();
    }
}
*/
#endregion

#region Inheritance 
// Inheriting from a generic class for a non-generic class.
/*
class NonGenericClass : GenericClass<int> // Here, specifying the type is mandatory.
{

}
class GenericClass<T>
{

}
*/


// Inheriting from a non-generic class for a generic class.
/*
class NonGenericClass
{

}
class GenericClass<T> : NonGenericClass
{

}
*/

// Inheriting from a generic class for another generic class.
/*
class GenericClass2<T> : //GenericClass1<int> 
                         //GenericClass1<T>     // Type declaration is mandatory.

{

}

class GenericClass1 <T1>
{

}
*/
#endregion
#endregion

#region Generic Methods
// Methods, even if part of a regular class, can be generic.
/*
MyClass2<int> m2 = new MyClass2<int>();

m2.Y("123", 123); // Allows providing values to A1 and A2 without the need for generic structure.


class MyClass
{
    public void X<T>()
    {

    }
}

class MyClass2<T2>
{
    public void X<T2>()
    {

    }

    public void Y<A1,A2>(A1 a, A2 b)
    {

    }

}
*/
#endregion

#region Constraints in Generic Structures
/*
class MyClass<T> where T : class
{
    public void X<T1, T2>(T1 x, T2 y) where T1 : class, new() where T2 : class, new()
    {

    }

}

/* 
    => Value Type Constraint -> where T : struct, int...
    => Reference Type Constraint -> where T : class, interface, record, struct
    => New Constraint -> Specifies that the type parameter must be an object-creatable type. new()
    => Base Class Constraint -> Specifies that the type parameter must be of the specified type or a subtype. where T : <BaseClassName>, where T : BaseEntity
    => Interface Constraint
    => Enum Constraint
    => Unmanaged Constraint
    => Not Null Constraint
    => Default Constraint -> Can be used in explicit implementation and override methods.
 
 */

#region Default Constraint
/*
interface IInterface
{
    void Y<T>();
}


class A
{
    public virtual void X<T>() { }
}

class B : A, IInterface
{
    public override void X<T>() where T : default
    { 
    }

    void IInterface.Y<T>() where T : default
    {
        throw new NotImplementedException();
    }
}
*/
#endregion
#endregion

#region Overloading with Generic Features
// With generic structures, overloading behavior can be achieved at different levels (class, interface, etc.).
/*
class MyClass 
{
    public void X() { }
    public void X<T>() { }
    public void X(int a) { }
}

class MyClass <T1>
{

}

class MyClass<T1, T2>
{

}
*/
#endregion

#region Generic Structure within Generic Structure Definition
MyClass1<A>.MyClass2<B> m = new();

class A
{

}

class B : A
{
}
class MyClass1<T1>
{
    public class MyClass2<T2> where T2 : T1 // Base class constraint
    {

    }
}

#endregion