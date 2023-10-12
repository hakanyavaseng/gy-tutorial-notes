using System;

namespace tutorial
{

    class Program
    {
        static void Main(string[] args)
        {

            int i = default;

            #region for variations
            for(int j = 0; j < args.Length; j++) {  }

            for(; i < args.Length; i++) { }

            for(i = 5; i < args.Length; i++) { } //If you want to use pre declared variable inside for loop, you must assign again.

            for(; i < args.Length; ) { i++; }

            string str = "try";
            for(i=5; str == "ntry"; i++) { } 

            for(; ; ) { break; } // Infinite loop

            for(int j = 0 , k = 10; j <10 && k > 10; j++, k++) { }





            #endregion

           



        }


    }


}
