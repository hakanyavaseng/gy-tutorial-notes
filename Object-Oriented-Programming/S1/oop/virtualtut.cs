
namespace virtualTut
{
    class Obje
    {
        public virtual void Bilgi()
        {
            Console.WriteLine("I am an object");
        }
    }

    class Terlik : Obje
    {
        public override void Bilgi()
        {
            Console.WriteLine("I am a Terlik");
        }
     
    }

    class Kalem : Obje
    {
        public override void Bilgi()
        {
            Console.WriteLine("I am a Kalem");
        }

    }

}