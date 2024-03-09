

#region Nedir?
/*
 C#'ta önceden tanımlanmış birkaç delegate türü bulunmaktadır, bu sayede yapılarına uygun kullanım senaryolarına karşın hızlıca aksiyon alınabilmektedir.
*/
#endregion

#region Action Delegate
/*
Action => Parametre almayan ve geriye değer döndürmeyen metotları temsil eder.
Action<T> => Verilen generic türde parametre alan fakat geriye değer döndürmeyen metotları temsil eder.
Action<T1, T2, T3..> => Generic türlerde parametre alan, geriye değer döndürmeyen metotları temsil eder.
*/

Action action = new(() => { Console.WriteLine("Action"); }); // public delegate void Action();
Action<bool> action2 = (b) => { Console.WriteLine("Action<T>"); };
Action<bool, int, int> action3 = (b, i1, i2) => { Console.WriteLine("Action<...>"); };
#endregion

#region Func Delegate 
/*
Func<T1> => Geriye T1 dönen metotları temsil eder.
Func<T1,T2,T3,T4 ... , Tn> // Tn dönüş türü, diğerleri parametreleri ifade eder.
*/

Func<int> func1 = () =>
{
    return 1;
};

Func<int, string> func2 = (i1) =>
{
    return "str";
};
#endregion

#region Predicate 
/*
Predicate<T> => Generic parametre alan ve bool değer döndüren metotları temsil eder.
*/

Predicate<int> predicate = b => 5 == 5;
#endregion

#region Lambda Discard Parameters
//Bazı delegate'lerin parametrelerine değer verilmek istenmediğinde kullanılabilir.
Func<int, int, string, char> func = (_, _, c) => 'a';
#endregion



