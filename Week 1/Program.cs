using System;


namespace Week_1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRegExp regExp = new TestRegExp();
           // regExp.testLanguage();

            TestAutomata.DfaTEST3().PrintTransitions();
            Console.WriteLine("Is DFA: " + TestAutomata.DfaTEST3().IsDfa());
            bool looping = true;
            while (looping)
            {
                string word = Console.ReadLine();
                switch (word)
                {
                    case "quit":
                        looping = false;
                        break;
                    case "print":
                        GraphVizParser.PrintGraph(TestAutomata.ExampleSlide14Lesson2(), "ExampleSlide14Lesson2");
                        break;
                    default:
                        Console.WriteLine("Correct grammar: " +
                                          TestAutomata.ExampleSlide14Lesson2().Accept(word));
                        break;
                }
            }
        }
    }
}