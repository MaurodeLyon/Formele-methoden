﻿using System;


namespace Week_1
{
    class Program
    {
        static void Main()
        {
            RegExp reg = new RegExp("a").Or(new RegExp("b")).Dot(new RegExp("c").Or(new RegExp("d")));
            Automaat<string> regExp = ThompsonConstruction.ConvertRegExp(reg);
            GraphVizParser.PrintGraph(regExp, "ThompsonNdfa");

            char[] alphabet = { 'a', 'b' };
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("1", 'a', "2"));
            dfa.AddTransition(new Transition<string>("1", 'b', "2"));
            dfa.AddTransition(new Transition<string>("1", 'b', "3"));

            dfa.AddTransition(new Transition<string>("2", 'a', "4"));
            dfa.AddTransition(new Transition<string>("2", 'a', "2"));
            
            dfa.AddTransition(new Transition<string>("4", 'b', "3"));
            dfa.AddTransition(new Transition<string>("4", 'b', "1"));

            dfa.DefineAsStartState("1");
            dfa.DefineAsFinalState("1");
            dfa.DefineAsFinalState("4");

            //Automaat<string> answer = NdfaToDfaConverter.Convert(dfa);
            //GraphVizParser.PrintGraph(answer, "ndfa2dfa");

            //Automaat<string> ndfa = NdfaToDfaConverter.reverse(answer);


            Automaat<string> dfaToMinimize = new Automaat<string>(alphabet);

            dfaToMinimize.AddTransition(new Transition<string>("0", 'a', "2"));
            dfaToMinimize.AddTransition(new Transition<string>("0", 'b', "3"));

            dfaToMinimize.AddTransition(new Transition<string>("1", 'a', "3"));
            dfaToMinimize.AddTransition(new Transition<string>("1", 'b', "2"));

            dfaToMinimize.AddTransition(new Transition<string>("2", 'a', "0"));
            dfaToMinimize.AddTransition(new Transition<string>("2", 'b', "4"));

            dfaToMinimize.AddTransition(new Transition<string>("3", 'a', "1"));
            dfaToMinimize.AddTransition(new Transition<string>("3", 'b', "5"));

            dfaToMinimize.AddTransition(new Transition<string>("4", 'a', "6"));
            dfaToMinimize.AddTransition(new Transition<string>("4", 'b', "5"));

            dfaToMinimize.AddTransition(new Transition<string>("5", 'a', "2"));
            dfaToMinimize.AddTransition(new Transition<string>("5", 'b', "0"));

            dfaToMinimize.AddTransition(new Transition<string>("6", 'a', "4"));
            dfaToMinimize.AddTransition(new Transition<string>("6", 'b', "0"));


            dfaToMinimize.DefineAsStartState("0");
            dfaToMinimize.DefineAsFinalState("1");
            dfaToMinimize.DefineAsFinalState("3");
            dfaToMinimize.DefineAsFinalState("4");
            dfaToMinimize.DefineAsFinalState("6");

            //Automaat<string> minimizedDfa = NdfaToDfaConverter.Convert(NdfaToDfaConverter.reverse(NdfaToDfaConverter.Convert(NdfaToDfaConverter.reverse(dfaToMinimize))));
            Automaat<string> minimizedDfa = NdfaToDfaConverter.Convert(NdfaToDfaConverter.Reverse(dfaToMinimize));
            GraphVizParser.PrintGraph(minimizedDfa,"minimizedDFA");

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
                        break;
                }
            }
        }
    }
}