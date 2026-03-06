using System;
using System.Collections.Generic;
using CSharpFundamentals.ChapterSupport;
using CSharpFundamentals.Debugging;
using System.Text;
using System.Linq;

// Compare automatic variable inspection tools in Visual Studio.
// Key terms in this chapter:
// Autos Window: Shows variables/expressions Visual Studio predicts are relevant.
// Locals Window: Shows variables currently in local scope.
// Local Scope: Variables declared in the current method/block and currently available.

namespace CSharpFundamentals.Debugging
{
    internal class Ch67_AutosAndLocals
    {
        public static void Run()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            var smallests = GetSmallests(numbers, 3);

            foreach (var number in smallests)
                Console.WriteLine(number);

            Console.ReadLine();
        }

        public static List<int> GetSmallests(List<int> list, int count)
        {
            if (count > list.Count || count <= 0)
                throw new ArgumentOutOfRangeException("count", "Count should be between 1 and the number of elements in this list."); // Meaningful Exception Message


            var buffer = new List<int>(list);
            var smallests = new List<int>();

            while (smallests.Count < count)
            {
                var minim = GetSmallest(buffer);
                smallests.Add(minim);
                buffer.Remove(minim);
            }
            return smallests;
        }
        public static int GetSmallest(List<int> list)
        {
            //Assume the first number is the smallest
            var min = list[0]; 

            for (var i = 1; i < list.Count; i++)
            {
                if (list[i] < min) //autos window selects automatically variables that vs deems vital, at the current location the user is at the program
                    min = list[i]; //locals isn't as resourceful and displays less variety of variables. Choose locals or autos based on reference
            }
            return min;
        }
    }
}