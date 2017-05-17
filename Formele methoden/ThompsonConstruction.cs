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


        private static int GetNewestState(Automata<string> automata)
        {
            int highestState = 0;
            foreach (string s in automata.States)
            {
                if (highestState < int.Parse(s))
                {
                    highestState = int.Parse(s);
                }
            }
            return highestState;
        }

        private static void Convert(RegExp regExp, Automata<string> automata)
        {
            string lastEndState = "";
            if (automata.FinalStates.Count > 0)
            {
                lastEndState = automata.FinalStates.ToList()[0];
            }

            if (regExp.Operator == RegExp.OperatorEnum.Plus)
            {
                int prevCount = automata.States.Count + 1;
                automata.AddTransition(
                    new Transition<string>(automata.States.Count.ToString(), '$', prevCount.ToString()));
                Convert(regExp.Left, automata);
                automata.AddTransition(new Transition<string>((automata.States.Count - 1).ToString(), '$',
                    prevCount.ToString()));
                automata.AddTransition(new Transition<string>((automata.States.Count - 1).ToString(), '$',
                    automata.States.Count.ToString()));

                automata.FinalStates.Clear();
                automata.DefineAsFinalState((automata.States.Count - 1).ToString());
            }

            if (regExp.Operator == RegExp.OperatorEnum.Star)
            {
                int startState;
                if (lastEndState != "")
                {
                    startState = int.Parse(lastEndState);
                }
                else
                {
                    startState = automata.States.Count;
                }

                int prevCount = automata.States.Count + 1;
                automata.AddTransition(
                    new Transition<string>(automata.States.Count.ToString(), '$', prevCount.ToString()));
                Convert(regExp.Left, automata);
                automata.AddTransition(new Transition<string>((automata.States.Count - 1).ToString(), '$',
                    prevCount.ToString()));
                automata.AddTransition(new Transition<string>((automata.States.Count - 1).ToString(), '$',
                    automata.States.Count.ToString()));
                automata.AddTransition(new Transition<string>(startState.ToString(), '$',
                    (automata.States.Count - 1).ToString()));

                automata.FinalStates.Clear();
                automata.DefineAsFinalState((automata.States.Count - 1).ToString());
            }

            if (regExp.Operator == RegExp.OperatorEnum.Or)
            {
                int startState;
                if (lastEndState != "")
                {
                    startState = int.Parse(lastEndState);
                }
                else
                {
                    startState = automata.States.Count;
                }

                int newestState = GetNewestState(automata);
                automata.AddTransition(
                    new Transition<string>(startState.ToString(), '$', (newestState + 1).ToString()));

                Convert(regExp.Left, automata);
                automata.AddTransition(new Transition<string>((automata.States.Count - 1).ToString(), '$',
                    automata.States.Count.ToString()));
                int endState = automata.States.Count - 1;

                automata.FinalStates.Clear();
                automata.DefineAsFinalState(endState.ToString());

                // See getNewestState check at earlier convert. works in with current expression due to having a simple blackbox. If blackbox itself
                // contains more operators this could fail aswell.

                automata.AddTransition(
                    new Transition<string>(startState.ToString(), '$', automata.States.Count.ToString()));
                Convert(regExp.Right, automata);
                automata.AddTransition(new Transition<string>((automata.States.Count - 1).ToString(), '$',
                    endState.ToString()));
            }

            if (regExp.Operator == RegExp.OperatorEnum.Dot)
            {
                Convert(regExp.Left, automata);

                Convert(regExp.Right, automata);
            }

            if (regExp.Terminals != null)
            {
                foreach (char character in regExp.Terminals)
                {
                    automata.AddTransition(new Transition<string>((automata.States.Count - 1).ToString(), character,
                        automata.States.Count.ToString()));
                }
            }
        }
    }
}