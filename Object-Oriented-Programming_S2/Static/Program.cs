//Static members can be reached by using that class' name. (Only static members)
int a = MyClass.StaticProperty;
MyClass.StaticMethod();
int b = MyClass.staticField;

//All members can be reached by using the instance of class.
MyClass m = new();

//Static structures' lifetime are same as the program's lifetime. That means static structures live and die with program.


class MyClass
{

    public int field;
    public static int staticField;
    protected static int protectedStaticField;

    public void NonStaticMethod() { }
    public static void StaticMethod() {
    //Static member can only reach static members.
    //NonStaticMethod() => No access
    StaticMethod(); // => Access
    }

    public int NonStaticProperty { get; set; }
    public static int StaticProperty { get; set; }

    //Constant variables is assigned as static by default, so there is no need to use static keyword.
    public const int constant = 0;
}

class MyClass2 : MyClass
{
    public MyClass2()
    {
        protectedStaticField = constant; // It is possible to reach protected static members in inherited class.
    }
}
//MyClass.protectedStaticField;  => Not possible


//Static classes can not be inherited, because their instances can not be created.
static class StaticClass
{
    //public StaticClass(){} => Gives error because static classes can not be newed.
   

    //Static class only contains static members, can not have non-static members.
    public static void StaticMethod(){ }

    // public void StaticMethod() => Not possible!
}

