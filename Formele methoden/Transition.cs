using System;
using System.Collections.Generic;

namespace Week_1
{
    public class Transition<T> : IComparable<Transition<T>> where T : IComparable
    {
        public const char Epsilon = '$';

        public T FromState { get; set; }
        public char Symbol { get; }
        public T ToState { get; set; }

        public Transition(T fromOrTo, char symbol) : this(fromOrTo, symbol, fromOrTo)
        {
        }

        public Transition(T from, T to) : this(from, Epsilon, to)
        {
        }

        public Transition(T from, char symbol, T to)
        {
            FromState = from;
            Symbol = symbol;
            ToState = to;
        }

        public override bool Equals(object other)
        {
            var transition = other as Transition<T>;
            if (transition != null)
            {
                return FromState.Equals(transition.FromState) &&
                       ToState.Equals(transition.ToState) &&
                       Symbol == (transition.Symbol);
            }
            return false;
        }
        
        public virtual int CompareTo(Transition<T> t)
        {
            int fromCmp = FromState.CompareTo(t.FromState);
            int symbolCmp = Symbol.CompareTo(t.Symbol);
            int toCmp = ToState.CompareTo(t.ToState);

            return (fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp));
        }

        public override string ToString()
        {
            return "(" + FromState + ", " + Symbol + ")" + "-->" + ToState;
        }
    }
}