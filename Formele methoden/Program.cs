using System;


namespace Week_1
{
    class Program
    {
        static void Main()
        {
            RegExp reg = new RegExp("a");
            reg = reg.Or(new RegExp("b"));
            RegExp reg2 = new RegExp("c");
            reg2 = reg2.Or(new RegExp("d"));
            RegExp reg3 = reg.Dot(reg2);

            Automata<string> regExpex = ThompsonConstruction.ConvertRegExpToAutomata(reg3);
            GraphVizParser.PrintGraph(regExpex, "TEST");

            Automata<string> answer = ndfaToDfaConverter.convert(TestAutomata.NDFAToConvert());


            //TestRegExp regExp = new TestRegExp();
            // regExp.testLanguage();
            GraphVizParser.PrintGraph(answer, "NDFATODFACONV");
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
                        break;
                    default:
                        break;
                }
            }
        }
    }
}