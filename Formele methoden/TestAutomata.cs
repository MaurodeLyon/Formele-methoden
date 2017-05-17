namespace Week_1
{
    /// <summary>
    /// This file shows how to build up some example automata
    /// 
    /// @author (your name) 
    /// @version (a version number or a date)
    /// </summary>
    public class TestAutomata
    {
        public static Automata<string> ExampleSlide8Lesson2()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("q0", 'a', "q1"));
            m.AddTransition(new Transition<string>("q0", 'b', "q4"));

            m.AddTransition(new Transition<string>("q1", 'a', "q4"));
            m.AddTransition(new Transition<string>("q1", 'b', "q2"));

            m.AddTransition(new Transition<string>("q2", 'a', "q3"));
            m.AddTransition(new Transition<string>("q2", 'b', "q4"));

            m.AddTransition(new Transition<string>("q3", 'a', "q1"));
            m.AddTransition(new Transition<string>("q3", 'b', "q2"));

            // the error state, loops for a and b:
            m.AddTransition(new Transition<string>("q4", 'a'));
            m.AddTransition(new Transition<string>("q4", 'b'));

            // only on start state in a dfa:
            m.DefineAsStartState("q0");

            // two final states:
            m.DefineAsFinalState("q2");
            m.DefineAsFinalState("q3");

            return m;
        }

        public static Automata<string> ExampleSlide14Lesson2()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'a', "C"));
            m.AddTransition(new Transition<string>("A", 'b', "B"));
            m.AddTransition(new Transition<string>("A", 'b', "C"));

            m.AddTransition(new Transition<string>("B", 'b', "C"));
            m.AddTransition(new Transition<string>("B", "C"));

            m.AddTransition(new Transition<string>("C", 'a', "D"));
            m.AddTransition(new Transition<string>("C", 'a', "E"));
            m.AddTransition(new Transition<string>("C", 'b', "D"));

            m.AddTransition(new Transition<string>("D", 'a', "B"));
            m.AddTransition(new Transition<string>("D", 'a', "C"));

            m.AddTransition(new Transition<string>("E", 'a'));
            m.AddTransition(new Transition<string>("E", "D"));

            // only on start state in a dfa:
            m.DefineAsStartState("A");

            // two final states:
            m.DefineAsFinalState("C");
            m.DefineAsFinalState("E");

            return m;
        }

        public static Automata<string> Dfa()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'a', "B"));

            m.AddTransition(new Transition<string>("B", 'b', "C"));

            m.DefineAsStartState("A");

            m.DefineAsFinalState("C");

            return m;
        }

        public static Automata<string> Example1Week1()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'a', "B"));
            m.AddTransition(new Transition<string>("B", 'b', "C"));

            m.AddTransition(new Transition<string>("C", 'b', "D"));

            m.AddTransition(new Transition<string>("E", 'b', "F"));
            m.AddTransition(new Transition<string>("F", 'a', "G"));
            m.AddTransition(new Transition<string>("G", 'a', "C"));

            m.DefineAsStartState("A");
            m.DefineAsStartState("E");

            m.DefineAsFinalState("D");

            return m;
        }

        public static Automata<string> Example2Week1()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'b', "B"));
            m.AddTransition(new Transition<string>("B", 'a', "A"));

            m.DefineAsStartState("A");

            m.DefineAsFinalState("A");

            return m;
        }

        public static Automata<string> Example3Week1()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'b', "B"));
            m.AddTransition(new Transition<string>("B", 'a', "C"));
            m.AddTransition(new Transition<string>("C", 'a', "D"));
            m.AddTransition(new Transition<string>("D", 'b', "E"));

            m.DefineAsStartState("A");

            m.DefineAsFinalState("E");

            return m;
        }

        public static Automata<string> Example4Week1()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'a', "B"));
            m.AddTransition(new Transition<string>("B", 'b', "C"));
            m.AddTransition(new Transition<string>("C", 'b', "D"));
            m.AddTransition(new Transition<string>("D", 'a', "E"));
            m.AddTransition(new Transition<string>("E", 'a', "F"));
            m.AddTransition(new Transition<string>("F", 'b', "G"));

            m.DefineAsStartState("A");

            m.DefineAsFinalState("G");

            return m;
        }

        public static Automata<string> DfaTEST1()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'a', "B"));
            m.AddTransition(new Transition<string>("A", 'b', "C"));

            m.AddTransition(new Transition<string>("B", 'b', "C"));
            m.AddTransition(new Transition<string>("B", 'a', "A"));

            m.AddTransition(new Transition<string>("C", 'a', "C"));
            m.AddTransition(new Transition<string>("C", 'b', "C"));

            m.DefineAsStartState("A");
            //Toggle test
            // m.DefineAsStartState("B");


            m.DefineAsFinalState("C");

            return m;
        }

        public static Automata<string> DfaTEST2()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'a', "B"));
            m.AddTransition(new Transition<string>("A", 'b', "C"));
            //Toggle test
            //m.AddTransition(new Transition<string>("A", '$', "C"));

            m.AddTransition(new Transition<string>("B", 'b', "C"));
            m.AddTransition(new Transition<string>("B", 'a', "A"));

            m.AddTransition(new Transition<string>("C", 'a', "C"));
            m.AddTransition(new Transition<string>("C", 'b', "C"));

            m.DefineAsStartState("A");


            m.DefineAsFinalState("C");

            return m;
        }

        public static Automata<string> DfaTEST3()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.AddTransition(new Transition<string>("A", 'a', "B"));
            //Toggle test for missing symbol
            m.AddTransition(new Transition<string>("A", 'b', "C"));
            //Toggle test for multiple of the same symbol
            //m.AddTransition(new Transition<string>("A", 'b', "A"));


            m.AddTransition(new Transition<string>("B", 'b', "C"));
            m.AddTransition(new Transition<string>("B", 'a', "A"));

            m.AddTransition(new Transition<string>("C", 'a', "C"));
            m.AddTransition(new Transition<string>("C", 'b', "C"));

            m.DefineAsStartState("A");


            m.DefineAsFinalState("C");

            return m;
        }
    }
}