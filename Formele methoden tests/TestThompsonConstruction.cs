using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week_1;

namespace Formele_methoden_tests
{
    [TestClass]
    public class TestThompsonConstruction
    {
        [TestMethod]
        public void RegExpToAutomata()
        {
            RegExp reg = new RegExp("a");
            reg = reg.Or(new RegExp("b"));
            RegExp reg2 = new RegExp("c");
            reg2 = reg2.Or(new RegExp("d"));
            RegExp reg3 = reg.Dot(reg2);

            Automata<string> regExpex = ThompsonConstruction.ConvertRegExpToAutomata(reg3);
        }

        [TestMethod]
        public void RegExpAaOrbbToAutomata()
        {
            RegExp regExp = new RegExp("aa").Or(new RegExp("bb"));
            Automata<string> automata = ThompsonConstruction.ConvertRegExpToAutomata(regExp);

            Assert.AreEqual(1, automata.FinalStates.Count);
            Assert.AreEqual(1, automata.StartStates.Count);
            Assert.AreEqual(8, automata.States.Count);
            Assert.AreEqual(2, automata.Symbols.Count);
            Assert.AreEqual(8, automata.Transitions.Count);

            Assert.AreEqual("4", automata.FinalStates.ToList()[0]);
            Assert.AreEqual("0", automata.StartStates.ToList()[0]);

            Assert.AreEqual('a', automata.Symbols.ToList()[0]);
            Assert.AreEqual('b', automata.Symbols.ToList()[1]);

            Assert.AreEqual("(0, $)-->1", automata.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, $)-->5", automata.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(1, a)-->2", automata.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(2, a)-->3", automata.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(3, $)-->4", automata.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(5, b)-->6", automata.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(6, b)-->7", automata.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(7, $)-->4", automata.Transitions.ToList()[7].ToString());
        }

        [TestMethod]
        public void RegExpaOrbDotcOrdToAutomata()
        {
            RegExp regExp = new RegExp("a").Or(new RegExp("b")).Dot(new RegExp("c").Or(new RegExp("d")));
            Automata<string> automata = ThompsonConstruction.ConvertRegExpToAutomata(regExp);

            Assert.AreEqual(1, automata.FinalStates.Count);
            Assert.AreEqual(1, automata.StartStates.Count);
            Assert.AreEqual(11, automata.States.Count);
            Assert.AreEqual(4, automata.Symbols.Count);
            Assert.AreEqual(12, automata.Transitions.Count);

            Assert.AreEqual("8", automata.FinalStates.ToList()[0]);
            Assert.AreEqual("0", automata.StartStates.ToList()[0]);

            Assert.AreEqual('a', automata.Symbols.ToList()[0]);
            Assert.AreEqual('b', automata.Symbols.ToList()[1]);
            Assert.AreEqual('c', automata.Symbols.ToList()[2]);
            Assert.AreEqual('d', automata.Symbols.ToList()[3]);

            Assert.AreEqual("(0, $)-->1", automata.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, $)-->4", automata.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(1, a)-->2", automata.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(10, $)-->8", automata.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(2, $)-->3", automata.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(3, $)-->6", automata.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(3, $)-->9", automata.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(4, b)-->5", automata.Transitions.ToList()[7].ToString());
            Assert.AreEqual("(5, $)-->3", automata.Transitions.ToList()[8].ToString());
            Assert.AreEqual("(6, c)-->7", automata.Transitions.ToList()[9].ToString());
            Assert.AreEqual("(7, $)-->8", automata.Transitions.ToList()[10].ToString());
            Assert.AreEqual("(9, d)-->10", automata.Transitions.ToList()[11].ToString());
        }

        [TestMethod]
        public void RegExpAllOperatorsToAutomata()
        {
            RegExp regExp = new RegExp("d").Or(new RegExp("e")).Star().Dot(new RegExp("a").Plus().Dot(new RegExp("b").Or(new RegExp("c")).Plus()));
            Automata<string> automata = ThompsonConstruction.ConvertRegExpToAutomata(regExp);

            Assert.AreEqual(1, automata.FinalStates.Count);
            Assert.AreEqual(1, automata.StartStates.Count);
            Assert.AreEqual(11, automata.States.Count);
            Assert.AreEqual(4, automata.Symbols.Count);
            Assert.AreEqual(12, automata.Transitions.Count);

            Assert.AreEqual("8", automata.FinalStates.ToList()[0]);
            Assert.AreEqual("0", automata.StartStates.ToList()[0]);

            Assert.AreEqual('a', automata.Symbols.ToList()[0]);
            Assert.AreEqual('b', automata.Symbols.ToList()[1]);
            Assert.AreEqual('c', automata.Symbols.ToList()[2]);
            Assert.AreEqual('d', automata.Symbols.ToList()[3]);

            Assert.AreEqual("(0, $)-->1", automata.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, $)-->4", automata.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(1, a)-->2", automata.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(10, $)-->8", automata.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(2, $)-->3", automata.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(3, $)-->6", automata.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(3, $)-->9", automata.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(4, b)-->5", automata.Transitions.ToList()[7].ToString());
            Assert.AreEqual("(5, $)-->3", automata.Transitions.ToList()[8].ToString());
            Assert.AreEqual("(6, c)-->7", automata.Transitions.ToList()[9].ToString());
            Assert.AreEqual("(7, $)-->8", automata.Transitions.ToList()[10].ToString());
            Assert.AreEqual("(9, d)-->10", automata.Transitions.ToList()[11].ToString());
        }
    }
}