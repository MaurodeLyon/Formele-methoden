using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week_1;

namespace Formele_methoden_tests
{
    [TestClass]
    public class TestAutomata
    {
        [TestMethod]
        public void DfaTest1()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("0", 'a', "1"));
            dfa.AddTransition(new Transition<string>("0", 'b', "4"));

            dfa.AddTransition(new Transition<string>("1", 'b', "2"));
            dfa.AddTransition(new Transition<string>("1", 'a', "4"));

            dfa.AddTransition(new Transition<string>("2", 'a', "3"));
            dfa.AddTransition(new Transition<string>("2", 'b', "4"));

            dfa.AddTransition(new Transition<string>("3", 'a', "1"));
            dfa.AddTransition(new Transition<string>("3", 'b', "2"));

            dfa.AddTransition(new Transition<string>("4", 'a', "4"));
            dfa.AddTransition(new Transition<string>("4", 'b', "4"));

            dfa.DefineAsStartState("0");
            dfa.DefineAsFinalState("0");
            dfa.DefineAsFinalState("2");

            Assert.IsTrue(dfa.IsDfa());
        }

        [TestMethod]
        public void DfaTest2()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("0", 'a', "0"));
            dfa.AddTransition(new Transition<string>("0", 'b', "1"));

            dfa.AddTransition(new Transition<string>("1", 'a', "2"));
            dfa.AddTransition(new Transition<string>("1", 'b', "3"));

            dfa.AddTransition(new Transition<string>("2", 'a', "2"));
            dfa.AddTransition(new Transition<string>("2", 'b', "1"));

            dfa.AddTransition(new Transition<string>("3", 'a', "0"));
            dfa.AddTransition(new Transition<string>("3", 'b', "3"));

            dfa.DefineAsStartState("0");
            dfa.DefineAsFinalState("2");
            dfa.DefineAsFinalState("3");

            Assert.IsTrue(dfa.IsDfa());
        }

        [TestMethod]
        public void DfaTest3()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("0", 'a', "1"));
            dfa.AddTransition(new Transition<string>("0", 'b', "4"));

            dfa.AddTransition(new Transition<string>("1", 'b', "4"));
            dfa.AddTransition(new Transition<string>("1", 'a', "2"));

            dfa.AddTransition(new Transition<string>("2", 'a', "0"));
            dfa.AddTransition(new Transition<string>("2", 'b', "3"));

            dfa.AddTransition(new Transition<string>("3", 'a', "1"));
            dfa.AddTransition(new Transition<string>("3", 'b', "4"));

            dfa.AddTransition(new Transition<string>("4", 'a', "4"));
            dfa.AddTransition(new Transition<string>("4", 'b', "4"));

            dfa.DefineAsStartState("0");
            dfa.DefineAsFinalState("3");

            Assert.IsTrue(dfa.IsDfa());
        }

        [TestMethod]
        public void NdfaTest1()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("0", 'a', "1"));
            dfa.AddTransition(new Transition<string>("0", 'a', "0"));
            dfa.AddTransition(new Transition<string>("0", 'b', "0"));
            dfa.AddTransition(new Transition<string>("0", 'b', "3"));

            dfa.AddTransition(new Transition<string>("1", 'b', "2"));

            dfa.AddTransition(new Transition<string>("2", 'b', "3"));

            dfa.AddTransition(new Transition<string>("3", 'a', "4"));

            dfa.AddTransition(new Transition<string>("4", 'a', "4"));
            dfa.AddTransition(new Transition<string>("4", 'b', "4"));

            dfa.DefineAsStartState("0");
            dfa.DefineAsFinalState("4");

            Assert.IsFalse(dfa.IsDfa());
        }

        [TestMethod]
        public void NdfaTest2()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("1", 'b', "0"));
            dfa.AddTransition(new Transition<string>("0", 'a', "0"));

            dfa.AddTransition(new Transition<string>("0", 'a', "1"));
            dfa.AddTransition(new Transition<string>("2", 'b', "1"));

            dfa.AddTransition(new Transition<string>("0", 'a', "2"));
            dfa.AddTransition(new Transition<string>("2", 'b', "2"));

            dfa.DefineAsStartState("2");
            dfa.DefineAsFinalState("0");

            Assert.IsFalse(dfa.IsDfa());
        }

        [TestMethod]
        public void NdfaTest3()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("0", 'a', "2"));
            dfa.AddTransition(new Transition<string>("0", 'b', "2"));
            dfa.AddTransition(new Transition<string>("0", 'b', "1"));

            dfa.AddTransition(new Transition<string>("1", 'b', "2"));
            dfa.AddTransition(new Transition<string>("1", '$', "2"));

            dfa.AddTransition(new Transition<string>("2", 'a', "3"));
            dfa.AddTransition(new Transition<string>("2", 'b', "3"));
            dfa.AddTransition(new Transition<string>("2", 'b', "4"));

            dfa.AddTransition(new Transition<string>("3", 'a', "1"));
            dfa.AddTransition(new Transition<string>("3", 'a', "2"));

            dfa.AddTransition(new Transition<string>("4", '$', "3"));
            dfa.AddTransition(new Transition<string>("4", 'b', "4"));

            dfa.DefineAsStartState("0");
            dfa.DefineAsFinalState("4");

            Assert.IsFalse(dfa.IsDfa());
        }

        [TestMethod]
        public void AcceptTestCorrectString()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("0", 'a', "1"));
            dfa.AddTransition(new Transition<string>("0", 'b', "4"));

            dfa.AddTransition(new Transition<string>("1", 'b', "2"));
            dfa.AddTransition(new Transition<string>("1", 'a', "4"));

            dfa.AddTransition(new Transition<string>("2", 'a', "3"));
            dfa.AddTransition(new Transition<string>("2", 'b', "4"));

            dfa.AddTransition(new Transition<string>("3", 'a', "1"));
            dfa.AddTransition(new Transition<string>("3", 'b', "2"));

            dfa.AddTransition(new Transition<string>("4", 'a', "4"));
            dfa.AddTransition(new Transition<string>("4", 'b', "4"));

            dfa.DefineAsStartState("0");
            dfa.DefineAsFinalState("0");
            dfa.DefineAsFinalState("2");

            Assert.IsTrue(dfa.Accept("ab"));
        }

        [TestMethod]
        public void AcceptTestInCorrectString()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("0", 'a', "1"));
            dfa.AddTransition(new Transition<string>("0", 'b', "4"));

            dfa.AddTransition(new Transition<string>("1", 'b', "2"));
            dfa.AddTransition(new Transition<string>("1", 'a', "4"));

            dfa.AddTransition(new Transition<string>("2", 'a', "3"));
            dfa.AddTransition(new Transition<string>("2", 'b', "4"));

            dfa.AddTransition(new Transition<string>("3", 'a', "1"));
            dfa.AddTransition(new Transition<string>("3", 'b', "2"));

            dfa.AddTransition(new Transition<string>("4", 'a', "4"));
            dfa.AddTransition(new Transition<string>("4", 'b', "4"));

            dfa.DefineAsStartState("0");
            dfa.DefineAsFinalState("0");
            dfa.DefineAsFinalState("2");

            Assert.IsFalse(dfa.Accept("cdfe"));
        }

        [TestMethod]
        public void AcceptTestInCorrectFinalState()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> dfa = new Automaat<string>(alphabet);
            dfa.AddTransition(new Transition<string>("0", 'a', "1"));
            dfa.AddTransition(new Transition<string>("0", 'b', "4"));

            dfa.AddTransition(new Transition<string>("1", 'b', "2"));
            dfa.AddTransition(new Transition<string>("1", 'a', "4"));

            dfa.AddTransition(new Transition<string>("2", 'a', "3"));
            dfa.AddTransition(new Transition<string>("2", 'b', "4"));

            dfa.AddTransition(new Transition<string>("3", 'a', "1"));
            dfa.AddTransition(new Transition<string>("3", 'b', "2"));

            dfa.AddTransition(new Transition<string>("4", 'a', "4"));
            dfa.AddTransition(new Transition<string>("4", 'b', "4"));

            dfa.DefineAsStartState("0");
            dfa.DefineAsFinalState("0");
            dfa.DefineAsFinalState("2");

            Assert.IsFalse(dfa.Accept("aba"));
        }

        [TestMethod]
        public void AcceptEpsilonTest()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> epsilonNdfa = new Automaat<string>(alphabet);
            epsilonNdfa.AddTransition(new Transition<string>("0", 'a', "1"));
            epsilonNdfa.AddTransition(new Transition<string>("1", '$', "2"));
            epsilonNdfa.AddTransition(new Transition<string>("2", 'b', "3"));
            epsilonNdfa.AddTransition(new Transition<string>("3", '$', "4"));
            epsilonNdfa.AddTransition(new Transition<string>("4", '$', "5"));
            epsilonNdfa.AddTransition(new Transition<string>("5", 'c', "6"));

            epsilonNdfa.DefineAsStartState("0");
            epsilonNdfa.DefineAsFinalState("6");

            Assert.IsTrue(epsilonNdfa.Accept("abc"));
        }

        [TestMethod]
        public void GeefTaalTotLengte()
        {
            Automaat<string> automaat = new Automaat<string>(new[] {'a', 'b'});
            automaat.AddTransition(new Transition<string>("0", 'a', "1"));
            automaat.AddTransition(new Transition<string>("0", 'b', "0"));

            automaat.AddTransition(new Transition<string>("1", 'a', "1"));
            automaat.AddTransition(new Transition<string>("1", 'b', "0"));

            automaat.DefineAsStartState("0");
            automaat.DefineAsFinalState("1");

            Assert.AreEqual(4, automaat.GeefTaalTotLengte(2).Count);
        }
    }
}