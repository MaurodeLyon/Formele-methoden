using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week_1;

namespace Formele_methoden_tests
{
    [TestClass]
    public class TestThompsonConstruction
    {
        [TestMethod]
        public void RegExpAllOperatorsToAutomata()
        {
            RegExp regExp = new RegExp("d").Or(new RegExp("e")).Star().Dot(new RegExp("a").Plus().Dot(new RegExp("b").Or(new RegExp("c")).Plus()));
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            
            Assert.AreEqual("(0, $)-->1", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, $)-->4", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(1, a)-->2", automaat.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(10, $)-->8", automaat.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(2, $)-->3", automaat.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(3, $)-->6", automaat.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(3, $)-->9", automaat.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(4, b)-->5", automaat.Transitions.ToList()[7].ToString());
            Assert.AreEqual("(5, $)-->3", automaat.Transitions.ToList()[8].ToString());
            Assert.AreEqual("(6, c)-->7", automaat.Transitions.ToList()[9].ToString());
            Assert.AreEqual("(7, $)-->8", automaat.Transitions.ToList()[10].ToString());
            Assert.AreEqual("(9, d)-->10", automaat.Transitions.ToList()[11].ToString());
        }

        [TestMethod]
        public void RegExpPlus()
        {
            RegExp regExp = new RegExp("a").Plus();
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);

            Assert.AreEqual("(0, $)-->2", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(2, a)-->3", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(3, $)-->1", automaat.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(3, $)-->2", automaat.Transitions.ToList()[3].ToString());
        }

        [TestMethod]
        public void RegExpStar()
        {
            RegExp regExp = new RegExp("a").Star();
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, $)-->1", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, $)-->2", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(2, a)-->3", automaat.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(3, $)-->1", automaat.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(3, $)-->2", automaat.Transitions.ToList()[4].ToString());
        }

        [TestMethod]
        public void RegExpOneA()
        {
            RegExp regExp = new RegExp("a");
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, a)-->1", automaat.Transitions.ToList()[0].ToString());
        }

        [TestMethod]
        public void RegExpOneAa()
        {
            RegExp regExp = new RegExp("aa");
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, a)-->2", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(2, a)-->1", automaat.Transitions.ToList()[1].ToString());
        }

        [TestMethod]
        public void RegExpOneAaa()
        {
            RegExp regExp = new RegExp("aaa");
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, a)-->2", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(2, a)-->3", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(3, a)-->1", automaat.Transitions.ToList()[2].ToString());
        }

        [TestMethod]
        public void RegExpOr()
        {
            RegExp regExp = new RegExp("a").Or(new RegExp("b"));
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, $)-->2", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, $)-->4", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(2, a)-->3", automaat.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(3, $)-->1", automaat.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(4, b)-->5", automaat.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(5, $)-->1", automaat.Transitions.ToList()[5].ToString());
        }

        [TestMethod]
        public void RegExpDot()
        {
            RegExp regExp = new RegExp("a").Dot(new RegExp("b"));
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, a)-->2", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(2, b)-->1", automaat.Transitions.ToList()[1].ToString());
        }

        [TestMethod]
        public void RegExpAorBplusDotcStar()
        {
            RegExp regExp = new RegExp("a").Dot(new RegExp("b"));
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
        }

    }
}