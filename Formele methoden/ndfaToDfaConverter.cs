using System;
using System.Collections.Generic;
using System.Linq;

namespace Week_1
{
    public class NdfaToDfaConverter
    {
        public static Automata<string> Convert(Automata<string> automata)
        {
            Automata<string> m = new Automata<string>(automata.Symbols);
            SortedSet<string> states = automata.States;
            ConvertState(states.ToList()[0], ref m, ref automata);

            if (m.States.Contains("FFFF"))
            {
                foreach (char route in m.Symbols)
                {
                    m.AddTransition(new Transition<string>("FFFF", route, "FFFF"));
                }
            }

            return m;
        }

        private static void ConvertState(string state, ref Automata<string> dest, ref Automata<string> source)
        {
            if (dest.GetTransition(state).Count == 2)
            {
                return;
            }
            string[] states = state.Split('_');

            foreach (char c in source.Symbols)
            {
                int[] counts = new int[states.Length];

                for (int i = 0; i < states.Length; i++)
                {
                    List<Transition<string>> transitions = source.GetTransition(states[i]);
                    int count = CheckAmountOfRoutesForChar(c, transitions);
                    counts[i] = count;
                }

                int amountOfRoutes = 0;
                foreach (int i in counts)
                {
                    if (i > amountOfRoutes)
                    {
                        amountOfRoutes = i;
                    }
                }

                if (amountOfRoutes == 0)
                {
                    dest.AddTransition(new Transition<string>(state, c, "FFFF"));
                }
                string toState = "";
                bool isFinalState = false;
                SortedSet<String> newState = new SortedSet<string>();
                if (amountOfRoutes >= 1)
                {
                    foreach (string s in states)
                    {
                        List<Transition<string>> trans = source.GetTransition(s);
                        foreach (Transition<string> t in trans)
                        {
                            if (t.Symbol == c)
                            {
                                newState.Add(t.ToState);
                                if (source.FinalStates.Contains(t.ToState))
                                {
                                    isFinalState = true;
                                }
                            }
                        }
                    }

                    foreach (string subState in newState)
                    {
                        toState += subState;
                        toState += "_";
                    }
                    if (newState.Count >= 1)
                    {
                        toState = toState.TrimEnd('_');
                    }

                    dest.AddTransition(new Transition<string>(state, c, toState));
                    if (source.FinalStates.Contains(state))
                    {
                        dest.DefineAsFinalState(state);
                    }
                    if (isFinalState)
                        dest.DefineAsFinalState(toState);

                    if (state != toState)
                        ConvertState(toState, ref dest, ref source);
                }
            }
        }

        private static int CheckAmountOfRoutesForChar(char c, List<Transition<string>> transitions)
        {
            int count = 0;

            foreach (Transition<string> t in transitions)
            {
                if (t.Symbol == c)
                {
                    count++;
                }
            }

            return count;
        }
    }
}