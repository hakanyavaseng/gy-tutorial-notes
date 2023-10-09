using System;

namespace tutorial
{

    class Program
    {
        static void Main(string[] args)
        {

            #region try - catch
#if true
            try
            {
                //Contains possible runtime errors.
                Console.WriteLine("Please enter number 1: ");
                int num1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter number 2: ");
                int num2 = int.Parse(Console.ReadLine());

                Console.WriteLine("Sum: " + (num1 + num2));

            }
            catch
            {
                Console.WriteLine("Please enter number.");
            }

#endif

            #endregion



        }


    }


}
