using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week_1;

namespace Formele_methoden_tests
{
    [TestClass]
    public class TestHopcroftAlgorithm
    {
        [TestMethod]
        public void DfaTest()
        {
            char[] alphabet = { 'a', 'b' };
            Automaat<string> dfaToHopcroft = new Automaat<string>(alphabet);

            dfaToHopcroft.AddTransition(new Transition<string>("1", 'a', "2"));
            dfaToHopcroft.AddTransition(new Transition<string>("1", 'b', "3"));

            dfaToHopcroft.AddTransition(new Transition<string>("2", 'a', "4"));
            dfaToHopcroft.AddTransition(new Transition<string>("2", 'b', "5"));

            dfaToHopcroft.AddTransition(new Transition<string>("3", 'a', "6"));
            dfaToHopcroft.AddTransition(new Transition<string>("3", 'b', "7"));

            dfaToHopcroft.AddTransition(new Transition<string>("4", 'a', "4"));
            dfaToHopcroft.AddTransition(new Transition<string>("4", 'b', "4"));

            dfaToHopcroft.AddTransition(new Transition<string>("5", 'a', "2"));
            dfaToHopcroft.AddTransition(new Transition<string>("5", 'b', "8"));

            dfaToHopcroft.AddTransition(new Transition<string>("6", 'a', "4"));
            dfaToHopcroft.AddTransition(new Transition<string>("6", 'b', "5"));

            dfaToHopcroft.AddTransition(new Transition<string>("7", 'a', "6"));
            dfaToHopcroft.AddTransition(new Transition<string>("7", 'b', "7"));

            dfaToHopcroft.AddTransition(new Transition<string>("8", 'a', "6"));
            dfaToHopcroft.AddTransition(new Transition<string>("8", 'b', "9"));

            dfaToHopcroft.AddTransition(new Transition<string>("9", 'a', "6"));
            dfaToHopcroft.AddTransition(new Transition<string>("9", 'b', "10"));

            dfaToHopcroft.AddTransition(new Transition<string>("10", 'a', "6"));
            dfaToHopcroft.AddTransition(new Transition<string>("10", 'b', "10"));

            dfaToHopcroft.DefineAsStartState("1");

            dfaToHopcroft.DefineAsFinalState("3");
            dfaToHopcroft.DefineAsFinalState("5");
            dfaToHopcroft.DefineAsFinalState("7");
            dfaToHopcroft.DefineAsFinalState("9");
            dfaToHopcroft.DefineAsFinalState("10");

            Automaat<string> hopcroftedDfa = HopcroftAlgorithm.MinimizeDfa(dfaToHopcroft);
        }

    }
}