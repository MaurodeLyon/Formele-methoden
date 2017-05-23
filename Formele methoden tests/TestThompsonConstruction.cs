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

            Assert.AreEqual("(0, $)-->2", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, $)-->3", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(10, a)-->11", automaat.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(11, $)-->10", automaat.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(11, $)-->9", automaat.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(12, $)-->14", automaat.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(12, $)-->16", automaat.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(13, $)-->1", automaat.Transitions.ToList()[7].ToString());
            Assert.AreEqual("(13, $)-->12", automaat.Transitions.ToList()[8].ToString());
            Assert.AreEqual("(14, b)-->15", automaat.Transitions.ToList()[9].ToString());
            Assert.AreEqual("(15, $)-->13", automaat.Transitions.ToList()[10].ToString());
            Assert.AreEqual("(16, c)-->17", automaat.Transitions.ToList()[11].ToString());
            Assert.AreEqual("(17, $)-->13", automaat.Transitions.ToList()[12].ToString());
            Assert.AreEqual("(2, $)-->10", automaat.Transitions.ToList()[13].ToString());
            Assert.AreEqual("(3, $)-->5", automaat.Transitions.ToList()[14].ToString());
            Assert.AreEqual("(3, $)-->7", automaat.Transitions.ToList()[15].ToString());
            Assert.AreEqual("(4, $)-->2", automaat.Transitions.ToList()[16].ToString());
            Assert.AreEqual("(4, $)-->3", automaat.Transitions.ToList()[17].ToString());
            Assert.AreEqual("(5, d)-->6", automaat.Transitions.ToList()[18].ToString());
            Assert.AreEqual("(6, $)-->4", automaat.Transitions.ToList()[19].ToString());
            Assert.AreEqual("(7, e)-->8", automaat.Transitions.ToList()[20].ToString());
            Assert.AreEqual("(8, $)-->4", automaat.Transitions.ToList()[21].ToString());
            Assert.AreEqual("(9, $)-->12", automaat.Transitions.ToList()[22].ToString());
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
        public void RegExpAbDotCd()
        {
            RegExp regExp = new RegExp("ab").Dot(new RegExp("cd"));
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, a)-->3", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(2, c)-->4", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(3, b)-->2", automaat.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(4, d)-->1", automaat.Transitions.ToList()[3].ToString());
        }

        [TestMethod]
        public void RegExpAbDotCdPlus()
        {
            RegExp regExp = new RegExp("ab").Dot(new RegExp("cd").Plus());
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, a)-->3", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(2, $)-->4", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(3, b)-->2", automaat.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(4, c)-->6", automaat.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(5, $)-->1", automaat.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(5, $)-->4", automaat.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(6, d)-->5", automaat.Transitions.ToList()[6].ToString());
        }

        [TestMethod]
        public void RegExpAorBstar()
        {
            RegExp regExp = new RegExp("a").Or(new RegExp("b")).Star();
            Automata<string> automaat = ThompsonConstruction.ConvertRegExp(regExp);
            Assert.AreEqual("(0, $)-->1", automaat.Transitions.ToList()[0].ToString());
            Assert.AreEqual("(0, $)-->2", automaat.Transitions.ToList()[1].ToString());
            Assert.AreEqual("(2, $)-->4", automaat.Transitions.ToList()[2].ToString());
            Assert.AreEqual("(2, $)-->6", automaat.Transitions.ToList()[3].ToString());
            Assert.AreEqual("(3, $)-->1", automaat.Transitions.ToList()[4].ToString());
            Assert.AreEqual("(3, $)-->2", automaat.Transitions.ToList()[5].ToString());
            Assert.AreEqual("(4, a)-->5", automaat.Transitions.ToList()[6].ToString());
            Assert.AreEqual("(5, $)-->3", automaat.Transitions.ToList()[7].ToString());
            Assert.AreEqual("(6, b)-->7", automaat.Transitions.ToList()[8].ToString());
            Assert.AreEqual("(7, $)-->3", automaat.Transitions.ToList()[9].ToString());
        }
    }
}