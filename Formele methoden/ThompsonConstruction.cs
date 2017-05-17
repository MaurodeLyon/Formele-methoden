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


        private static int getNewestState(Automata<string> automata)
        {
            int highestState = 0;
            foreach(string s in automata.getStates())
            {
                if(highestState < int.Parse(s))
                {
                    highestState = int.Parse(s);
                }
            }
            return highestState;
        }
        private static void Convert(RegExp regExp, Automata<string> automata)
        {
            string lastEndState="";
            if (automata.GetFinalStates().Count>0)
            {
               lastEndState=automata.GetFinalStates().ToList()[0];
              
                
            }
            
            if (regExp.Operator == RegExp.OperatorEnum.Plus)
            {
                int prevCount = automata.GetStates().Count + 1;
                automata.AddTransition(new Transition<string>(automata.GetStates().Count.ToString(), '$', prevCount.ToString()));
                Convert(regExp.Left, automata);
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', prevCount.ToString()));
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', automata.GetStates().Count.ToString()));

                automata.GetFinalStates().Clear();
                automata.DefineAsFinalState((automata.GetStates().Count - 1).ToString());

            }

            if (regExp.Operator == RegExp.OperatorEnum.Star)
            {
                int startState;
                if (lastEndState != "") { startState = int.Parse(lastEndState); }
                else { startState = automata.GetStates().Count; }

                int prevCount = automata.GetStates().Count + 1;
                automata.AddTransition(new Transition<string>(automata.GetStates().Count.ToString(), '$', prevCount.ToString()));
                Convert(regExp.Left, automata);
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', prevCount.ToString()));
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', automata.GetStates().Count.ToString()));
                automata.AddTransition(new Transition<string>(startState.ToString(), '$', (automata.GetStates().Count - 1).ToString()));

                automata.GetFinalStates().Clear();
                automata.DefineAsFinalState((automata.GetStates().Count - 1).ToString());
            }

            if (regExp.Operator == RegExp.OperatorEnum.Or)
            {
                int startState;
                if (lastEndState != "") { startState = int.Parse(lastEndState); }
                else {startState = automata.GetStates().Count; }

                int newestState = getNewestState(automata);
                automata.AddTransition(new Transition<string>(startState.ToString(), '$', (newestState + 1).ToString()));

                Convert(regExp.Left,automata);
                automata.AddTransition(new Transition<string>((automata.GetStates().Count - 1).ToString(), '$', automata.GetStates().Count.ToString()));
                int endState = automata.GetStates().Count - 1;

                automata.GetFinalStates().Clear();
                automata.DefineAsFinalState(endState.ToString());

                ///See getNewestState check at earlier convert. works in with current expression due to having a simple blackbox. If blackbox itself
                ///contains more operators this could fail aswell.

                newestState = getNewestState(automata);
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