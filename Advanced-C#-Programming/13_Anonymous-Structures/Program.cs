using Microsoft.VisualBasic;


#region Anonymous Structures Nedir?
//Anonymous Type, Method, Collection, Property, Array ve Delegate gibi alt kavramlar mevcuttur.
//İhtiyaç doğrultusunda ilgili yapılanmayı önceden tanımlamaya gerek kalmaksızın kullanmamıza olanak sağlar.

/*
    => Anonymous Objects (Types) : Anonymous Objects, derleme zamanında tanımlanmayan ve adlandırılmayan nesnelerdir. Anlık ve tek kullanımlık veri yapısı oluşturulması gereken durumlarda oldukça kullanışlıdır.
    => Anonymous Methods : Bir isim vermeksizin bir metodun örneğini oluşturmamızı sağlayan özelliktir. 
    => Anonymous Collections : Anonymous Collections, derleme zamanında tanımlanmayan ve adlandırılmayan koleksiyonlardır. 
*/
#endregion

#region Anonymous Objects (Types)  
//var keywordu ile tanımlanır, çünkü isim ve türü belli olmadığından compiler tarafından türü belirlenir.


var anonymousObject = new 
{ 
    Name = "Hakan",
    Surname = "Yavaş",
    Age = 22 
};

//anonymousObject.Name = "Alperen"; //Hata verir, çünkü readonly'dir.
#endregion

#region Anonymous Methods
//Anonymous Methods, genellikle delegate'lerle beraber kullanılmaktadır. 


AddHandler add1 = new AddHandler((int a, int b) => a + b); //Anonymous Method
AddHandler add2 = delegate(int a, int b) { return a + b; }; //Anonymous Method
var add3 = (int a, int b) => a + b; //Anonymous Method (Compiler var kullanıldığı için hazır olarak bulunan bir delegate türüne dönüştürür. Bu durumda Func.)

delegate int AddHandler(int num1, int num2);

#endregion

#region Anonymous Collections
//Array
var a = new[] { 1, 2, 3, 4, 5 }; //Array, int[]?

//List
var l = new Collection()
{
    new { Name = "Hakan", Surname = "Yavaş", Age = 22 },
    new { Name = "Alperen", Surname = "Güneş", Age = 22 }
};

#endregion