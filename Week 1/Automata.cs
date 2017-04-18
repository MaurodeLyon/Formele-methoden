using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    class Automata
    {
        // Or use a Map structure
        private HashSet<Transition> transitions;

        private SortedSet<Transition> states;
        private SortedSet<Transition> startStates;
        private SortedSet<Transition> finalStates;
        private SortedSet<char> symbols;

        public Automata() : this(new SortedSet<char>())
        {
        }

        public Automata(char[] s) : this(new SortedSet<char>(s.ToList()))
        {
        }

        public Automata(SortedSet<char> symbols)
        {
            transitions = new HashSet<Transition>();
            states = new SortedSet<Transition>();
            startStates = new SortedSet<Transition>();
            finalStates = new SortedSet<Transition>();
            this.setAlphabet(symbols);
        }

        public void setAlphabet(char[] s)
        {
            this.setAlphabet(new SortedSet<char>(s.ToList()));
        }

        public void setAlphabet(SortedSet<char> symbols)
        {
            this.symbols = symbols;
        }

        public SortedSet<char> getAlphabet()
        {
            return symbols;
        }

        public void addTransition(Transition t)
        {
            transitions.Add(t);
            states.Add(t.getFromState());
            states.Add(t.getToState());
        }

        public void defineAsStartState(Transition t)
        {
            // if already in states no problem because a Set will remove duplicates.
            states.Add(t);
            startStates.Add(t);
        }

        public void defineAsFinalState(Transition t)
        {
            // if already in states no problem because a Set will remove duplicates.
            states.Add(t);
            finalStates.Add(t);
        }

        public void printTransitions()
        {
            foreach (Transition transition in transitions)
            {
                Console.WriteLine(transition);
            }
        }

        /*public bool isDFA()
        {
            bool isDFA = true;

            foreach (Transition from in states)
            {
                foreach (char symbol in symbols)
                {
                    isDFA = isDFA && GetToStates(from, symbol);
                }
            }

            return isDFA;
        }

        private bool GetToStates(Transition from, char symbol)
        {
            if (states.Select(e => e.getFromState().Equals(from.getFromState())).Select(e => e.getSymbol() == symbol).Count() == 1)
            {
                return true;
            }
            return false;
        }*/
    }
}