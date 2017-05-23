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

            Automaat<string> answer = NdfaToDfaConverter.Convert(dfa);
            GraphVizParser.PrintGraph(answer, "ndfa2dfa");

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