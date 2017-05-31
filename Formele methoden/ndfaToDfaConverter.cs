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

            foreach (string startState in ndfa.StartStates)
            {
                ConvertState(startState, ref dfa, ref ndfa);
                dfa.DefineAsStartState(startState);
            }

            if (dfa.States.Contains("F"))
            {
                foreach (char route in dfa.Symbols)
                {
                    dfa.AddTransition(new Transition<string>("F", route, "F"));
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
                List<Transition<string>> currentTrans = dfa.GetTransition(currentState);
                foreach (Transition<string> t in currentTrans)
                {
                    if (t.Symbol == symbol)
                    {
                        return;
                    }
                }

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
                    dfa.AddTransition(new Transition<string>(currentState, symbol, "F"));
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

        public static Automaat<string> Reverse(Automaat<string> automaat)
        {
            SortedSet<string> finalstates = automaat.FinalStates;
            SortedSet<string> startStates = automaat.StartStates;

            foreach (Transition<String> t in automaat.Transitions)
            {
                string fromState = t.FromState;
                string toState = t.ToState;

                t.FromState = toState;
                t.ToState = fromState;
            }

            automaat.FinalStates = startStates;
            automaat.StartStates = finalstates;

            return automaat;
        }
    }
}