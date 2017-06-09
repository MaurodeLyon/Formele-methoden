using System;


namespace Week_1
{
    class Program
    {
        static void Main()
        {
            RegExp reg = new RegExp("a").Or(new RegExp("b")).Dot(new RegExp("c").Or(new RegExp("d")));
            Automaat<string> regExp = ThompsonConstruction.ConvertRegExp(reg);
            GraphVizParser.PrintGraph(regExp, "ThompsonNdfa");

            char[] alphabet = {'a', 'b'};
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

            ////Automaat<string> ndfa = NdfaToDfaConverter.reverse(answer);


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
            //Automaat<string> minimizedDfa = NdfaToDfaConverter.Convert(NdfaToDfaConverter.Reverse(dfaToMinimize));
            //GraphVizParser.PrintGraph(minimizedDfa,"minimizedDFA");

            Automaat<string> epsilonNDFA = new Automaat<string>(alphabet);

            epsilonNDFA.AddTransition(new Transition<string>("0", '$', "1"));
            epsilonNDFA.AddTransition(new Transition<string>("0", '$', "7"));

            epsilonNDFA.AddTransition(new Transition<string>("1", '$', "2"));
            epsilonNDFA.AddTransition(new Transition<string>("1", '$', "4"));

            epsilonNDFA.AddTransition(new Transition<string>("2", 'a', "3"));

            epsilonNDFA.AddTransition(new Transition<string>("3", '$', "6"));

            epsilonNDFA.AddTransition(new Transition<string>("4", 'b', "5"));

            epsilonNDFA.AddTransition(new Transition<string>("5", '$', "6"));

            epsilonNDFA.AddTransition(new Transition<string>("6", '$', "7"));
            epsilonNDFA.AddTransition(new Transition<string>("6", '$', "1"));

            epsilonNDFA.AddTransition(new Transition<string>("7", 'a', "8"));

            epsilonNDFA.AddTransition(new Transition<string>("8", 'b', "9"));

            epsilonNDFA.AddTransition(new Transition<string>("9", 'b', "10"));

            epsilonNDFA.DefineAsStartState("0");
            epsilonNDFA.DefineAsFinalState("10");

            Automaat<string> epsilonDFA = NdfaToDfaConverter.Convert(epsilonNDFA);
            GraphVizParser.PrintGraph(epsilonDFA, "epsilondfa");

            Automaat<string> L1 = new Automaat<string>(alphabet);

            L1.AddTransition(new Transition<string>("1", 'a', "2"));
            L1.AddTransition(new Transition<string>("1", 'b', "1"));

            L1.AddTransition(new Transition<string>("2", 'a', "2"));
            L1.AddTransition(new Transition<string>("2", 'b', "3"));

            L1.AddTransition(new Transition<string>("3", 'a', "4"));
            L1.AddTransition(new Transition<string>("3", 'b', "1"));

            L1.AddTransition(new Transition<string>("4", 'a', "4"));
            L1.AddTransition(new Transition<string>("4", 'b', "4"));

            L1.DefineAsStartState("1");
            L1.DefineAsFinalState("4");

            Automaat<string> L2 = new Automaat<string>(alphabet);

            L2.AddTransition(new Transition<string>("1", 'a', "1"));
            L2.AddTransition(new Transition<string>("1", 'b', "2"));

            L2.AddTransition(new Transition<string>("2", 'a', "3"));
            L2.AddTransition(new Transition<string>("2", 'b', "2"));

            L2.AddTransition(new Transition<string>("3", 'a', "1"));
            L2.AddTransition(new Transition<string>("3", 'b', "4"));

            L2.AddTransition(new Transition<string>("4", 'a', "3"));
            L2.AddTransition(new Transition<string>("4", 'b', "2"));

            L2.DefineAsStartState("1");
            L2.DefineAsFinalState("1");
            L2.DefineAsFinalState("2");
            L2.DefineAsFinalState("3");


            Automaat<string> L3 = Automaat<string>.Union(L1, L2);
            GraphVizParser.PrintGraph(L3, "L3");

            RegExp regExpLeft = new RegExp("baa");
            RegExp regExpRight = new RegExp("bb");
            RegExp regExpOr = regExpLeft.Or(regExpRight);
            Automaat<string> ndfaOr = ThompsonConstruction.ConvertRegExp(regExpOr);
            GraphVizParser.PrintGraph(ndfaOr, "ndfaOr");
            Automaat<string> dfaOr = NdfaToDfaConverter.Convert(ndfaOr);
            GraphVizParser.PrintGraph(dfaOr, "dfaOr");


            Automaat<string>.DfaGenerateValue test = new Automaat<string>.DfaGenerateValue { Parameter = "aab", IsNot = false, Type = Automaat<string>.GeneratorType.BeginsWith };
            Automaat<string> gendfa = Automaat<string>.GenerateDfa(test,alphabet);
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