using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week_1;

namespace Formele_methoden_tests
{
    [TestClass]
    public class TestRegExp
    {
        [TestMethod]
        public void RegExpIsA()
        {
            RegExp regExp = new RegExp("a");
            Assert.AreEqual("a", regExp.Terminals);
            Assert.IsNull(regExp.Left);
            Assert.IsNull(regExp.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExp.Operator);
        }

        [TestMethod]
        public void RegExpIsB()
        {
            RegExp regExp = new RegExp("b");
            Assert.AreEqual("b", regExp.Terminals);
            Assert.IsNull(regExp.Left);
            Assert.IsNull(regExp.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExp.Operator);
        }

        [TestMethod]
        public void RegExpIsBaa()
        {
            RegExp regExp = new RegExp("baa");
            Assert.AreEqual("baa", regExp.Terminals);
            Assert.IsNull(regExp.Left);
            Assert.IsNull(regExp.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExp.Operator);
        }

        [TestMethod]
        public void RegExpIsBb()
        {
            RegExp regExp = new RegExp("bb");
            Assert.AreEqual("bb", regExp.Terminals);
            Assert.IsNull(regExp.Left);
            Assert.IsNull(regExp.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExp.Operator);
        }

        [TestMethod]
        public void RegExpAorA()
        {
            RegExp regExpLeft = new RegExp("a");
            RegExp regExpRight = new RegExp("a");
            RegExp regExpOr = regExpLeft.Or(regExpRight);

            Assert.AreEqual("a", regExpLeft.Terminals);
            Assert.AreEqual("a", regExpRight.Terminals);
            Assert.AreEqual("", regExpOr.Terminals);

            Assert.AreEqual("a", regExpOr.Left.Terminals);
            Assert.AreEqual("a", regExpOr.Right.Terminals);

            Assert.AreEqual(RegExp.OperatorEnum.Or, regExpOr.Operator);
        }

        [TestMethod]
        public void RegExpAorB()
        {
            RegExp regExpLeft = new RegExp("a");
            RegExp regExpRight = new RegExp("b");
            RegExp regExpOr = regExpLeft.Or(regExpRight);

            Assert.AreEqual("a", regExpLeft.Terminals);
            Assert.AreEqual("b", regExpRight.Terminals);
            Assert.AreEqual("", regExpOr.Terminals);

            Assert.AreEqual("a", regExpOr.Left.Terminals);
            Assert.AreEqual("b", regExpOr.Right.Terminals);

            Assert.AreEqual(RegExp.OperatorEnum.Or, regExpOr.Operator);
        }

        [TestMethod]
        public void RegExpAbaOrBab()
        {
            RegExp regExpLeft = new RegExp("aba");
            RegExp regExpRight = new RegExp("bab");
            RegExp regExpOr = regExpLeft.Or(regExpRight);

            Assert.AreEqual("aba", regExpLeft.Terminals);
            Assert.AreEqual("bab", regExpRight.Terminals);
            Assert.AreEqual("", regExpOr.Terminals);

            Assert.AreEqual("aba", regExpOr.Left.Terminals);
            Assert.AreEqual("bab", regExpOr.Right.Terminals);

            Assert.AreEqual(RegExp.OperatorEnum.Or, regExpOr.Operator);
        }

        [TestMethod]
        public void RegExpStarA()
        {
            RegExp regExp = new RegExp("a");
            RegExp regExpStar = regExp.Star();

            Assert.IsNull(regExp.Left);
            Assert.IsNull(regExp.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExp.Operator);
            Assert.AreEqual("a", regExp.Terminals);

            Assert.AreSame(regExp, regExpStar.Left);
            Assert.IsNull(regExpStar.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Star, regExpStar.Operator);
            Assert.AreEqual("", regExpStar.Terminals);
        }

        [TestMethod]
        public void RegExpStarAa()
        {
            RegExp regExp = new RegExp("aa");
            RegExp regExpStar = regExp.Star();

            Assert.IsNull(regExp.Left);
            Assert.IsNull(regExp.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExp.Operator);
            Assert.AreEqual("aa", regExp.Terminals);

            Assert.AreSame(regExp, regExpStar.Left);
            Assert.IsNull(regExpStar.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Star, regExpStar.Operator);
            Assert.AreEqual("", regExpStar.Terminals);
        }

        [TestMethod]
        public void RegExpStarAaOrBb()
        {
            RegExp regExpLeft = new RegExp("aa");
            Assert.IsNull(regExpLeft.Left);
            Assert.IsNull(regExpLeft.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpLeft.Operator);
            Assert.AreEqual("aa", regExpLeft.Terminals);

            RegExp regExpRight = new RegExp("bb");
            Assert.IsNull(regExpRight.Left);
            Assert.IsNull(regExpRight.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpRight.Operator);
            Assert.AreEqual("bb", regExpRight.Terminals);

            RegExp regExpOr = regExpLeft.Or(regExpRight);
            Assert.AreSame(regExpLeft,regExpOr.Left);
            Assert.AreSame(regExpRight,regExpOr.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Or, regExpOr.Operator);
            Assert.AreEqual("", regExpOr.Terminals);

            RegExp regExpStar = regExpOr.Star();
            Assert.AreSame(regExpOr, regExpStar.Left);
            Assert.IsNull(regExpStar.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Star, regExpStar.Operator);
            Assert.AreEqual("", regExpStar.Terminals);
        }

        [TestMethod]
        public void RegExpPlusA()
        {
            RegExp regExp = new RegExp("a");
            Assert.IsNull(regExp.Left);
            Assert.IsNull(regExp.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExp.Operator);
            Assert.AreEqual("a", regExp.Terminals);

            RegExp regExpPlus = regExp.Plus();
            Assert.AreSame(regExp, regExpPlus.Left);
            Assert.IsNull(regExpPlus.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Plus, regExpPlus.Operator);
            Assert.AreEqual("", regExpPlus.Terminals);
        }

        [TestMethod]
        public void RegExpPlusAa()
        {
            RegExp regExp = new RegExp("aa");
            Assert.IsNull(regExp.Left);
            Assert.IsNull(regExp.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExp.Operator);
            Assert.AreEqual("aa", regExp.Terminals);

            RegExp regExpPlus = regExp.Plus();
            Assert.AreSame(regExp, regExpPlus.Left);
            Assert.IsNull(regExpPlus.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Plus, regExpPlus.Operator);
            Assert.AreEqual("", regExpPlus.Terminals);
        }

        [TestMethod]
        public void RegExpPlusAaOrBb()
        {
            RegExp regExpLeft = new RegExp("aa");
            Assert.IsNull(regExpLeft.Left);
            Assert.IsNull(regExpLeft.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpLeft.Operator);
            Assert.AreEqual("aa", regExpLeft.Terminals);

            RegExp regExpRight = new RegExp("bb");
            Assert.IsNull(regExpRight.Left);
            Assert.IsNull(regExpRight.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpRight.Operator);
            Assert.AreEqual("bb", regExpRight.Terminals);

            RegExp regExpOr = regExpLeft.Or(regExpRight);
            Assert.AreSame(regExpLeft, regExpOr.Left);
            Assert.AreSame(regExpRight, regExpOr.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Or, regExpOr.Operator);
            Assert.AreEqual("", regExpOr.Terminals);

            RegExp regExpPlus = regExpOr.Plus();
            Assert.AreSame(regExpOr, regExpPlus.Left);
            Assert.IsNull(regExpPlus.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Plus, regExpPlus.Operator);
            Assert.AreEqual("", regExpPlus.Terminals);
        }

        [TestMethod]
        public void RegExpAdotA()
        {
            RegExp regExpLeft = new RegExp("a");
            Assert.IsNull(regExpLeft.Left);
            Assert.IsNull(regExpLeft.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpLeft.Operator);
            Assert.AreEqual("a", regExpLeft.Terminals);

            RegExp regExpRight = new RegExp("a");
            Assert.IsNull(regExpRight.Left);
            Assert.IsNull(regExpRight.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpRight.Operator);
            Assert.AreEqual("a", regExpRight.Terminals);

            RegExp regExpDot = regExpLeft.Dot(regExpRight);
            Assert.AreEqual(regExpLeft, regExpDot.Left);
            Assert.AreEqual(regExpRight, regExpDot.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Dot, regExpDot.Operator);
            Assert.AreEqual("", regExpDot.Terminals);
        }

        [TestMethod]
        public void RegExpAaDotBb()
        {
            RegExp regExpLeft = new RegExp("aa");
            Assert.IsNull(regExpLeft.Left);
            Assert.IsNull(regExpLeft.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpLeft.Operator);
            Assert.AreEqual("aa", regExpLeft.Terminals);

            RegExp regExpRight = new RegExp("bb");
            Assert.IsNull(regExpRight.Left);
            Assert.IsNull(regExpRight.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpRight.Operator);
            Assert.AreEqual("bb", regExpRight.Terminals);

            RegExp regExpDot = regExpLeft.Dot(regExpRight);
            Assert.AreEqual(regExpLeft, regExpDot.Left);
            Assert.AreEqual(regExpRight, regExpDot.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Dot, regExpDot.Operator);
            Assert.AreEqual("", regExpDot.Terminals);
        }

        [TestMethod]
        public void RegExpAaOrBbDotAa()
        {
            RegExp regExpLeft = new RegExp("aa");
            Assert.IsNull(regExpLeft.Left);
            Assert.IsNull(regExpLeft.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpLeft.Operator);
            Assert.AreEqual("aa", regExpLeft.Terminals);

            RegExp regExpRight = new RegExp("bb");
            Assert.IsNull(regExpRight.Left);
            Assert.IsNull(regExpRight.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpRight.Operator);
            Assert.AreEqual("bb", regExpRight.Terminals);

            RegExp regExpOr = regExpLeft.Or(regExpRight);
            Assert.AreSame(regExpLeft, regExpOr.Left);
            Assert.AreSame(regExpRight, regExpOr.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Or, regExpOr.Operator);
            Assert.AreEqual("", regExpOr.Terminals);

            RegExp regExpDot = regExpOr.Dot(regExpLeft);
            Assert.AreSame(regExpOr, regExpDot.Left);
            Assert.AreSame(regExpLeft, regExpDot.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Dot, regExpDot.Operator);
            Assert.AreEqual("", regExpDot.Terminals);
        }

        [TestMethod]
        public void RegExpAaOrBbDotAaOrBb()
        {
            RegExp regExpLeft = new RegExp("aa");
            Assert.IsNull(regExpLeft.Left);
            Assert.IsNull(regExpLeft.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpLeft.Operator);
            Assert.AreEqual("aa", regExpLeft.Terminals);

            RegExp regExpRight = new RegExp("bb");
            Assert.IsNull(regExpRight.Left);
            Assert.IsNull(regExpRight.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpRight.Operator);
            Assert.AreEqual("bb", regExpRight.Terminals);

            RegExp regExpOr = regExpLeft.Or(regExpRight);
            Assert.AreSame(regExpLeft, regExpOr.Left);
            Assert.AreSame(regExpRight, regExpOr.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Or, regExpOr.Operator);
            Assert.AreEqual("", regExpOr.Terminals);

            RegExp regExpDot = regExpOr.Dot(regExpOr);
            Assert.AreSame(regExpOr, regExpDot.Left);
            Assert.AreSame(regExpOr, regExpDot.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Dot, regExpDot.Operator);
            Assert.AreEqual("", regExpDot.Terminals);
        }

        [TestMethod]
        public void RegExpAaOrBbDotAaOrBbStar()
        {
            RegExp regExpLeft = new RegExp("aa");
            Assert.IsNull(regExpLeft.Left);
            Assert.IsNull(regExpLeft.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpLeft.Operator);
            Assert.AreEqual("aa", regExpLeft.Terminals);

            RegExp regExpRight = new RegExp("bb");
            Assert.IsNull(regExpRight.Left);
            Assert.IsNull(regExpRight.Right);
            Assert.AreEqual(RegExp.OperatorEnum.One, regExpRight.Operator);
            Assert.AreEqual("bb", regExpRight.Terminals);

            RegExp regExpOr = regExpLeft.Or(regExpRight);
            Assert.AreSame(regExpLeft, regExpOr.Left);
            Assert.AreSame(regExpRight, regExpOr.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Or, regExpOr.Operator);
            Assert.AreEqual("", regExpOr.Terminals);

            RegExp regExpDot = regExpOr.Dot(regExpOr);
            Assert.AreSame(regExpOr, regExpDot.Left);
            Assert.AreSame(regExpOr, regExpDot.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Dot, regExpDot.Operator);
            Assert.AreEqual("", regExpDot.Terminals);

            RegExp regExpStar = regExpDot.Star();
            Assert.AreSame(regExpDot, regExpStar.Left);
            Assert.IsNull(regExpStar.Right);
            Assert.AreEqual(RegExp.OperatorEnum.Star, regExpStar.Operator);
            Assert.AreEqual("", regExpStar.Terminals);
        }

        [TestMethod]
        public void GetLanguagePlusTest()
        {
            RegExp regExp = new RegExp("a").Plus();
            SortedSet<string> language = regExp.GetLanguage(2);
            Assert.AreEqual(2,language.Count);
            Assert.AreEqual("a",language.ToList()[0]);
            Assert.AreEqual("aa",language.ToList()[1]);
        }

        [TestMethod]
        public void GetLanguageStarTest()
        {
            RegExp regExp = new RegExp("a").Star();
            SortedSet<string> language = regExp.GetLanguage(2);
            Assert.AreEqual(3,language.Count);
            Assert.AreEqual("",language.ToList()[0]);
            Assert.AreEqual("a",language.ToList()[1]);
            Assert.AreEqual("aa",language.ToList()[2]);
        }

        [TestMethod]
        public void GetLanguageOrTest()
        {
            RegExp regExp = new RegExp("a").Or(new RegExp("b"));
            SortedSet<string> language = regExp.GetLanguage(2);
            Assert.AreEqual(2,language.Count);
            Assert.AreEqual("a",language.ToList()[0]);
            Assert.AreEqual("b",language.ToList()[1]);
        }

        [TestMethod]
        public void GetLanguageDotTest()
        {
            RegExp regExp = new RegExp("a").Dot(new RegExp("b"));
            SortedSet<string> language = regExp.GetLanguage(2);
            Assert.AreEqual(1,language.Count);
            Assert.AreEqual("ab",language.ToList()[0]);
        }

        [TestMethod]
        public void GetLanguageEmptyTest()
        {
            RegExp regExp = new RegExp("a");
            SortedSet<string> language = regExp.GetLanguage(0);
            Assert.AreEqual(0,language.Count);
        }

        [TestMethod]
        public void ToStringTest()
        {
            RegExp regExp = new RegExp("d").Or(new RegExp("e")).Star().Dot(new RegExp("a").Plus().Dot(new RegExp("b").Or(new RegExp("c")).Plus()));
            Assert.AreEqual("((d|e)*.(a+.(b|c)+))", regExp.ToString());
        }
    }
}