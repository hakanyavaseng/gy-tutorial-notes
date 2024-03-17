using Microsoft.VisualBasic;

#region What are Anonymous Structures?
//Anonymous Type, Method, Collection, Property, Array, and Delegate are subconcepts.
//It allows us to use the respective structuring without the need to define it in advance according to the need.

/*
    => Anonymous Objects (Types): Anonymous Objects are objects that are not defined or named at compile time. They are useful when an instant and disposable data structure is needed.
    => Anonymous Methods: A feature that allows us to create an instance of a method without naming it.
    => Anonymous Collections: Anonymous Collections are collections that are not defined or named at compile time.
*/
#endregion

#region Anonymous Objects (Types)  
//It is defined with the var keyword because its name and type are unknown, so the type is determined by the compiler.

var anonymousObject = new
{
    Name = "Hakan", // (Name property)
    Surname = "Yavaş", // (Surname property)
    Age = 22 // (Age property)
};

//anonymousObject.Name = "Alperen"; //Produces an error because it is readonly.
#endregion

#region Anonymous Methods
//Anonymous Methods are typically used with delegates.

AddHandler add1 = new AddHandler((int a, int b) => a + b); //Anonymous Method
AddHandler add2 = delegate (int a, int b) { return a + b; }; //Anonymous Method
var add3 = (int a, int b) => a + b; //Anonymous Method (Since var is used, it is converted to a predefined delegate type by the compiler. In this case, Func.)

delegate int AddHandler(int num1, int num2);

#endregion

#region Anonymous Collections
//Array
var a = new[] { 1, 2, 3, 4, 5 }; //Array, int[]?

//List
var l = new Collection()
{
    new { Name = "Hakan", Surname = "Yavaş", Age = 22 }, // (Anonymous object with Name, Surname, and Age properties)
    new { Name = "Alperen", Surname = "Güneş", Age = 22 } // (Anonymous object with Name, Surname, and Age properties)
};

#endregion
