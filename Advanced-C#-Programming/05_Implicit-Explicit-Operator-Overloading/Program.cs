Console.WriteLine();

#region Implicit & Explicit Operator Overloading
A a = new B(); // Compiler error if there is no operator overloading explicit or implicit
A a2 = (A)new B();

B b = (B)new A();

class A
{
    public static implicit operator A(B i)
    {
        return new A()
        {
            //If there is properties in i, assigning here.
        };
    }

}

class B
{
    public static explicit operator B(A i)
    {
        return new B();

    }
}
#endregion

#region Best Practises
/*
    => In conversion processes, complex operations can affect performance, so unnecessary type conversions should be avoided.
    => The use of implicit conversions can reduce code readability. Therefore, implicit conversions should be documented.
    => When working with nullable values in both implicit and explicit conversions, sending an appropriate default value instead of null can help prevent errors.
    => Testing is important in critical conversions.
    => If the program has high performance requirements and conversions are impacting performance, optimization is necessary.
*/

#endregion