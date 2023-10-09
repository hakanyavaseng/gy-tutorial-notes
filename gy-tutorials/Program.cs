using System;

namespace tutorial
{

    class Program
    {
        static void Main(string[] args)
        {

            #region try - catch
#if true
            Console.WriteLine("Please enter number 1: ");
            int num1 = default, num2= default;
            try
            {
                //Contains possible runtime errors.
                num1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter number 2: ");
                num2 = int.Parse(Console.ReadLine());
                Console.WriteLine("Sum: " + (num1 / num2));

            }
          
            catch (DivideByZeroException ex) 
            {
                Console.WriteLine("Message: " + ex.Message); 
            }
            catch (FormatException ex) 
            {
                Console.WriteLine("Message: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: " + ex.Message);
            }



#endif

            #endregion



        }


    }


}
