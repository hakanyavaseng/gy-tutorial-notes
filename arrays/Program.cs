using System;
using System.ComponentModel.DataAnnotations;

namespace arrays
{

    class Program
    {
        static void Main(string[] args)
        {

            #region Arrays

            int[] age = new int[5]; // Age referans turlu olup stackte tutulur, heap'teki degerleri referans eder


            #endregion
            #region arrayClass

            int[] a = new int[5] { 1, 2, 3, 4, 5 }; // Indexer can be used
            Array a2 = new int[5]; // Indexer can not be used

            // a2[0] = 5;       can not be done

            // First way
            // a2 = a; 

            //Second way
            // a2 = new int[5] { 1, 2, 3, 4, 5 };

            //Third way
            a2.SetValue(1, 0); a2.SetValue(2, 1); a2.SetValue(3, 2);

            object value = a2.GetValue(1);
            //Console.WriteLine(value);

            #region Methods

            //Clear
            //Assigns default value to all elements

            Array.Clear(a2);
                //for(int i=0; i<a2.Length; i++) { Console.WriteLine(a2.GetValue(i)); }
            
            //Copy, has many variations
                int[] age2 = new int[a.Length];
                Array.Copy(a, a2, a.Length);
                //for (int i = 0; i < a2.Length; i++) { Console.WriteLine(a2.GetValue(i)); }

            //IndexOf

               // int index = Array.IndexOf(a2, 10);
                //Console.WriteLine(index); // returns -1, 10 not exists in a2
                //index = Array.IndexOf(a2, 5, 0,5);
                //Console.WriteLine(index); // returns 4, 5 in 4th order

            //Reverse

               Array.Reverse(a2);
               //for (int i = 0; i < a2.Length; i++) { Console.WriteLine(a2.GetValue(i)); }

            //Sort

                Array.Sort(a2);
                //for (int i = 0; i < a2.Length; i++) { Console.WriteLine(a2.GetValue(i)); }

            #endregion
            #region Properties

            ////IsReadOnly
            //    bool flag = a2.IsReadOnly;
            //    Console.WriteLine(flag);

            ////IsFixedSize
            //    flag = a2.IsFixedSize;
            //    Console.WriteLine(flag);
            
            ////Length
            //    Console.WriteLine(a2.Length);

            ////Rank
            //    Console.WriteLine(a2.Rank);
            //    int[,,] x = new int[3, 4, 5];
            //    Console.WriteLine(x.Rank);






            #endregion
            #endregion
            #region CreateInstance

            Array a3 = Array.CreateInstance(typeof(int), 3); 
            Array a4 = Array.CreateInstance(typeof(int), 1,2,3,4); 
          //  Console.WriteLine(a4.Rank);


            #endregion
            #region Ranges & Indices

            //INDEX 

            int[] numbers = { 3, 4, 7, 9, 1, 2, 31, 321, 534, 5643, 457, 56, 78 };

            Index index = 5; // 0 - > n-1
            Console.WriteLine(numbers[index]); //2
            index = ^8; //Starts from right end and counts from 1.
            Console.WriteLine(numbers[index]); //2

            //RANGE
            // Range range = 3..7; // Left side = index, right side = sequence
            // After that operations we can use range as array.

            // between 2 to 5643
            Range range = 5..10;
            var numbers2 = numbers[range];

            // Range range = .. -> Gets entire array

            range = 5..^3;
            var numbers3 = numbers[range];

            range = ^8..^3; //Index durumunda sıra numarası gibi davranıp 1den istenilen degere kadar gider
                            
            var numbers4 = numbers[range];

            //^1 gets last element of array
            Console.WriteLine(numbers[^1]); // 78








            #endregion

        }



    }





}