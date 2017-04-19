using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;

namespace Week_1
{
    class GraphVizParser 
    {

        public static void printGraph(Automata<string> data, string filename)
        {
            string toPrint="";
            toPrint += "digraph{";
            toPrint += " ";
            toPrint += "node [shape = doublecircle];";
           

            ISet<Transition<string>> transitions = data.getTransitions();

            SortedSet<string> finalStates = data.getFinalStates();

            foreach (string t in finalStates)
            {
                toPrint += " " + t + " ";
            }
            toPrint += "; ";
            toPrint += " ";
            toPrint += "node [shape = circle];";

            foreach (Transition<string> t in transitions)
            {
                toPrint += " " + t.getFromState() + " -> " + t.getToState() + " " +
                     "[ label = " + "\"" + t.getSymbol() + "\"" + " ];";
            }
            toPrint += " }";

            Console.WriteLine(toPrint);

            generateGraphFile(toPrint, filename);
        }

        static void generateGraphFile(string data, string filename)
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
