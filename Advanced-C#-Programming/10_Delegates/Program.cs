using System.Threading.Channels;

Console.WriteLine();

#region Delegate Nedir? Ne Amaca Hizmet Etmektedir?
/*
    =>Delegate'ler metotları temsil eden yapılardır. 
    =>Metotları temsil ederek bir yandan da delegate'ler üzerinden metotlara erişebilmemize ve tetikleyebilmemize olanak sağlarlar.
    
    =>*Callback işlevleri 
      *event-based programming
      *fonksiyon parametreleri 
      *dinamik metot atamaları

    =>!!!Delagate'ler esas olarak bir nesnenin davranışının özelleştirilmesine izin vermek için kullanılır!!!
*/
#endregion

#region Delegate Nasıl Tanımlanır? Metotlar nasıl temsil edilir?
//Delegate'ler tanımlama süreçleri farklı bir söz dizimine sahip olsa dahi özünde System.Delegate sınıfından türeyen referans türlü değişkenlerdir.
//Bu yüzden delegate'i kullanabilmek için delegate'ten bir instance üretilmelidir.

/*
//Metotların Temsil Edilmesi
XHandler xDelegate = new XHandler(X);
XHandler xDelegate2 = X; //Aynı işlemi yapar.
XHandler xDelegate3 = () => { }; // Lambda
XHandler xDelegate4 = delegate //Anonymous Method
{

};

YHandler yDelegate = new YHandler(Y);
YHandler yDelegate2 = Y;

A instance = new();
YHandler yDelegate3 = (instance, p) =>
{
    return (3, 'a');
};

//Delegate ile temsil edilen metotların kullanımı
xDelegate();
xDelegate.Invoke();

(int,char) r = yDelegate3(new(), ("asd", 2));

void X()
{
    Console.WriteLine("X Function");
}

(int, char) Y(A a, (string ,int) a)
{
    Console.WriteLine("Y");
    return (3, 'a');
}

//Delegate Tanımlamaları
public delegate void XHandler();

public delegate (int,char) YHandler(A a, (string,int) p);

public class A
{

}
*/
#endregion

#region Multicast Delegate 
//Bir delegate ile birden fazla event, fonksiyon tetiklenebilir.

/*
XHandler xDelegate = () =>
{
    Console.WriteLine("Method1");
};

xDelegate += () => Console.WriteLine("Method2");
xDelegate += () => Console.WriteLine("Method3");
xDelegate += Method4;

xDelegate();

xDelegate -= Method4;

Console.WriteLine("********");

xDelegate();

Console.WriteLine("********");

var methods = xDelegate.GetInvocationList();

foreach (var method in methods)
    Console.WriteLine(method.Method.Name);



void Method4()
{
    Console.WriteLine("Method4");
}

public delegate void XHandler();
*/
#endregion

#region Generic Delegates 
//public delegate T3 XHandler<T1,T2,T3>(T1 Param, T2 Param2);
#endregion

#region Delegate ile Async Programlama - Deprecated
//Eski versiyonlarda bulunuyordu, await & async efektif olarak kullanılabildiğinden deprecated durumda.
#endregion

#region Delegate'lerde Covariance Contravariance Durumları

//Covariance
XHandler<B> bdelegate = () => new();
XHandler<A> adelegate = bdelegate;

public delegate T XHandler<out T>();

class A
{

}

class B : A
{

}
#endregion