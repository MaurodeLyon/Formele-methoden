using System.Collections.Generic;
using System.Linq;

namespace Week_1
{
    public class ThompsonConstruction
    {
        public static Automaat<string> ConvertRegExp(RegExp regExp)
        {
            Automaat<string> automaat = new Automaat<string>();

            automaat.DefineAsStartState("0");
            automaat.DefineAsFinalState("1");
            int stateCounter = 2;

            Convert(regExp, ref automaat, ref stateCounter, 0, 1);
            automaat.Symbols =new SortedSet<char>(automaat.Transitions.Distinct().Select(e => e.Symbol).ToList());
            //Epsilon should not be in alphabet
            automaat.Symbols.Remove('$');
            return automaat;
        }

        public static void Convert(RegExp regExp, ref Automaat<string> automaat, ref int stateCounter, int leftState, int rightState)
        {
            switch (regExp.Operator)
            {
                case RegExp.OperatorEnum.Plus:
                    Plus(regExp, ref automaat, ref stateCounter, leftState, rightState);
                    break;
                case RegExp.OperatorEnum.Star:
                    Star(regExp, ref automaat, ref stateCounter, leftState, rightState);
                    break;
                case RegExp.OperatorEnum.Or:
                    Or(regExp, ref automaat, ref stateCounter, leftState, rightState);
                    break;
                case RegExp.OperatorEnum.Dot:
                    Dot(regExp, ref automaat, ref stateCounter, leftState, rightState);
                    break;
                case RegExp.OperatorEnum.One:
                    One(regExp, ref automaat, ref stateCounter, leftState, rightState);
                    break;
            }
        }

        public static void Plus(RegExp regExp, ref Automaat<string> automaat, ref int stateCounter, int leftState, int rightState)
        {
            int stateTwo = stateCounter;
            int stateThree = stateCounter + 1;
            stateCounter = stateCounter + 2;
            automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', stateTwo.ToString()));
            automaat.AddTransition(new Transition<string>(stateThree.ToString(), '$', stateTwo.ToString()));
            automaat.AddTransition(new Transition<string>(stateThree.ToString(), '$', rightState.ToString()));
            Convert(regExp.Left, ref automaat, ref stateCounter, stateTwo, stateThree);
        }

        public static void Star(RegExp regExp, ref Automaat<string> automaat, ref int stateCounter, int leftState,
            int rightState)
        {
            int stateTwo = stateCounter;
            int stateThree = stateCounter + 1;
            stateCounter = stateCounter + 2;
            automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', stateTwo.ToString()));
            automaat.AddTransition(new Transition<string>(stateThree.ToString(), '$', stateTwo.ToString()));
            automaat.AddTransition(new Transition<string>(stateThree.ToString(), '$', rightState.ToString()));
            automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', rightState.ToString()));
            Convert(regExp.Left, ref automaat, ref stateCounter, stateTwo, stateThree);
        }

        public static void Or(RegExp regExp, ref Automaat<string> automaat, ref int stateCounter, int leftState, int rightState)
        {
            int state2 = stateCounter;
            int state3 = stateCounter + 1;
            int state4 = stateCounter + 2;
            int state5 = stateCounter + 3;
            stateCounter = stateCounter + 4;
            automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', state2.ToString()));
            automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', state4.ToString()));
            automaat.AddTransition(new Transition<string>(state3.ToString(), '$', rightState.ToString()));
            automaat.AddTransition(new Transition<string>(state5.ToString(), '$', rightState.ToString()));
            Convert(regExp.Left, ref automaat, ref stateCounter, state2, state3);
            Convert(regExp.Right, ref automaat, ref stateCounter, state4, state5);
        }

        public static void Dot(RegExp regExp, ref Automaat<string> automaat, ref int stateCounter, int leftState, int rightState)
        {
            int midState = stateCounter;
            stateCounter++;
            Convert(regExp.Left, ref automaat, ref stateCounter, leftState, midState);
            Convert(regExp.Right, ref automaat, ref stateCounter, midState, rightState);
        }

        public static void One(RegExp regExp, ref Automaat<string> automaat, ref int stateCounter, int leftState, int rightState)
        {
            char[] characters = regExp.Terminals.ToCharArray();
            if (characters.Length == 1)
            {
                automaat.AddTransition(
                    new Transition<string>(leftState.ToString(), characters[0], rightState.ToString()));
            }
            else
            {
                automaat.AddTransition(
                    new Transition<string>(leftState.ToString(), characters[0], stateCounter.ToString()));
                int i = 1;
                while (i < characters.Length - 1)
                {
                    automaat.AddTransition(new Transition<string>(stateCounter.ToString(), characters[i],
                        (stateCounter + 1).ToString()));
                    stateCounter++;
                    i++;
                }
                automaat.AddTransition(
                    new Transition<string>(stateCounter.ToString(), characters[i], rightState.ToString()));
                stateCounter++;
            }
        }
    }
}