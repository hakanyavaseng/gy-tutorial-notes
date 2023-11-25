//Programlama sureclerinde nesnelere direkt olarak bir arayuz olusturulmasini ve bu arayuz uzerinden gelistirici ile nesne arasındaki etkilesimin daha da kolaylasitirilmasini saglayan bir tasarim desenidir.
//Bu desenin amacı, nesnelerin birbirleri ile olan etkilesimlerini bir arayuz uzerinden yapmalarini saglamaktir. Boylece nesneler arasindaki bagimlilik azalir ve birbirlerinin iceriklerini bilmeden birbirleri ile etkilesim kurabilirler.
//INTERFACE'IN ABSTRACT CLASS'tan FARKİ, interface'in sadece imzalari iceren bir yapilanma olmasidir. Abstract class'larda imzalarin yaninda implementasyonlar da bulunabilir.
//Interface'in genel davranisi CAN-DO iliskisidir. Bunun anlami interface'i implemente eden class'lar, interface'in icerisindeki imzalari implemente etmek zorundadir. Boylece interface'i implemente eden class'lar, interface'in icerisindeki imzalari kullanabilirler.
//Interface'ler referans turlu degiskenlerdir. Bu degiskenlerin degerleri null olabilir.
//Class'in tanimlandigi her yerde Interface de tanimlanabilir.


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

    #region Interfaces - 2
   
    #endregion
}



