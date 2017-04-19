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
            char?[] alphabet = {'a', 'b'};
            Automata<string> m = new Automata<string>(alphabet);

            m.addTransition(new Transition<string>("q0", 'a', "q1"));
            m.addTransition(new Transition<string>("q0", 'b', "q4"));

            m.addTransition(new Transition<string>("q1", 'a', "q4"));
            m.addTransition(new Transition<string>("q1", 'b', "q2"));

            m.addTransition(new Transition<string>("q2", 'a', "q3"));
            m.addTransition(new Transition<string>("q2", 'b', "q4"));

            m.addTransition(new Transition<string>("q3", 'a', "q1"));
            m.addTransition(new Transition<string>("q3", 'b', "q2"));

            // the error state, loops for a and b:
            m.addTransition(new Transition<string>("q4", 'a'));
            m.addTransition(new Transition<string>("q4", 'b'));

            // only on start state in a dfa:
            m.defineAsStartState("q0");

            // two final states:
            m.defineAsFinalState("q2");
            m.defineAsFinalState("q3");

            return m;
        }

        public static Automata<string> ExampleSlide14Lesson2()
        {
            char?[] alphabet = { 'a', 'b' };
            Automata<string> m = new Automata<string>(alphabet);

            m.addTransition(new Transition<string>("A", 'a', "C"));
            m.addTransition(new Transition<string>("A", 'b', "B"));
            m.addTransition(new Transition<string>("A", 'b', "C"));

            m.addTransition(new Transition<string>("B", 'b', "C"));
            m.addTransition(new Transition<string>("B", "C"));

            m.addTransition(new Transition<string>("C", 'a', "D"));
            m.addTransition(new Transition<string>("C", 'a', "E"));
            m.addTransition(new Transition<string>("C", 'b', "D"));

            m.addTransition(new Transition<string>("D", 'a', "B"));
            m.addTransition(new Transition<string>("D", 'a', "C"));

            m.addTransition(new Transition<string>("E", 'a'));
            m.addTransition(new Transition<string>("E", "D"));

            // only on start state in a dfa:
            m.defineAsStartState("A");

            // two final states:
            m.defineAsFinalState("C");
            m.defineAsFinalState("E");

            return m;
        }
        public static Automata<string> Dfa()
        {
            char?[] alphabet = { 'a', 'b' };
            Automata<string> m = new Automata<string>(alphabet);

            m.addTransition(new Transition<string>("A", 'a', "B"));

            m.addTransition(new Transition<string>("B", 'b', "C"));

            m.defineAsStartState("A");

            m.defineAsFinalState("C");

            return m;
        }
        public static Automata<string> Example1Week1()
        {
            char?[] alphabet = { 'a', 'b' };
            Automata<string> m = new Automata<string>(alphabet);

            m.addTransition(new Transition<string>("A", 'a', "B"));
            m.addTransition(new Transition<string>("B", 'b', "C"));

            m.addTransition(new Transition<string>("C", 'b', "D"));

            m.addTransition(new Transition<string>("E", 'b', "F"));
            m.addTransition(new Transition<string>("F", 'a', "G"));
            m.addTransition(new Transition<string>("G", 'a', "C"));

            m.defineAsStartState("A");
            m.defineAsStartState("E");

            m.defineAsFinalState("D");

            return m;
        }
        public static Automata<string> Example2Week1()
        {
            char?[] alphabet = { 'a', 'b' };
            Automata<string> m = new Automata<string>(alphabet);

            m.addTransition(new Transition<string>("A", 'b', "B"));
            m.addTransition(new Transition<string>("B", 'a', "A"));

            m.defineAsStartState("A");
            
            m.defineAsFinalState("A");

            return m;
        }
        public static Automata<string> Example3Week1()
        {
            char?[] alphabet = { 'a', 'b' };
            Automata<string> m = new Automata<string>(alphabet);

            m.addTransition(new Transition<string>("A", 'b', "B"));
            m.addTransition(new Transition<string>("B", 'a', "C"));
            m.addTransition(new Transition<string>("C", 'a', "D"));
            m.addTransition(new Transition<string>("D", 'b', "E"));

            m.defineAsStartState("A");
            
            m.defineAsFinalState("E");

            return m;
        }
    }
}