using ClassLibrary1;

#region What are Access Modifiers?
/*
    Access Modifiers are keywords used to specify the declared accessibility of a member or a type.
    There are 5 types of access modifiers in C#:
     1. Public 
     2. Private
     3. Protected
     4. Internal 
     5. Protected Internal
     6. Private Protected 
*/
#endregion

#region Public 
/*
 Public access modifier is the most permissive access level. There are no restrictions on accessing public members.
 Any public member can be accessed from outside the class.
*/

Class1 class1 = new Class1(); // Class1 is public , so it can be accessed from outside the class.
#endregion

#region Private 
/* 
 Private members are accessible only within the body of the class or the struct in which they are declared.
*/

//class1.privateInt = 10; // privateInt is private, so it can't be accessed from outside the class. (Inaccessible due to its protection level)
#endregion

#region Protected
/* 
  Protected members are accessible only within the body of the class in which they are declared, and from within any class derived from the class that declared this member.   
*/

Class2 class2 = new Class2();
class2.Print(); //X() is a protected member of Class1, so it can be accessed within the derived class with using Print() method.

#endregion

#region Internal
/*
    Internal members are accessible only within files in the same assembly. Also classes, interfaces, structs, and enums declared as internal as default. 
*/

//class1.Z(); // Z() is an internal member of Class1, so it can be accessed within the same assembly. Here Class1 and Program are not in the same assembly. So, it can't be accessed from here.


#endregion

#region Protected Internal
/*
    Protected Internal members are accessible within the same assembly and from within a derived class in any another assembly.
*/

class Class3 : Class1
{
    Class1 Class1 = new Class1();
    public void Print()
    {
       W(); // W() is a protected internal member of Class1, so it can be accessed within the derived class in another assembly.
       //Class1.W(); // Error, can not be accessed from instance of Class1.
    }
}
#endregion

#region Private Protected
/*
   Private Protected members are accessible only within the body of the class in which they are declared, and only from within its own assembly.
   It is a combination of private and protected access modifiers.
*/
class Class4 : Class1
{
    public void Print()
    {
        V(); // V() is a private protected member of Class1, so it can not be accessed within the derived class in another assembly.
    }
}


#endregion

