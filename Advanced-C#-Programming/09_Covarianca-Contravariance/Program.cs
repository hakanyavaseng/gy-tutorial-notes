Console.WriteLine();

#region Giriş
//Sağ - Sol Kuralı
int i = 3;
string s = "asd";
char c = 'a';

//Covariance
object[] names = new string[5] { "A", "B", "C", "D", "E" };
IEnumerable<object> cars = new List<string>() { "CAR1", "CAR2", "CAR3" };
//IEnumerable<A> As = new List<B> { new(), new() };
#endregion

#region Covariance - Contravariance Nedir?
//Bu terimler; array types, delegate types, return types ve generic types için implicit converting davranışlarını ifade eden özelliklerdir.
// => object[] names = new string[5] { "A", "B", "C", "D", "E" }; // Burada dönüşüm implicit olarak gerçekleşiyor.

/*
Covariance => Matematikte iki değişkenin birlikte nasıl değiştiğini ifade etmektedir. İki değişken aynı yönde değişiyorsa covariance denir.
Contravariance => Bu terim ise iki değişkenin birbirine karşı farklı yönde değişim göstermesine denir.
*/

/*
    => C#'ta covariance daha spesifik olan bir türün daha genel bir türün yerine kullanılabilmesidir.
    => Contravariance ise daha genel bir türün daha spesifik bir türün yerine kullanılabilmesidir.
*/
/*
//Covariance
IEnumerable<B> _b = new List<B>();
IEnumerable<A> _a = _b; //Burada IEnumerable türünde spesifik olan türün daha genel bir türe atanması gerçekleştirilmektedir.

//Contravariance
void XMethod(A a)
{

}

Action<A> aDelegate = XMethod;
Action<B> bDelegate = XMethod;

class A
{

}
class B : A 
{

}
*/
#endregion

#region Covariance  - Kullanıldığı Durumlar
#region Array Types
/*
Animal[] animals = new Cat[3];

object[] objs = new string[5];
objs[0] = 123 //Runtime error, not type safe
*/
#endregion

#region Delegate Types 
/*

Func<Animal> getAnimal = GetCat;
Cat GetCat() => new();
*/

//class Animal { }
//class Cat : Animal { }
#endregion

#region Return Types - C# 9.0
/* 

class A
{
    public virtual A X() {  return new A(); }
}

class B : A 
{
   public override B X() 
    {
        return new(); 
    }
}
*/
#endregion

#region Generic Types 
/*
IAnimal<object> objectAnimal = new Animal<string>();

interface IAnimal<out T> { }
class Animal<T> : IAnimal<T>
{

}
*/
#endregion

#endregion

