using System;
using System.Collections.Generic;
using CSharpFundamentals.ChapterSupport;
using CSharpFundamentals.Debugging;
using System.Text;
using System.Linq;

// Learn how to trace the path of method calls that led to the current line.
// Key terms in this chapter:
// Call Stack: The ordered list of active method calls.
// Current Frame: The method currently being executed (top of stack).
// Caller: The method that invoked the current method.
// Navigation: Jumping between stack frames to inspect code context.

namespace CSharpFundamentals.Debugging
{
    internal class Ch66_CallStack_Navigation
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
            // if (list == null) // ArgumentNullException
            // throw new ArgumentException("list");

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
            var min = list[0];  //via debug, windows, Call Stack I can easily redirect myself here
            // call stack top is where we last were stationed

            for (var i = 1; i < list.Count; i++)
            {
                if (list[i] < min)
                    min = list[i];
            }
            return min;
        }
    }
}