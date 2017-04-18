using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    class Transition : IComparable<Transition>
    {
        public const char EPSILON = '$';

        private Transition fromState;
        private char symbol;
        private Transition toState;

        // this constructor can be used to define loops:
        public Transition(Transition fromOrTo, char s) : this(fromOrTo, s, fromOrTo)
        {
        }

        public Transition(Transition from, Transition to) : this(from, EPSILON, to)
        {
        }

        public Transition(Transition from, char s, Transition to)
        {
            this.fromState = from;
            this.symbol = s;
            this.toState = to;
        }

        public int CompareTo(Transition transition)
        {
            int fromCmp = fromState.CompareTo(transition.fromState);
            int symbolCmp = symbol.CompareTo(transition.symbol);
            int toCmp = toState.CompareTo(transition.toState);

            return (fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp));
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            else if (other is Transition)
            {
                return this.fromState.Equals(((Transition) other).fromState)
                       && this.toState.Equals(((Transition) other).toState)
                       && this.symbol == (((Transition) other).symbol);
            }
            else return false;
        }


        public Transition getFromState()
        {
            return fromState;
        }

        public Transition getToState()
        {
            return toState;
        }

        public char getSymbol()
        {
            return symbol;
        }

        public String toString()
        {
            return "(" + this.getFromState() + ", " + this.getSymbol() + ")" + "-->" + this.getToState();
        }
    }
}