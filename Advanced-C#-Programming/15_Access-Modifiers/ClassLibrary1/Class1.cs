namespace ClassLibrary1
{
    public class Class1
    {
        private int privateInt = 10;

        public void Print()
        {
            Console.WriteLine(privateInt);  //Since privateInt is a private member of the class, it can be accessed within the class.
                                            //Outside the class, privateInt can be accessed through a public method but not directly.
        }

        protected void X()
        {
            Console.WriteLine("Protected Method");
        }

        public void Y()
        {
            X(); // X is a protected member of Class1, so it can be accessed within the class.
        }

        internal void Z()
        {
            Console.WriteLine("Internal Method");
        }

        protected internal void W()
        {
            Console.WriteLine("Protected Internal Method");
        }

        private protected void V()
        {
            Console.WriteLine("Private Protected Method");
        }
    }

    public class Class2 : Class1
    {
        public void Print()
        {
            X(); // X is a protected member of Class1, so it can be accessed within the derived class.
        }

        public void Print2()
        {
            Z(); // Accessible
        }

        public void Print3()
        {
            V(); // Accessible, although it is private protected, it is accessible within the same assembly and from within a derived class.
        }
    }
}




