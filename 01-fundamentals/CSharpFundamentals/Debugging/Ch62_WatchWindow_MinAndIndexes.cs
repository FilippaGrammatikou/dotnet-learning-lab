using System;
using System.Collections.Generic;
using CSharpFundamentals.ChapterSupport;
using CSharpFundamentals.Debugging;
using System.Text;
using System.Linq;

// Learn to monitor important values while stepping through code.
// Key terms in this chapter:
// Watch Window: A debugging panel where we manually track variables/expressions.
// Expression: A value or computation (e.g., min, list[0], list[i]).
// Scope: Where a variable exists and can be accessed.
// * Example: 'i' is only available inside the for-loop scope.

namespace CSharpFundamentals.Debugging
    {
        internal class Ch62_WatchWindow_MinAndIndexes
        {
            public static void Run()
            {
                {
                    var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };  
                    var smallests = GetSmallests(numbers, 3);

                    foreach (var number in smallests)
                        Console.WriteLine(number);

                    Console.ReadLine();
                }
            }

            public static List<int> GetSmallests(List<int> list, int count)
            {
                var smallests = new List<int>();

                while (smallests.Count < count)
                {
                    var minim = GetSmallest(list); //proves sth is deffinetely wrong with this method 
                    smallests.Add(minim);
                    list.Remove(minim);
                }
                return smallests;
            }
            public static int GetSmallest(List<int> list)
            {
                //Assume the first number is the smallest
                var min = list[0];

                for (var i = 1; i < list.Count; i++)
                {
                    if (list[i] < min) // bug
                        min = list[i];
                }
                return min;
            }
        }
    }

