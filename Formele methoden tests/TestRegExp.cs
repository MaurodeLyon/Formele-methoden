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
    }
}