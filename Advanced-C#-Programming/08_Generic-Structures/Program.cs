Console.WriteLine();

#region Generic Yapılar Nedir? 
/*
    =>Veri tipi bağımsız yapılar oluşturmamızı sağlayan özelliktir.
    =>Performans ve bellek tasarrufu sağlarlar.
    =>Tip güvenliği ile olası hatalardan kodu arındırırlar.
*/
#endregion

#region Generic Class'lar

#region Tanımlama
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

#region Kalıtım 
//Generic olmayan sınıfın generic sınıftan kalıtım alması.
/*
class NonGenericClass : GenericClass<int> // Burada türün verilmesi zorunludur.
{

}
class GenericClass<T>
{

}
*/


//Generic sınıfın generic olmayan sınıftan kalıtım alması.
/*
class NonGenericClass
{

}
class GenericClass<T> : NonGenericClass
{

}
*/

//Generic sınıfın generic classtan kalıtım alması
/*
class GenericClass2<T> : //GenericClass1<int> 
                         //GenericClass1<T>     // Tür bildiriminde bulunmak zorundadır.

{

}

class GenericClass1 <T1>
{

}
*/
#endregion
#endregion

#region Generic Metotlar
//Metotlar, içinde bulundukları class normal bir class olsa dahi generic yapılanmada olabilirler.

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
}





#endregion