﻿using System;
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
            States.Add(t);
            StartStates.Add(t);
        }

        public void DefineAsFinalState(T t)
        {
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



        public static Automaat<string> Union(Automaat<string> dfaA, Automaat<string> dfaB)
        {
            //string completeStartState = "";
            SortedSet<char> mergedAlphabet = dfaA.Symbols;
            mergedAlphabet.UnionWith(dfaB.Symbols);
            Automaat<string> merged = new Automaat<string>(mergedAlphabet);

            //foreach(string startState in dfaA.StartStates)
            //    completeStartState += startState + "_";

            //foreach (string startState in dfaB.StartStates)
            //    completeStartState += startState + "_";

            //completeStartState = completeStartState.TrimEnd('_');
            Dictionary<char, string> completeMergedState = new Dictionary<char, string>();
            foreach(string startState in dfaA.StartStates)
            {
                if (!completeMergedState.ContainsKey('A'))
                    completeMergedState.Add('A', startState + "_");
                else
                    completeMergedState['A'] = completeMergedState['A'] + startState + "_";
    
            }

            foreach (string startState in dfaA.StartStates)
            {
                if (!completeMergedState.ContainsKey('B'))
                    completeMergedState.Add('B', startState + "_");
                else
                    completeMergedState['B'] = completeMergedState['A'] + startState + "_";

            }

            completeMergedState['A'] = completeMergedState['A'].TrimEnd('_');
            completeMergedState['B'] = completeMergedState['B'].TrimEnd('_');

            AddMergedState(completeMergedState, ref merged, dfaA, dfaB);
            //Add new state to merged, work recursively from there 

            return merged;

            return null;
        }

        public Automaat<string> Concatenation(Automaat<string> other)
        {

            return null;
        }

        private static void AddMergedState(Dictionary<char,string> prevMergedState, ref Automaat<string> merged, Automaat<string> dfaA, Automaat<string> dfaB)
        {
            //string[] states = prevMergedState.Split('_');
            //Add prev
            string completePrevMergedState = "";
            foreach(KeyValuePair<char,string> entry in prevMergedState)
            {
                completePrevMergedState += entry.Value + "_";
            }
            completePrevMergedState= completePrevMergedState.TrimEnd('_');

            if (merged.GetTransition(completePrevMergedState).Count == merged.Symbols.Count)
                return;



           

            foreach ( char symbol in merged.Symbols)
            {
                Dictionary<char, string> newMergedState = new Dictionary<char, string>();
                ///This could break though
                if (CheckExistingRouteForChar(completePrevMergedState, symbol, merged))
                    return;

                foreach(KeyValuePair<char,string> entry in prevMergedState)
                {
                    string[] states = entry.Value.Split('_');
                    if(entry.Key=='A')
                    {
                        foreach(string state in states)
                        {
                            List<Transition<string>> trans = dfaA.GetTransition(state);

                            foreach(Transition<string> t in trans)
                            {
                                if(t.Symbol == symbol)
                                {
                                    if (!newMergedState.ContainsKey('A'))
                                        newMergedState.Add('A', t.ToState + "_");
                                    else
                                        newMergedState['A'] = newMergedState['A'] + t.ToState + "_";
                                }
                            }
                            newMergedState['A'] = newMergedState['A'].TrimEnd('_');
                        }
                        
                    }
                    if(entry.Key=='B')
                    {
                        foreach (string state in states)
                        {
                            List<Transition<string>> trans = dfaB.GetTransition(state);

                            foreach (Transition<string> t in trans)
                            {
                                if (t.Symbol == symbol)
                                {
                                    if (!newMergedState.ContainsKey('B'))
                                        newMergedState.Add('B', t.ToState + "_");
                                    else
                                        newMergedState['B'] = newMergedState['B'] + t.ToState + "_";
                                }
                            }
                            newMergedState['B'] = newMergedState['B'].TrimEnd('_');
                        }
                    }
                }

                string completeNewMergedState = "";
                foreach (KeyValuePair<char, string> entry in newMergedState)
                {
                    
                    completeNewMergedState += entry.Value + "_";
                }
                completeNewMergedState = completeNewMergedState.TrimEnd('_');
                merged.AddTransition(new Transition<string>(completePrevMergedState, symbol, completeNewMergedState));

                AddMergedState(newMergedState, ref merged, dfaA, dfaB);
            }


            return;
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

    }


}