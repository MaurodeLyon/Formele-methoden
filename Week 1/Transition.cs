using System;

namespace Week_1
{
    class Transition : IComparable<Transition>
    {
        public const char Epsilon = '$';

        private readonly Transition _fromState;
        private readonly char _symbol;
        private readonly Transition _toState;

        // this constructor can be used to define loops:
        public Transition(Transition fromOrTo, char s) : this(fromOrTo, s, fromOrTo)
        {
        }

        public Transition(Transition from, Transition to) : this(from, Epsilon, to)
        {
        }

        public Transition(Transition from, char s, Transition to)
        {
            _fromState = from;
            _symbol = s;
            _toState = to;
        }

        public int CompareTo(Transition transition)
        {
            int fromCmp = _fromState.CompareTo(transition._fromState);
            int symbolCmp = _symbol.CompareTo(transition._symbol);
            int toCmp = _toState.CompareTo(transition._toState);

            return (fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp));
        }

        public override bool Equals(object other)
        {
            Transition transition = other as Transition;
            if (transition != null)
            {
                return _fromState.Equals(transition._fromState)
                       && _toState.Equals(transition._toState)
                       && _symbol == (transition._symbol);
            }
            return false;
        }

        protected bool Equals(Transition other)
        {
            return Equals(_fromState, other._fromState) && _symbol == other._symbol && Equals(_toState, other._toState);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_fromState != null ? _fromState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _symbol.GetHashCode();
                hashCode = (hashCode * 397) ^ (_toState != null ? _toState.GetHashCode() : 0);
                return hashCode;
            }
        }

        public Transition GetFromState()
        {
            return _fromState;
        }

        public Transition GetToState()
        {
            return _toState;
        }

        public char GetSymbol()
        {
            return _symbol;
        }

        public override string ToString()
        {
            return "(" + GetFromState() + ", " + GetSymbol() + ")" + "-->" + GetToState();
        }
    }
}