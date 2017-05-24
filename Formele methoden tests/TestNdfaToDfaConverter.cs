using System.Collections.Generic;
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
            Assert.AreEqual("(1, b)-->2_3", dfa.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(1_3, a)-->2", dfa.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(1_3, b)-->2_3", dfa.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(2, a)-->2_4", dfa.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(2, b)-->F", dfa.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(2_3, a)-->2_4", dfa.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(2_3, b)-->F", dfa.Transitions.ToList()[7].ToString());
            Assert.AreEqual("(2_4, a)-->2_4", dfa.Transitions.ToList()[8].ToString());
            Assert.AreEqual("(2_4, b)-->1_3", dfa.Transitions.ToList()[9].ToString());
            Assert.AreEqual("(F, a)-->F", dfa.Transitions.ToList()[10].ToString());
            Assert.AreEqual("(F, b)-->F", dfa.Transitions.ToList()[11].ToString());

            Assert.AreEqual("1", dfa.FinalStates.ToList()[0]);
            Assert.AreEqual("1_3", dfa.FinalStates.ToList()[1]);
            Assert.AreEqual("2_4", dfa.FinalStates.ToList()[2]);

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
            Assert.AreEqual("(A_S, a)-->A_S", dfa.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(A_S, b)-->B", dfa.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(B, a)-->F", dfa.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(B, b)-->F", dfa.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(F, a)-->F", dfa.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(F, b)-->F", dfa.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(S, a)-->A_S", dfa.Transitions.ToList()[6].ToString());
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
        }
    }
}