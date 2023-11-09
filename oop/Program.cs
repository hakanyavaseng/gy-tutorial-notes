//GY-OOP-TUTORIALS
namespace oop
{
    class Program
    {
        static void Main(string[] args)
        {
            //myClass o1 = new myClass();
            myClass o1 = new(); // C# 9.0
            o1.MyProperty = 1;

            myClass o2 = null; //Has reference on stack but null on heap
            //o2.MyProperty = 20; Object reference not set to an instance of an object.


            new myClass(); //Referansla isaretlenmedigi icin, bu nesneye artık erisilemez. 
        }
    }
    class myClass
    {
        public int MyProperty { get; set; }
    }
}