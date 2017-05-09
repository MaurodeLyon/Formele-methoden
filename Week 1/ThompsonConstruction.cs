using System.Collections.Generic;
using System.Linq;

namespace Week_1
{
    class ThompsonConstruction
    {
        public static Automata<string> ConvertRegExpToAutomata(RegExp regExp)
        {
            Automata<string> automata = new Automata<string>();
            Convert(regExp, automata);
            return automata;
        }

        private static void Convert(RegExp regExp, Automata<string> automata)
        {
            if (regExp.Operator == RegExp.OperatorEnum.Plus)
            {
                int prevCount = automata.GetStates().Count + 1;
                automata.AddTransition(new Transition<string>(automata.GetStates().Count.ToString(), '$', prevCount.ToString()));
                Convert(regExp.Left, automata);
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', prevCount.ToString()));
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', automata.GetStates().Count.ToString()));
            }

            if (regExp.Operator == RegExp.OperatorEnum.Star)
            {
                int startState = automata.GetStates().Count;
                int prevCount = automata.GetStates().Count + 1;
                automata.AddTransition(new Transition<string>(automata.GetStates().Count.ToString(), '$', prevCount.ToString()));
                Convert(regExp.Left, automata);
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', prevCount.ToString()));
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', automata.GetStates().Count.ToString()));
                automata.AddTransition(new Transition<string>(startState.ToString(), '$', (automata.GetStates().Count - 1).ToString()));
            }

            if (regExp.Operator == RegExp.OperatorEnum.Or)
            {
                int startState = automata.GetStates().Count;
                automata.AddTransition(new Transition<string>(startState.ToString(), '$', (startState + 1).ToString()));
                Convert(regExp.Left,automata);
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', automata.GetStates().Count.ToString()));
                int endState = automata.GetStates().Count - 1;
                automata.AddTransition(new Transition<string>(startState.ToString(), '$', automata.GetStates().Count.ToString()));
                Convert(regExp.Right,automata);
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', endState.ToString()));
            }

            if (regExp.Operator == RegExp.OperatorEnum.Dot)
            {
                Convert(regExp.Left,automata);
                Convert(regExp.Right,automata);
            }

            if (regExp.Terminals != null)
            {
                foreach (char character in regExp.Terminals)
                {
                    automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), character,automata.GetStates().Count.ToString()));
                }
            }
        }
    }
}