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
        private ISet<Transition<T>> transitions;

        private SortedSet<T> states;
        private SortedSet<T> startStates;
        private SortedSet<T> finalStates;
        private SortedSet<char> _symbols;

        public ISet<Transition<T>> GetTransitions()
        {
            return transitions;
        }

        public SortedSet<T> GetFinalStates()
        {
            return finalStates;
        }

        public Automata() : this(new SortedSet<char>())
        {
        }

        public Automata(char[] s) : this(new SortedSet<char>(s))
        {
        }

        public Automata(SortedSet<char> symbols)
        {
            transitions = new SortedSet<Transition<T>>();
            states = new SortedSet<T>();
            startStates = new SortedSet<T>();
            finalStates = new SortedSet<T>();
            SetAlphabet(symbols);
        }

        public void SetAlphabet(char[] s)
        {
            SetAlphabet(new SortedSet<char>(s));
        }

        public void SetAlphabet(SortedSet<char> symbols)
        {
            _symbols = symbols;
        }

        public SortedSet<char> GetAlphabet()
        {
            return _symbols;
        }

        public void AddTransition(Transition<T> t)
        {
            transitions.Add(t);
            states.Add(t.FromState);
            states.Add(t.ToState);
        }

        public void DefineAsStartState(T t)
        {
            // if already in states no problem because a Set will remove duplicates.
            states.Add(t);
            startStates.Add(t);
        }

        public void DefineAsFinalState(T t)
        {
            // if already in states no problem because a Set will remove duplicates.
            states.Add(t);
            finalStates.Add(t);
        }

        public void PrintTransitions()
        {
            foreach (var t in transitions)
                Console.WriteLine(t);
        }

        public bool IsDfa()
        {
            var isDfa = true;

            foreach (var from in states)
            foreach (char? c in _symbols)
                if (c != null)
                {
                    var symbol = (char) c;
                    isDfa = isDfa && GetToStates(@from, symbol);
                }
            return isDfa;
        }

        public bool GetToStates(T from, char symbol)
        {
            var transitionsList = new List<Transition<T>>();
            foreach (var transition in transitions)
                if (@from.Equals(transition.FromState))
                    transitionsList.Add(transition);

            var results = transitionsList.Select(e => e.Symbol).ToList();
            for (var i = 0; i < results.Count; i++)
            for (var j = i + 1; j < results.Count; j++)
                if (results[i] == results[j])
                    return false;
            return true;
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
                if (finalStates.Contains(state))
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
            return transitions.Where(e => e.FromState.Equals(state)).ToList();
        }

        public void GeefTaalTotLengte(int numberOfLetters)
        {
            NextChar(0, new char[numberOfLetters], new List<string>(), numberOfLetters);
        }

        public void NextChar(int letterIndex, char[] currentWord, List<string> words, int amountOfLetters)
        {
            for (int i = 0; i < _symbols.Count; i++)
            {
                currentWord[letterIndex] = _symbols.ToList()[i];

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