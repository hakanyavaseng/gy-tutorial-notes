using abstraction;

//Abstract class tanımlamak için abstract keyword'unu kullanmaliyiz.
//Bir abstract class icine member'lar bilinen kurallariyla eklenebilir.
//Bu abstract class'i implemente edecek member'larin da imzalari eklenebilir, bu imzalar abstract ile isaretlenmelidir ve bu imzalar public olmalidir.
//Abstract classlarda implementasyon yapilirken override keyword'u kullanilmalidir.
//Virtual olan yapilanma opsiyonel olarak override edilebilirken, abstract olan yapilanma zorunlu olarak override edilmelidir.
//Bir sinif yalnizca bir abstract class'i inherit edebilir.
//Abstract class'i implemente eden sinif concrete class olarak adlandirilir.
//Abstract class implemente edilirken, imzalarin ayni olmasina dikkat edilmelidir.


 
//abstractClass a = new abstractClass();  //Gives error because abstract classes cannot be instantiated
                                        //Cannot create an instance of the abstract type or interface 'abstractClass'

new _abstractClass();                   //This is the constructor of abstractClass
                                        //This is the constructor of _abstractClass



namespace abstraction
{
    abstract class abstractClass 
    {
        //Somut kisim
        public abstractClass()
        {
            System.Console.WriteLine("This is the constructor of abstractClass");
        }
        public void X(){}
        public int MyProperty { get; set; }

        //Abstract kisim
        public abstract void Y();

        public abstract void Z();
        public abstract int MyProperty2 { get; set; }

    }


    class _abstractClass : abstractClass
    {
        public _abstractClass()
        {
            System.Console.WriteLine("This is the constructor of _abstractClass");
        }

        public override int MyProperty2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Y() //Gives error if there is no override keyword
        {
            throw new NotImplementedException();
        }
        public override void Z() //Gives error if there is not public keyword
        {
            throw new NotImplementedException();
        }
    }


}

namespace abstraction2
{
    //Abstract class'lar başka abstract class'lardan türetilebilir.
    //Concrete classlarda tüm abstract member'lar implemente edilmelidir. Kalitim kurallari gecerlidir.
    abstract class X
    {

        public void A()
        {

        }

        public void B()
        {

        }

        public void C()
        {

        }

        public abstract void D();
        abstract public void W();

    }

    abstract class Y : X
    {
        public void E()
        {

        }

        abstract public void F();

    }

    class Z : Y
    {
        public override void D()
        {
            throw new NotImplementedException();
        }

        public override void F()
        {
            throw new NotImplementedException();
        }

        public override void W()
        {
            throw new NotImplementedException();
        }
    }






}