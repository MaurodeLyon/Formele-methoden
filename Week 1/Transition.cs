using System;
using System.Collections.Generic;

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
    /// </summary>
    public class Transition<T> : IComparable<Transition<T>> where T : IComparable
    {
        public const char Epsilon = '$';

        private readonly T _fromState;
        private readonly char _symbol;
        private readonly T _toState;

        public T GetFromState()
        {
            return _fromState;
        }

        public char GetSymbol()
        {
            return _symbol;
        }

        public T GetToState()
        {
            return _toState;
        }

        // this constructor can be used to define loops:
        public Transition(T fromOrTo, char s) : this(fromOrTo, s, fromOrTo)
        {
        }

        public Transition(T from, T to) : this(from, Epsilon, to)
        {
        }


        public Transition(T from, char s, T to)
        {
            _fromState = from;
            _symbol = s;
            _toState = to;
        }

        public override bool Equals(object other)
        {
            var transition = other as Transition<T>;
            if (transition != null)
            {
                return _fromState.Equals(transition._fromState) &&
                       _toState.Equals(transition._toState) &&
                       _symbol == (transition._symbol);
            }
            return false;
        }

        protected bool Equals(Transition<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_fromState, other._fromState)
                   && _symbol == other._symbol
                   && EqualityComparer<T>.Default.Equals(_toState, other._toState);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<T>.Default.GetHashCode(_fromState);
                hashCode = (hashCode * 397) ^ _symbol.GetHashCode();
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(_toState);
                return hashCode;
            }
        }

        public virtual int CompareTo(Transition<T> t)
        {
            int fromCmp = _fromState.CompareTo(t._fromState);
            int symbolCmp = _symbol.CompareTo(t._symbol);
            int toCmp = _toState.CompareTo(t._toState);

            return (fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp));
        }

        public virtual T FromState => _fromState;

        public virtual T ToState => _toState;

        public virtual char Symbol => _symbol;

        public override string ToString()
        {
            return "(" + FromState + ", " + Symbol + ")" + "-->" + ToState;
        }
    }
}