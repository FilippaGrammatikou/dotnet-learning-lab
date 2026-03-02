using System;
using System.Collections.Generic;
using CSharpFundamentals.ChapterSupport;
using CSharpFundamentals.Debugging;
using System.Text;
using System.Linq;

// Validate inputs before processing to prevent invalid program states.
// Key terms in this chapter:
// Defensive Programming: Writing code that checks assumptions early.
// Guard Clause: An early validation check that stops execution when input is invalid.
// ArgumentOutOfRangeException: Thrown when a value is outside allowed range.
// ArgumentNullException: Thrown when a required reference argument is null.
// Meaningful Exception Message: A message that explains what is wrong and why.

namespace CSharpFundamentals.Debugging
{
    internal class Ch65_DefensiveProgramming_GuardClauses
    {
        public static void Run()
        {


            Console.ReadKey();
        }
    }
}
