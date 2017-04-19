using System;

namespace Week_1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAutomata.ExampleSlide14Lesson2().printTransitions();
            Console.WriteLine("Is DFA: " + TestAutomata.ExampleSlide14Lesson2().IsDfa());
            while (true)
            {
                Console.WriteLine("Correct grammar: " + TestAutomata.ExampleSlide14Lesson2().Accept(Console.ReadLine()));
            }
        }
    }
}