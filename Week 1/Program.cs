using System;

namespace Week_1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAutomata.Example3Week1().printTransitions();
            Console.WriteLine(TestAutomata.Example3Week1().IsDfa());
            Console.ReadKey();
        }
    }
}