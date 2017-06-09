using System;


namespace Week_1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Formele methoden test applicatie Mauro de Lyon & Arthur Brink");
            Console.WriteLine("-------------------------------------------------------------");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please select a regular expression:");
                Console.WriteLine("1: a+");
                Console.WriteLine("2: (a|b)*");
                Console.WriteLine("3: (d|e)*.(a+.(b|c)+");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();
                switch (keyInfo.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Option 1 selected : a+");
                        Execute(new RegExp("a").Plus());
                        break;
                    case '2':
                        Console.WriteLine("Option 2 selected : (a|b)*");
                        Execute(new RegExp("a").Or(new RegExp("b")).Star());
                        break;
                    case '3':
                        Console.WriteLine("Option 3 selected : (d|e)*.(a+.(b|c)+");
                        Execute(new RegExp("d").Or(new RegExp("e")).Star().Dot(new RegExp("a").Plus().Dot(new RegExp("b").Or(new RegExp("c")).Plus())));
                        break;
                    default:
                        Console.WriteLine("incorrect key entered.");
                        break;
                }
            }
        }

        public static void Execute(RegExp regExp)
        {
            // Convert regex to ndfa
            Console.WriteLine();
            Console.WriteLine("----- Converting regex to ndfa -----");
            Automaat<string> ndfa = ThompsonConstruction.ConvertRegExp(regExp);
            foreach (Transition<string> transition in ndfa.Transitions)
            {
                Console.WriteLine(transition.ToString());
            }
            Console.WriteLine("Printing image");
            GraphVizParser.PrintGraph(ndfa, "ndfa");
            Console.WriteLine("Image saved as ndfa.jpg");

            // Convert ndfa to dfa
            Console.WriteLine();
            Console.WriteLine("----- Converting ndfa to dfa -----");
            Automaat<string> dfa = NdfaToDfaConverter.Convert(ndfa);
            foreach (Transition<string> transition in dfa.Transitions)
            {
                Console.WriteLine(transition.ToString());
            }
            Console.WriteLine("Printing image");
            GraphVizParser.PrintGraph(dfa, "dfa");
            Console.WriteLine("Image saved as dfa.jpg");

            // Optimizing dfa Brzozowski
            Console.WriteLine();
            Console.WriteLine("----- Optimizing dfa: Brzozowski's algorithm -----");
            Automaat<string> brzozowskiDfa = NdfaToDfaConverter.OptimizeDfa(dfa);
            foreach (Transition<string> transition in brzozowskiDfa.Transitions)
            {
                Console.WriteLine(transition.ToString());
            }
            Console.WriteLine("Printing image");
            GraphVizParser.PrintGraph(brzozowskiDfa, "BrzozowskiDfa");
            Console.WriteLine("Image saved as BrzozowskiDfa.jpg");

            // Optimize dfa Hopcroft
            Console.WriteLine();
            Console.WriteLine("----- Optimizing dfa: Hopcroft's algorithm -----");
            Automaat<string> hopcroftDfa = HopcroftAlgorithm.MinimizeDfa(dfa);
            foreach (Transition<string> transition in hopcroftDfa.Transitions)
            {
                Console.WriteLine(transition.ToString());
            }
            Console.WriteLine("Printing image");
            GraphVizParser.PrintGraph(hopcroftDfa, "HopcroftDfa");
            Console.WriteLine("Image saved as HopcroftDfa.jpg");
        }
    }
}