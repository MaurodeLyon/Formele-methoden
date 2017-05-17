using System;
using System.Collections.Generic;
using System.Linq;

namespace Week_1
{
    /// <summary>
    /// The class Automata represents both DFA and NDFA: some NDFA's are also DFA
    /// Using the method isDFA we can check this
    /// 
    /// We use '$' to denote the empty symbol epsilon
    /// 
    /// @author Paul de Mast
    /// @version 1.0
    /// 
    /// </summary>
    public sealed class Automata<T> where T : IComparable
    {
        // Or use a Map structure
        public ISet<Transition<T>> Transitions { get; }

        public SortedSet<T> States { get; }
        public SortedSet<T> startStates;
        public SortedSet<T> FinalStates { get; }
        public SortedSet<char> Symbols { get; set; }

        public Automata() : this(new SortedSet<char>())
        {
        }

        public Automata(char[] s) : this(new SortedSet<char>(s))
        {
        }

        public Automata(SortedSet<char> symbols)
        {
            Transitions = new SortedSet<Transition<T>>();
            States = new SortedSet<T>();
            startStates = new SortedSet<T>();
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
            startStates.Add(t);
        }

        public void DefineAsFinalState(T t)
        {
            States.Add(t);
            FinalStates.Add(t);
        }

        public void PrintTransitions()
        {
            foreach (var t in Transitions)
                Console.WriteLine(t);
        }

        public bool IsDfa()
        {
            bool isDfa = true;
            if (startStates.Count > 1)
            {
                return false;
            }

            foreach (T from in States)
            foreach (char? c in Symbols)
                if (c != null)
                {
                    var symbol = (char) c;
                    isDfa = isDfa && GetToStates(@from, symbol);
                }
            return isDfa;
        }

        public bool GetToStates(T from, char symbol)
        {
            List<Transition<T>> transitionsList = new List<Transition<T>>();
            foreach (Transition<T> transition in Transitions)
            {
                if (from.Equals(transition.FromState))
                {
                    transitionsList.Add(transition);
                }
            }

            List<char> results = transitionsList.Select(e => e.Symbol).ToList();
            int count = 0;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i] == symbol)
                {
                    count++;
                }
                if (results[i] == '$')
                {
                    Console.WriteLine("$ found");
                    return false;
                }
            }
            if (count == 1)
            {
                return true;
            }
            Console.WriteLine("Count wrong: " + count);
            return false;
        }

        public bool Accept(string text)
        {
            foreach (var startState in startStates)
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
            foreach (var possibleTransition in GetTransition(state))
                if (possibleTransition.Symbol == text[index])
                    if (IsPossible(index + 1, text, possibleTransition.ToState))
                        return true;
            return false;
        }

        public List<Transition<T>> GetTransition(T state)
        {
            return Transitions.Where(e => e.FromState.Equals(state)).ToList();
        }

        public void GeefTaalTotLengte(int numberOfLetters)
        {
            NextChar(0, new char[numberOfLetters], new List<string>(), numberOfLetters);
        }

        public void NextChar(int letterIndex, char[] currentWord, List<string> words, int amountOfLetters)
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
        }
    }
}