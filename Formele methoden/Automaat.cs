using System;
using System.Collections.Generic;
using System.Linq;

namespace Week_1
{
    public sealed class Automaat<T> where T : IComparable
    {
        public ISet<Transition<T>> Transitions { get; }

        public SortedSet<T> States { get; }
        public SortedSet<T> StartStates { get; set; }
        public SortedSet<T> FinalStates { get; set; }
        public SortedSet<char> Symbols { get; set; }

        public Automaat() : this(new SortedSet<char>())
        {
        }

        public Automaat(char[] s) : this(new SortedSet<char>(s))
        {
        }

        public Automaat(SortedSet<char> symbols)
        {
            Transitions = new SortedSet<Transition<T>>();
            States = new SortedSet<T>();
            StartStates = new SortedSet<T>();
            FinalStates = new SortedSet<T>();
            Symbols = symbols;
        }

        public void AddTransition(Transition<T> t)
        {
            Transitions.Add(t);
            States.Add(t.FromState);
            States.Add(t.ToState);
        }

        public void DefineAsStartState(T t)
        {
            if (!States.Contains(t))
                States.Add(t);

            StartStates.Add(t);
        }

        public void DefineAsFinalState(T t)
        {
            if (!States.Contains(t))
                States.Add(t);

            FinalStates.Add(t);
        }

        public bool IsDfa()
        {
            bool isDfa = !(Transitions.Where(e => e.Symbol.Equals('$')).ToList().Count > 0);
            foreach (T state in States)
            {
                foreach (char symbol in Symbols)
                {
                    isDfa = isDfa && GetToStates(state, symbol).Count <= 1;
                }
            }
            return isDfa;
        }

        public List<Transition<T>> GetToStates(T state, char symbol)
        {
            return Transitions.Where(e => e.Symbol == symbol).Where(e => e.FromState.Equals(state)).ToList();
        }

        public bool Accept(string text)
        {
            foreach (T startState in StartStates)
                if (IsPossible(0, text, startState))
                    return true;
            return false;
        }

        public bool IsPossible(int index, string text, T state)
        {
            if (index == text.Length)
            {
                if (FinalStates.Contains(state))
                    return true;
                return false;
            }
            foreach (Transition<T> possibleTransition in GetTransition(state))
                if (possibleTransition.Symbol == text[index])
                    if (IsPossible(index + 1, text, possibleTransition.ToState))
                        return true;
            return false;
        }

        public List<Transition<T>> GetTransition(T state)
        {
            List<Transition<T>> transitions = Transitions.Where(e => e.FromState.Equals(state)).ToList();
            List<T> epsilonStates = transitions.Where(e => e.Symbol == '$').Select(e => e.ToState).ToList();
            foreach (T epsilonState in epsilonStates)
            {
                transitions.AddRange(GetTransition(epsilonState));
            }
            return transitions;
        }

        public List<string> GeefTaalTotLengte(int numberOfLetters)
        {
            return NextChar(0, new char[numberOfLetters], new List<string>(), numberOfLetters);
        }

        public List<string> NextChar(int letterIndex, char[] currentWord, List<string> words, int amountOfLetters)
        {
            for (int i = 0; i < Symbols.Count; i++)
            {
                currentWord[letterIndex] = Symbols.ToList()[i];

                if (letterIndex == amountOfLetters - 1)
                {
                    words.Add(new string(currentWord));
                }
                else
                {
                    NextChar(letterIndex + 1, currentWord, words, amountOfLetters);
                }
            }
            return words;
        }

        // Merge methods
        enum MergeType
        {
            Union,
            Concatenation
        };

        public static Automaat<string> Union(Automaat<string> dfaA, Automaat<string> dfaB)
        {
            SortedSet<char> mergedAlphabet = dfaA.Symbols;
            mergedAlphabet.UnionWith(dfaB.Symbols);
            Automaat<string> merged = new Automaat<string>(mergedAlphabet);

            Dictionary<char, string> completeMergedState = SetupMerge(dfaA, dfaB);


            AddMergedState(completeMergedState, ref merged, dfaA, dfaB, MergeType.Union);
            // Add new state to merged, work recursively from there 



            return finaliseMerge(merged);
        }

        public Automaat<string> Concatenation(Automaat<string> dfaA, Automaat<string> dfaB)
        {
            SortedSet<char> mergedAlphabet = dfaA.Symbols;
            mergedAlphabet.UnionWith(dfaB.Symbols);
            Automaat<string> merged = new Automaat<string>(mergedAlphabet);

            Dictionary<char, string> completeMergedState = SetupMerge(dfaA, dfaB);


            AddMergedState(completeMergedState, ref merged, dfaA, dfaB, MergeType.Concatenation);
            // Add new state to merged, work recursively from there 
            
            return finaliseMerge(merged);
        }

        private static void AddMergedState(Dictionary<char, string> prevMergedState, ref Automaat<string> merged, Automaat<string> dfaA, Automaat<string> dfaB, MergeType type)
        {
            // string[] states = prevMergedState.Split('_');
            // Add prev      
            int countFinal = 0;
            int countStart = 0;
            string completePrevMergedState = "";
            foreach (KeyValuePair<char, string> entry in prevMergedState)
            {
                completePrevMergedState += entry.Value + "_";

                if (entry.Key == 'A')
                {
                    if (dfaA.FinalStates.Contains(entry.Value))
                        countFinal++;
                    if (dfaA.StartStates.Contains(entry.Value))
                        countStart++;
                }

                else if (entry.Key == 'B')
                {
                    if (dfaB.FinalStates.Contains(entry.Value))
                        countFinal++;
                    if (dfaB.StartStates.Contains(entry.Value))
                        countStart++;
                }
            }

            completePrevMergedState = completePrevMergedState.TrimEnd('_');

            if (type == MergeType.Union && countFinal == prevMergedState.Count)
                merged.DefineAsFinalState(completePrevMergedState);
            else if (type == MergeType.Concatenation && countFinal >= 1)
                merged.DefineAsFinalState(completePrevMergedState);

            if (type == MergeType.Union && countStart == prevMergedState.Count)
                merged.DefineAsStartState(completePrevMergedState);
            else if (type == MergeType.Concatenation && countStart >= 1)
                merged.DefineAsStartState(completePrevMergedState);


            if (merged.GetTransition(completePrevMergedState).Count == merged.Symbols.Count)
                return;

            foreach (char symbol in merged.Symbols)
            {
                Dictionary<char, string> newMergedState = new Dictionary<char, string>();
                // This could break though
                if (CheckExistingRouteForChar(completePrevMergedState, symbol, merged))
                    return;

                foreach (KeyValuePair<char, string> entry in prevMergedState)
                {
                    if (entry.Key == 'A')
                        CollectRoutesFromDfa(entry, symbol, dfaA, ref newMergedState);
                    else if (entry.Key == 'B')
                        CollectRoutesFromDfa(entry, symbol, dfaB, ref newMergedState);
                }

                string completeNewMergedState = "";
                foreach (KeyValuePair<char, string> entry in newMergedState)
                {
                    completeNewMergedState += entry.Value + "_";
                }
                completeNewMergedState = completeNewMergedState.TrimEnd('_');
                merged.AddTransition(new Transition<string>(completePrevMergedState, symbol, completeNewMergedState));


                AddMergedState(newMergedState, ref merged, dfaA, dfaB, type);
            }
        }

        private static void CollectRoutesFromDfa(KeyValuePair<char, string> entry, char symbol, Automaat<string> source,
            ref Dictionary<char, string> newMergedState)
        {
            string[] states = entry.Value.Split('_');
            foreach (string state in states)
            {
                List<Transition<string>> trans = source.GetTransition(state);

                foreach (Transition<string> t in trans)
                {
                    if (t.Symbol == symbol)
                    {
                        if (!newMergedState.ContainsKey(entry.Key))
                            newMergedState.Add(entry.Key, t.ToState + "_");
                        else
                            newMergedState[entry.Key] = newMergedState[entry.Key] + t.ToState + "_";
                    }
                }
                ///TEST THIS
                if(newMergedState.ContainsKey(entry.Key))
                newMergedState[entry.Key] = newMergedState[entry.Key].TrimEnd('_');
            }
        }

        private static bool CheckExistingRouteForChar(string currentState, char symbol, Automaat<string> dfa)
        {
            List<Transition<string>> currentTrans = dfa.GetTransition(currentState);
            foreach (Transition<string> t in currentTrans)
            {
                if (t.Symbol == symbol)
                {
                    return true;
                }
            }
            return false;
        }

        private static Dictionary<char, string> SetupMerge(Automaat<string> dfaA, Automaat<string> dfaB)
        {
            Dictionary<char, string> completeMergedState = new Dictionary<char, string>();
            foreach (string startState in dfaA.StartStates)
            {
                if (!completeMergedState.ContainsKey('A'))
                    completeMergedState.Add('A', startState + "_");
                else
                    completeMergedState['A'] = completeMergedState['A'] + startState + "_";
            }

            foreach (string startState in dfaB.StartStates)
            {
                if (!completeMergedState.ContainsKey('B'))
                    completeMergedState.Add('B', startState + "_");
                else
                    completeMergedState['B'] = completeMergedState['B'] + startState + "_";
            }

            completeMergedState['A'] = completeMergedState['A'].TrimEnd('_');
            completeMergedState['B'] = completeMergedState['B'].TrimEnd('_');


            return completeMergedState;
        }

        private static Automaat<string> finaliseMerge(Automaat<string> merged)
        {
            Automaat<string> finalisedMerge = new Automaat<string>(merged.Symbols);
            
            foreach(Transition<string> t in merged.Transitions)
            {
                finalisedMerge.AddTransition(new Transition<string>(t.FromState.Replace("_", String.Empty),t.Symbol,t.ToState.Replace("_",String.Empty)));
            }

            foreach(string startState in merged.StartStates)
            {
                finalisedMerge.DefineAsStartState(startState.Replace("_", String.Empty));
            }

            foreach(string finalState in merged.FinalStates)
            {
                finalisedMerge.DefineAsFinalState(finalState.Replace("_", String.Empty));
            }


            return finalisedMerge;
        }

        public static Automaat<string> Not(Automaat<string> automaat)
        {
            Automaat<string> notAutomaat = new Automaat<string>(automaat.Symbols);
            //save way of copying transitions
            notAutomaat.StartStates = automaat.StartStates;
            foreach (Transition<string> t in automaat.Transitions)
            {
                notAutomaat.AddTransition(t);
            }

            foreach (string state in notAutomaat.States)
            {
                if (!automaat.FinalStates.Contains(state))
                {
                    notAutomaat.DefineAsFinalState(state);
                }
            }
            return notAutomaat;
        }


        public static Automaat<string> GenerateDfa(DfaGenerateValue param, char[] symbols)
        {
            Automaat<string> dfa = new Automaat<string>(symbols);

            if (param.Parameter == "")
                return null;

            ///Check alphabet and parameter
            
            foreach(char c in param.Parameter.ToCharArray())
            {
                if (!dfa.Symbols.Contains(c))
                    return null;
            }

            switch (param.Type)
            {
                case GeneratorType.BeginsWith:
                    beginsWith(param, ref dfa);       
                    break;
                case GeneratorType.Contains:
                    contains(param, ref dfa);
                    break;
                case GeneratorType.EndsWith:
                    endsWith(param, ref dfa);
                    break;
            }
            if (param.IsNot)
                return Not(dfa);
            else
                return dfa;
        }

        private static void beginsWith(DfaGenerateValue param, ref Automaat<string> dfa)
        {
            char[] chars = param.Parameter.ToCharArray();
            int stateCounter = 0;
            dfa.DefineAsStartState(stateCounter.ToString());
            foreach (char c in chars)
            {
                dfa.AddTransition(new Transition<string>(stateCounter.ToString(), c,
                    (stateCounter + 1).ToString()));

                stateCounter = dfa.States.Count - 1;
            }

            dfa.DefineAsFinalState(stateCounter.ToString());
            foreach (char c in dfa.Symbols)
            {
                dfa.AddTransition(new Transition<string>(stateCounter.ToString(), c, stateCounter.ToString()));
            }

            //Hardcopy states
            SortedSet<string> ogStates = new SortedSet<string>();
            foreach (string state in dfa.States)
            {
                ogStates.Add(state);
            }

            foreach (string state in ogStates)
            {
                List<Transition<string>> trans = dfa.GetTransition(state);

                SortedSet<char> routesPresent = new SortedSet<char>();
                foreach (Transition<string> t in trans)
                {
                    routesPresent.Add(t.Symbol);
                }

                foreach (char letter in dfa.Symbols)
                {
                    if (!routesPresent.Contains(letter))
                    {
                        dfa.AddTransition(new Transition<string>(state, letter, "F"));
                    }
                }

            }

            if(dfa.States.Contains("F"))
            {
                foreach(char letter in dfa.Symbols)
                {
                    dfa.AddTransition(new Transition<string>("F", letter, "F"));
                }
            }
        }

        private static void contains(DfaGenerateValue param, ref Automaat<string> dfa)
        {
            char[] chars = param.Parameter.ToCharArray();
            int stateCounter = 0;
            dfa.DefineAsStartState(stateCounter.ToString());
            foreach (char c in chars)
            {
                dfa.AddTransition(new Transition<string>(stateCounter.ToString(), c,
                    (stateCounter + 1).ToString()));

                stateCounter = dfa.States.Count - 1;
            }

            dfa.DefineAsFinalState(stateCounter.ToString());

            //Hardcopy states
            List<string> ogStates = new List<string>();
            foreach (string state in dfa.States)
            {
                ogStates.Add(state);
            }

            for (int i = 0; i < ogStates.Count; i++)
            {
                string state = ogStates[i];

                List<Transition<string>> trans = dfa.GetTransition(state);

                SortedSet<char> routesPresent = new SortedSet<char>();
                foreach (Transition<string> t in trans)
                {
                    routesPresent.Add(t.Symbol);
                }

                foreach (char letter in dfa.Symbols)
                {
                    if (!routesPresent.Contains(letter)&&!dfa.FinalStates.Contains(state))
                    {
                        int stateToReturnTo = backTrackForWorkingRoute(chars, letter);

                        dfa.AddTransition(new Transition<string>(state, letter, ogStates[stateToReturnTo]));

                    }
                }


            }

            foreach (char c in dfa.Symbols)
            {
                foreach(string finalstate in dfa.FinalStates)
                    dfa.AddTransition(new Transition<string>(stateCounter.ToString(), c, stateCounter.ToString()));
            }


        }

        private static void endsWith(DfaGenerateValue param, ref Automaat<string> dfa)
        {
            char[] chars = param.Parameter.ToCharArray();
            int stateCounter = 0;
            dfa.DefineAsStartState(stateCounter.ToString());
            foreach (char c in chars)
            {
                dfa.AddTransition(new Transition<string>(stateCounter.ToString(), c,
                    (stateCounter + 1).ToString()));

                stateCounter = dfa.States.Count - 1;
            }

            dfa.DefineAsFinalState(stateCounter.ToString());

            //Hardcopy states
            List<string> ogStates = new List<string>();
            foreach (string state in dfa.States)
            {
                ogStates.Add(state);
            }

            for(int i=0; i < ogStates.Count; i++)
            {
                string state = ogStates[i];

                List<Transition<string>> trans = dfa.GetTransition(state);

                SortedSet<char> routesPresent = new SortedSet<char>();
                foreach (Transition<string> t in trans)
                {
                    routesPresent.Add(t.Symbol);
                }

                foreach (char letter in dfa.Symbols)
                {
                    if (!routesPresent.Contains(letter))
                    {
                        int stateToReturnTo =backTrackForWorkingRoute(chars, letter);

                        dfa.AddTransition(new Transition<string>(state, letter, ogStates[stateToReturnTo]));

                    }
                }


            }
            


        }

        private static int backTrackForWorkingRoute(char[] route, char toUse)
        {
            string completeRoute = new string(route);

            for (int i = route.Length - 1; i >= 0; i--)
            {
                string tobeAdded = "";

                for(int j=i; j< route.Length; j++)
                {
                    tobeAdded += route[j];
                }

                string routeTocheck = (toUse.ToString() + tobeAdded);
                if (routeTocheck.Contains(completeRoute))
                {
                    return i;
                }

            }

            return -1;
        }
        public enum GeneratorType
        {
            BeginsWith,
            Contains,
            EndsWith
        }

        public struct DfaGenerateValue
        {
            public bool IsNot;
            public string Parameter;
            public GeneratorType Type;
        }
    }
}