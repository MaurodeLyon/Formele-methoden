using System;
using System.Collections.Generic;
using System.Linq;

namespace Week_1
{
    public sealed class Automaat<T> where T : IComparable
    {
        public ISet<Transition<T>> Transitions { get; }

        public SortedSet<T> States { get; }
        public SortedSet<T> StartStates { get; }
        public SortedSet<T> FinalStates { get; }
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
    }
}