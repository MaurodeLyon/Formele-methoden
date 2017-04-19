using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAutomata.ExampleSlide14Lesson2().printTransitions();
            Console.WriteLine(TestAutomata.ExampleSlide14Lesson2().IsDfa());
            TestAutomata.Dfa().printTransitions();
            Console.WriteLine(TestAutomata.Dfa().IsDfa());
            Console.ReadKey();
        }
    }
}