using System;
using System.Collections.Generic;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;

namespace Week_1
{
    class GraphVizParser 
    {

        public static void PrintGraph(Automata<string> data, string filename)
        {
            string toPrint="";
            toPrint += "digraph{";
            toPrint += " ";
            toPrint += "node [shape = doublecircle];";
           

            ISet<Transition<string>> transitions = data.GetTransitions();

            SortedSet<string> finalStates = data.GetFinalStates();

            foreach (string t in finalStates)
            {
                toPrint += " " + t + " ";
            }
            toPrint += "; ";
            toPrint += " ";
            toPrint += "node [shape = circle];";

            foreach (Transition<string> t in transitions)
            {
                toPrint += " " + t.GetFromState() + " -> " + t.GetToState() + " " +
                     "[ label = " + "\"" + t.GetSymbol() + "\"" + " ];";
            }
            toPrint += " }";

            Console.WriteLine(toPrint);

            GenerateGraphFile(toPrint, filename);
        }

        static void GenerateGraphFile(string data, string filename)
        {
            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            var wrapper = new GraphGeneration(getStartProcessQuery,
                                  getProcessStartInfoQuery,
                                  registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph(data, Enums.GraphReturnType.Jpg);
            System.IO.File.WriteAllBytes(filename+".jpg", output);

        }


    }
}
