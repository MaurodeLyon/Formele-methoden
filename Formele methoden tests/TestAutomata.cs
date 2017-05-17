using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week_1;

namespace Formele_methoden_tests
{
    [TestClass]
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

        [TestMethod]
        public void Test()
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

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SimpleNdfa()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> ndfa = new Automata<string>(alphabet);
            ndfa.AddTransition(new Transition<string>("q0", 'a', "q1"));
            ndfa.DefineAsStartState("q0");
            ndfa.DefineAsFinalState("q1");

            Assert.AreEqual(1, ndfa.FinalStates.Count);
            Assert.AreEqual(1, ndfa.StartStates.Count);
            Assert.AreEqual(2, ndfa.States.Count);
            Assert.AreEqual(2, ndfa.Symbols.Count);
            Assert.AreEqual(1, ndfa.Transitions.Count);

            Assert.AreEqual("q0",ndfa.StartStates.ToList()[0]);
            Assert.AreEqual("q1",ndfa.FinalStates.ToList()[0]);
            Assert.IsFalse(ndfa.IsDfa());
        }

        [TestMethod]
        public void SimpleDfa()
        {
            char[] alphabet = {'a', 'b'};
            Automata<string> ndfa = new Automata<string>(alphabet);
            ndfa.AddTransition(new Transition<string>("q0", 'a', "q1"));
            ndfa.AddTransition(new Transition<string>("q0", 'b', "q1"));
            ndfa.AddTransition(new Transition<string>("q1", 'a', "q1"));
            ndfa.AddTransition(new Transition<string>("q1", 'b', "q1"));

            ndfa.DefineAsStartState("q0");
            ndfa.DefineAsFinalState("q1");

            Assert.AreEqual(1, ndfa.FinalStates.Count);
            Assert.AreEqual(1, ndfa.StartStates.Count);
            Assert.AreEqual(2, ndfa.States.Count);
            Assert.AreEqual(2, ndfa.Symbols.Count);
            Assert.AreEqual(4, ndfa.Transitions.Count);

            Assert.AreEqual("q0",ndfa.StartStates.ToList()[0]);
            Assert.AreEqual("q1",ndfa.FinalStates.ToList()[0]);

            Assert.IsTrue(ndfa.IsDfa());
        }
    }
}