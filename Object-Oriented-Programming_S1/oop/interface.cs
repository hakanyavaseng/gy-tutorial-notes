//Programlama sureclerinde nesnelere direkt olarak bir arayuz olusturulmasini ve bu arayuz uzerinden gelistirici ile nesne arasındaki etkilesimin daha da kolaylasitirilmasini saglayan bir tasarim desenidir.

//Bu desenin amacı, nesnelerin birbirleri ile olan etkilesimlerini bir arayuz uzerinden yapmalarini saglamaktir. Boylece nesneler arasindaki bagimlilik azalir ve birbirlerinin iceriklerini bilmeden birbirleri ile etkilesim kurabilirler.

//INTERFACE'IN ABSTRACT CLASS'tan FARKİ, interface'in sadece imzalari iceren bir yapilanma olmasidir. Abstract class'larda imzalarin yaninda implementasyonlar da bulunabilir.

//Interface'in genel davranisi CAN-DO iliskisidir. Bunun anlami interface'i implemente eden class'lar, interface'in icerisindeki imzalari implemente etmek zorundadir. Boylece interface'i implemente eden class'lar, interface'in icerisindeki imzalari kullanabilirler.

//Interface'ler referans turlu degiskenlerdir. Bu degiskenlerin degerleri null olabilir.

//Class'in tanimlandigi her yerde Interface de tanimlanabilir.

//Class'larin imzalarinin abstract class degil de interface olmalarinin sebebi, interfacelerin nesnelerinin uretilmemesidir. Boylece implementasyon surecinde abstract class yerine interface kullanilmasi kaynak tuketimi acisindan daha avantajlidir.



namespace interfaces
{
    #region  Interfaces - 1 
    interface IMyInterface // Interface'lerin ismi I ile baslatilir, bu gelenektir.
    {
        // int a; //Gives error because interfaces cannot contain fields.
        //Interface icinde imzalari tanimlarken, access modifier kullanilmaz.
        void X();
        int Y { get; set; }
    }

    interface IMyInterface2
    {
        void Z();
    }

    class singleImplementClass : IMyInterface //Burada : operatoru kalitim operatoru degil, implementasyon operatorudur.
    {
        public int Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public void X()
        {
            throw new NotImplementedException();
        }
    }

    class explicitClass : IMyInterface
    {
        //Explicit olarak implementasyon yapilirken, imzalarin basina interface'in ismi yazilir.
        //Bu sekilde implementasyon yapilirken, interface'in icerisindeki imzalarin access modifier'lari degistirilemez. Private olmak zorundadir.
        int IMyInterface.Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void IMyInterface.X()
        {
            throw new NotImplementedException();
        }
    }

    class multiImplementClass : IMyInterface, IMyInterface2 //Bir class birden fazla interface'i implemente edebilir.
    {
        public int Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public void X()
        {
            throw new NotImplementedException();
        }

        public void Z()
        {
            throw new NotImplementedException();
        }
    }

    #region Interface'lerin birbirinden kalitimi

    //Interface'ler birbirinden kalitim yapabilirler.
    //
    interface IMyInterface3 : IMyInterface
    {
        void A();
    }

    class interfaceInheritanceClass : IMyInterface3
    {
        public int Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public void X()
        {
            throw new NotImplementedException();
        }

        public void A()
        {
            throw new NotImplementedException();
        }
    }


    #endregion

    #region Class bir farkli class'tan kalitim alirken, bir interface'den de implementasyon yapabilir.

    class classInheritanceClass : singleImplementClass, IMyInterface2 // Burada once class'in kalitimini, sonra interface'in implementasyonunu yapariz.
    {
        public void Z()
        {
            throw new NotImplementedException();
        }
    }

    #endregion




    #endregion

}

namespace interfaces2
{
    //Explicit Implement & Name Hiding
    interface IA
    {
        int X();
    }

    interface IB
    {
        int X();
    }
    class explicitImplementNameHidingClass : IA, IB
    {
        int IA.X()
        {
            Console.WriteLine("IA.X");
            return 0;
        }

        int IB.X()
        {
            Console.WriteLine("IB.X");
            return 0;
        }
    }


    #region Base Class ve Interface'in bir sinifa birlikte kalitim verme durumunda name hiding
    class BaseClass
    {
        public void Run()
        {
            Console.WriteLine("BaseClass.Run");
        }


    }

    interface IRun
    {
        void Run();
    }

    class RunClass : BaseClass, IRun
    {
        //Burada compiler hata vermeyecektir, cünkü interface tarafindan implemente edilmesi istenen Run methodu, BaseClass tarafindan zaten implemente edilmistir. Bu method kalitim ile RunClass'a aktarilacagindan interface'in istedigi implementasyon saglanmis olacaktir.
    }

    #endregion

    #region  Default Implementations
    //Interface'lerin icerisinde sadece imzalar bulunur. Ancak C# 8.0 ile birlikte interface'lerin icerisinde default implementasyonlar da bulunabilir. Bu sayede methodlarin imzalarinin yaninda govdesi de bulunabilir.
    //Default implementation, opsiyoneldir.
    //Default implementasyonlar, interface'i implemente eden class'lar tarafindan override edilebilir.

    //Default implementasyonlarin varligi interface'in varlik amaciyla ters dusmektedir. Bazen de default implementasyonlarin varliginin gerekli oldugu durumlar olabilir. Ornegin, mimaride mevcut olan bir interface'e yeni bir member eklenmek istenirse, bu durumda bu interface'i implemente eden tum class'larin bu yeni member'i implemente etmesi gerekecektir. Bu durumda default implementasyonlar kullanilarak, bu yeni member'in implementasyonu interface'e eklenir ve bu member'i implemente etmeyen class'lar bu default implementasyonu kullanir.

    interface IDefaultImplementation
    {
        void Y();
        void X()
        {
            Console.WriteLine("IDefaultImplementation.X");
        }
    }

    class DefaultImplementationClass : IDefaultImplementation
    {
        //Burada X methodu override edilmemistir. Bu durumda X methodu IDefaultImplementation'dan alinir.
        public void Y()
        {
            throw new NotImplementedException();
        }

        //Eger X burada tekrar implemente edilirse, IDefaultImplementation'dan alinmaz.
        //Tekrar implemente edilirken override keyword'u kullanilmaz.

        // public void X()
        // {
        //     Console.WriteLine("DefaultImplementationClass.X");
        // }

    }

    #endregion

    #region Interface'lerin isaretleme amacli kullanilmasi
    //Classlar interfaceler sayesinde gruplanabilir. Bu yüzlerce class'in oldugu bir projede, class'larin gruplanmasini saglar. 

    #endregion
}