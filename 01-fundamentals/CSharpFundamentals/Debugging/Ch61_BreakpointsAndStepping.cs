using System;
using System.Collections.Generic;
using CSharpFundamentals.ChapterSupport;
using CSharpFundamentals.Debugging;
using System.Text;
using System.Linq;

// Learn how to pause execution and inspect code flow line-by-line.
// Key terms in this chapter:
// Breakpoint (F9): A marker that pauses execution at a specific line.
// Debug Mode (F5): Runs the app with debugging enabled.
// Run Without Debugging (Ctrl+F5): Runs normally without pausing.

// Step Over (F10): Executes the current line without entering method calls.
// Step Into (F11): Enters a called method to inspect its internals.
// Step Out (Shift+F11): Finishes the current method and returns to caller.
// Stop Debugging (Shift+F5): Stops the current debug session.

namespace CSharpFundamentals.Debugging
{
    internal class Ch61_BreakpointsAndStepping
    {
        public static void Run()
        {
            {
                var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };  //added a breakpoint example, pressing F10 excecutes one line at a time
                var smallests = GetSmallests(numbers, 3); // inspecting var smallests, notice of possible issue. F11 to step into

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
                if (list[i] > min) // bug
                    min = list[i];
            }
            return min;
        }
    }
}
