using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public class Automata<T> where T : IComparable
    {
        // Or use a Map structure
        private ISet<Transition<T>> transitions;

        private SortedSet<T> states;
        private SortedSet<T> startStates;
        private SortedSet<T> finalStates;
        private SortedSet<char?> symbols;

        public ISet<Transition<T>> getTransitions()
        {
            return transitions;
        }
        
        public SortedSet<T> getFinalStates()
        {
            return finalStates;
        }

        public Automata() : this(new SortedSet<char?>())
        {
        }

        public Automata(char?[] s) : this(new SortedSet<char?>(s))
        {
        }

        public Automata(SortedSet<char?> symbols)
        {
            transitions = new SortedSet<Transition<T>>();
            states = new SortedSet<T>();
            startStates = new SortedSet<T>();
            finalStates = new SortedSet<T>();
            this.setAlphabet(symbols);
        }

        public virtual void setAlphabet(char?[] s)
        {
            this.setAlphabet(new SortedSet<char?>(s));
        }

        public virtual void setAlphabet(SortedSet<char?> symbols)
        {
            this.symbols = symbols;
        }

        public virtual SortedSet<char?> getAlphabet()
        {
            return symbols;
        }

        public virtual void addTransition(Transition<T> t)
        {
            transitions.Add(t);
            states.Add(t.FromState);
            states.Add(t.ToState);
        }

        public virtual void defineAsStartState(T t)
        {
            // if already in states no problem because a Set will remove duplicates.
            states.Add(t);
            startStates.Add(t);
        }

        public virtual void defineAsFinalState(T t)
        {
            // if already in states no problem because a Set will remove duplicates.
            states.Add(t);
            finalStates.Add(t);
        }

        public virtual void printTransitions()
        {
            foreach (Transition<T> t in transitions)
            {
                Console.WriteLine(t);
            }
        }

        public bool IsDfa()
        {
            bool isDfa = true;

            foreach (T from in states)
            {
                foreach (char symbol in symbols)
                {
                    isDfa = isDfa && GetToStates(from, symbol);
                }
            }
            return isDfa;
        }

        public bool GetToStates(T from, char symbol)
        {
            List<Transition<T>> transitionsList = new List<Transition<T>>();
            foreach (Transition<T> transition in transitions)
            {
                if (from.Equals(transition.FromState))
                {
                    transitionsList.Add(transition);
                }
            }

            List<char> results = transitionsList.Select(e => e.Symbol).ToList();
            for (int i = 0; i < results.Count; i++)
            {
                for (int j = i + 1; j < results.Count; j++)
                {
                    if (results[i] == results[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Accept(string text)
        {
            foreach (T startState in startStates)
            {
                if (IsPossible(0, text, startState))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsPossible(int index, string text, T state)
        {
            if (index == text.Length)
            {
                if (finalStates.Contains(state))
                {
                    return true;
                }
                return false;
            }
            foreach (Transition<T> possibleTransition in GetTransition(state))
            {
                if (possibleTransition.Symbol == text[index])
                {
                    if (IsPossible(index + 1, text, possibleTransition.ToState))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Transition<T>> GetTransition(T state)
        {
            return transitions.Where(e => e.FromState.Equals(state)).ToList();
        }
    }
}