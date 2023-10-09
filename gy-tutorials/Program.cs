using System;

namespace tutorial
{

    class Program
    {
        static void Main(string[] args)
        {

            #region try - catch
#if false
            Console.WriteLine("Please enter number 1: ");
            int num1 = default, num2= default;
            try
            {
                //Contains possible runtime errors.
                num1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter number 2: ");
                num2 = int.Parse(Console.ReadLine());

            }
            catch
            {
                Console.WriteLine("Please enter number.");
            }

            Console.WriteLine("Sum: " + (num1 + num2));

#endif

            #endregion
            #region try-catch exceptions


#if true
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

#endif




            #endregion



        }


    }


}
