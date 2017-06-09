﻿using System;
using System.Collections.Generic;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;

namespace Week_1
{
    public class GraphVizParser
    {
        public static void PrintGraph(Automaat<string> data, string filename)
        {
            string toPrint = "";
            toPrint += "digraph{";
            toPrint += " ";
            toPrint += "node [shape = doublecircle];";

            ISet<Transition<string>> transitions = data.Transitions;

            SortedSet<string> finalStates = data.FinalStates;

            foreach (string t in finalStates)
            {
                toPrint += " " + ("S"+t) + " ";
            }
            toPrint += "; ";
            toPrint += " ";
            toPrint += "node [shape = circle];";

            foreach (Transition<string> t in transitions)
            {
                toPrint += " " + ("S"+t.FromState) + " -> " + ("S"+t.ToState) + " " + "[ label = " + "\"" + t.Symbol + "\"" + " ];";
            }
            toPrint += " }";

            Console.WriteLine(toPrint);

            GenerateGraphFile(toPrint, filename);
        }

        static void GenerateGraphFile(string data, string filename)
        {
            GetStartProcessQuery getStartProcessQuery = new GetStartProcessQuery();
            GetProcessStartInfoQuery getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            RegisterLayoutPluginCommand registerLayoutPluginCommand =
                new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            GraphGeneration wrapper = new GraphGeneration(getStartProcessQuery,
                getProcessStartInfoQuery,
                registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph(data, Enums.GraphReturnType.Jpg);
            System.IO.File.WriteAllBytes(filename + ".jpg", output);
        }
    }
}