using System;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;


namespace Week_1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAutomata.ExampleSlide14Lesson2().printTransitions();
            Console.WriteLine("Is DFA: " + TestAutomata.ExampleSlide14Lesson2().IsDfa());
            bool looping = true;
            while (looping)
            {
                switch (Console.ReadLine())
                {
                    case "quit":
                        looping = false;
                        break;
                    case "print":
                        GraphVizParser.printGraph(TestAutomata.ExampleSlide14Lesson2(), "ExampleSlide14Lesson2");
                        break;
                    default:
                        Console.WriteLine("Correct grammar: " +
                                          TestAutomata.ExampleSlide14Lesson2().Accept(Console.ReadLine()));
                        break;
                }
            }
        }
    }
}