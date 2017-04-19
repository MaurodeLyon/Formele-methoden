using System;

namespace Week_1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAutomata.Example4Week1().printTransitions();
            Console.WriteLine(TestAutomata.Example4Week1().IsDfa());
            Console.ReadKey();
        }
    }
}