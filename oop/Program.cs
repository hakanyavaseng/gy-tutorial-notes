//GY-OOP-TUTORIALS
namespace oop
{
    class Program
    {
        static void Main(string[] args)
        {
            //myClass o1 = new myClass();
            //myClass o1 = new(); // C# 9.0
            //o1.MyProperty = 1;

            //myClass o2 = null; //Has reference on stack but null on heap
            ////o2.MyProperty = 20; Object reference not set to an instance of an object.


            //new myClass(); //Referansla isaretlenmedigi icin, bu nesneye artık erisilemez. 

            #region Deep Copy
            //myClass m1 = new myClass();
            //myClass m2 = m1; //Shallow Copy

            //myClass m3 = m1.Clone(); //Deep Copy

            #endregion
            #region Encapsulation

            //Old method
            myClass m = new myClass();
            m.ASet(15);
            Console.WriteLine(m.AGet());

            //New method
            m.A = 123;
            Console.WriteLine(m.A);

            #endregion
        }
    }
    class myClass 
    {

        int a; 
        
        //Encapsulation, old method 
        public int AGet()
        {
            return this.a;
        }
        public void ASet(int value) 
        {
            this.a = value; 
        }

        //Encapsulation, new method
        public int A
        {
            get { return this.a; }
            set { this.a = value; }
        }


        public myClass Clone()
        {
            return (myClass)this.MemberwiseClone(); //MemberwiseClone bir sinifin icerisinde bir siniftan uretilmis olan o anki nesneyi clone'lamamizi saglar
        }


    }
}