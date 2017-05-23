using System;
using System.Collections.Generic;
using System.Linq;

namespace Week_1
{
    public class NdfaToDfaConverter
    {
        public static Automaat<string> Convert(Automaat<string> ndfa)
        {
            Automaat<string> dfa = new Automaat<string>(ndfa.Symbols);
            ConvertState(ndfa.States.ToList()[0], ref dfa, ref ndfa);
            if (dfa.States.Contains("FFFF"))
            {
                foreach (char route in dfa.Symbols)
                {
                    dfa.AddTransition(new Transition<string>("FFFF", route, "FFFF"));
                }
            }
            return dfa;
        }

        private static void ConvertState(string currentState, ref Automaat<string> dfa, ref Automaat<string> ndfa)
        {
            if (dfa.GetTransition(currentState).Count == ndfa.Symbols.Count)
            {
                return;
            }
            string[] states = currentState.Split('_');

            foreach (char symbol in ndfa.Symbols)
            {
                int[] counts = new int[states.Length];

                for (int i = 0; i < states.Length; i++)
                {
                    counts[i] = ndfa.GetTransition(states[i]).Count(transition => transition.Symbol == symbol);
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
                    dfa.AddTransition(new Transition<string>(currentState, symbol, "FFFF"));
                }

                string toState = "";
                bool isFinalState = false;
                SortedSet<String> newState = new SortedSet<string>();
                if (amountOfRoutes >= 1)
                {
                    foreach (string state in states)
                    {
                        List<Transition<string>> trans = ndfa.GetTransition(state);
                        foreach (Transition<string> t in trans)
                        {
                            if (t.Symbol == symbol)
                            {
                                newState.Add(t.ToState);
                                if (ndfa.FinalStates.Contains(t.ToState))
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

                    dfa.AddTransition(new Transition<string>(currentState, symbol, toState));
                    if (ndfa.FinalStates.Contains(currentState))
                    {
                        dfa.DefineAsFinalState(currentState);
                    }
                    if (isFinalState)
                        dfa.DefineAsFinalState(toState);

                    if (currentState != toState)
                        ConvertState(toState, ref dfa, ref ndfa);
                }
            }
        }
    }
}