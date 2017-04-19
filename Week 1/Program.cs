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
            TestAutomata.Example3Week1().printTransitions();
            Console.WriteLine(TestAutomata.Example3Week1().IsDfa());

            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            var wrapper = new GraphGeneration(getStartProcessQuery,
                                  getProcessStartInfoQuery,
                                  registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph("digraph{a -> b; b -> c; c -> a;}", Enums.GraphReturnType.Jpg);
            System.IO.File.WriteAllBytes("test22.jpg", output);

            GraphVizParser.printGraph(TestAutomata.ExampleSlide14Lesson2(), "ExampleSlide14Lesson2");


            Console.ReadKey();
        }
    }
}