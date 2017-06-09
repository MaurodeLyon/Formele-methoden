using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week_1;

namespace Formele_methoden_tests
{
    [TestClass]
    public class TestNdfaToDfaConverter
    {
        [TestMethod]
        public void TestConvertAutomaat1()
        {
            char[] alphabet = {'a', 'b'};
            Automaat<string> ndfa = new Automaat<string>(alphabet);
            ndfa.AddTransition(new Transition<string>("1", 'a', "2"));
            ndfa.AddTransition(new Transition<string>("1", 'b', "2"));
            ndfa.AddTransition(new Transition<string>("1", 'b', "3"));

            ndfa.AddTransition(new Transition<string>("2", 'a', "4"));
            ndfa.AddTransition(new Transition<string>("2", 'a', "2"));

            ndfa.AddTransition(new Transition<string>("4", 'b', "3"));
            ndfa.AddTransition(new Transition<string>("4", 'b', "1"));

            ndfa.DefineAsStartState("1");
            ndfa.DefineAsFinalState("1");
            ndfa.DefineAsFinalState("4");

            Automaat<string> dfa = NdfaToDfaConverter.Convert(ndfa);
            Assert.AreEqual("(1, a)-->2", dfa.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(1, b)-->23", dfa.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(13, a)-->2", dfa.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(13, b)-->23", dfa.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(2, a)-->24", dfa.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(2, b)-->F", dfa.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(23, a)-->24", dfa.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(23, b)-->F", dfa.Transitions.ToList()[7].ToString());
            Assert.AreEqual("(24, a)-->24", dfa.Transitions.ToList()[8].ToString());
            Assert.AreEqual("(24, b)-->13", dfa.Transitions.ToList()[9].ToString());
            Assert.AreEqual("(F, a)-->F", dfa.Transitions.ToList()[10].ToString());
            Assert.AreEqual("(F, b)-->F", dfa.Transitions.ToList()[11].ToString());

            Assert.AreEqual("1", dfa.FinalStates.ToList()[0]);
            Assert.AreEqual("13", dfa.FinalStates.ToList()[1]);
            Assert.AreEqual("24", dfa.FinalStates.ToList()[2]);

            Assert.AreEqual("1", dfa.StartStates.ToList()[0]);
        }

        [TestMethod]
        public void TestConvertAutomaat2()
        {
            Automaat<string> ndfa = new Automaat<string>(new[] {'a', 'b'});
            ndfa.AddTransition(new Transition<string>("S", 'a', "A"));
            ndfa.AddTransition(new Transition<string>("S", 'a', "S"));

            ndfa.AddTransition(new Transition<string>("A", 'b', "B"));

            ndfa.DefineAsStartState("S");
            ndfa.DefineAsFinalState("B");

            Automaat<string> dfa = NdfaToDfaConverter.Convert(ndfa);
            Assert.AreEqual("(AS, a)-->AS", dfa.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(AS, b)-->B", dfa.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(B, a)-->F", dfa.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(B, b)-->F", dfa.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(F, a)-->F", dfa.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(F, b)-->F", dfa.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(S, a)-->AS", dfa.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(S, b)-->F", dfa.Transitions.ToList()[7].ToString());

            Assert.AreEqual("B", dfa.FinalStates.ToList()[0]);

            Assert.AreEqual("S", dfa.StartStates.ToList()[0]);
        }

        [TestMethod]
        public void TestConvertAutomaat3()
        {
            Automaat<string> ndfa = new Automaat<string>(new[] {'a', 'b'});
            ndfa.AddTransition(new Transition<string>("0", 'a', "0"));
            ndfa.AddTransition(new Transition<string>("0", 'a', "1"));
            ndfa.AddTransition(new Transition<string>("0", 'b', "1"));

            ndfa.AddTransition(new Transition<string>("1", 'a', "2"));
            ndfa.AddTransition(new Transition<string>("1", 'b', "2"));

            ndfa.AddTransition(new Transition<string>("2", 'a', "0"));
            ndfa.AddTransition(new Transition<string>("2", 'a', "2"));

            ndfa.DefineAsStartState("0");
            ndfa.DefineAsFinalState("2");

            Automaat<string> dfa = NdfaToDfaConverter.Convert(ndfa);
            Assert.AreEqual("(0, a)-->01", dfa.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, b)-->1", dfa.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(01, a)-->012", dfa.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(01, b)-->12", dfa.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(012, a)-->012", dfa.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(012, b)-->12", dfa.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(02, a)-->012", dfa.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(02, b)-->1", dfa.Transitions.ToList()[7].ToString());
            Assert.AreEqual("(1, a)-->2", dfa.Transitions.ToList()[8].ToString());
            Assert.AreEqual("(1, b)-->2", dfa.Transitions.ToList()[9].ToString());
            Assert.AreEqual("(12, a)-->02", dfa.Transitions.ToList()[10].ToString());
            Assert.AreEqual("(12, b)-->2", dfa.Transitions.ToList()[11].ToString());
            Assert.AreEqual("(2, a)-->02", dfa.Transitions.ToList()[12].ToString());
            Assert.AreEqual("(2, b)-->F", dfa.Transitions.ToList()[13].ToString());
            Assert.AreEqual("(F, a)-->F", dfa.Transitions.ToList()[14].ToString());
            Assert.AreEqual("(F, b)-->F", dfa.Transitions.ToList()[15].ToString());

            Assert.AreEqual("012", dfa.FinalStates.ToList()[0]);
            Assert.AreEqual("02", dfa.FinalStates.ToList()[1]);
            Assert.AreEqual("12", dfa.FinalStates.ToList()[2]);
            Assert.AreEqual("2", dfa.FinalStates.ToList()[3]);

            Assert.AreEqual("0", dfa.StartStates.ToList()[0]);
        }

        [TestMethod]
        public void TestReverseTransition()
        {
            Automaat<string> automaat = new Automaat<string>();
            automaat.AddTransition(new Transition<string>("0", 'a', "1"));
            automaat.AddTransition(new Transition<string>("0", 'b', "1"));
            automaat.AddTransition(new Transition<string>("1", 'b', "0"));
            automaat.AddTransition(new Transition<string>("1", 'a', "1"));
            Automaat<string> reverse = NdfaToDfaConverter.Reverse(automaat);
            Assert.AreEqual("(0, b)-->1", reverse.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(1, a)-->0", reverse.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(1, a)-->1", reverse.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(1, b)-->0", reverse.Transitions.ToList()[3].ToString());
        }

        [TestMethod]
        public void TestReverseStartStop()
        {
            Automaat<string> automaat = new Automaat<string>();
            automaat.DefineAsStartState("0");
            automaat.DefineAsFinalState("1");
            Automaat<string> reverse = NdfaToDfaConverter.Reverse(automaat);
            Assert.AreEqual(automaat.StartStates, reverse.FinalStates);
            Assert.AreEqual(automaat.FinalStates, reverse.StartStates);
        }
    }
}