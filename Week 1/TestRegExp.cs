using System;
using System.Collections.Generic;

namespace Week_1
{
    class TestRegExp
    {
        private RegExp expr1, expr2, expr3, expr4, expr5, a, b, all;

        public TestRegExp()
        {
            a = new RegExp("a");
            b = new RegExp("b");

            // expr1: "baa"
            expr1 = new RegExp("baa");
            // expr2: "bb"
            expr2 = new RegExp("bb");
            // expr3: "baa | baa"
            expr3 = expr1.Or(expr2);

            // all: "(a|b)*"
            all = (a.Or(b)).Star();

            // expr4: "(baa | baa)+"
            expr4 = expr3.Plus();
            // expr5: "(baa | baa)+.(a|b)*"
            expr5 = expr4.Dot(all);
        }

        private void printLanguage(SortedSet<String> set)
        {
            foreach (string val in set)
            {
                Console.WriteLine(val);
            }
        }

        public void TestLanguage()
        {
            Console.WriteLine("taal van(baa):\n");
            printLanguage(expr1.GetLanguage(5));

            Console.WriteLine("taal van (bb):\n");
            printLanguage(expr2.GetLanguage(5));

            Console.WriteLine("taal van (baa | bb):\n");
            printLanguage(expr3.GetLanguage(5));

            Console.WriteLine("taal van (a|b)*:\n");
            printLanguage(all.GetLanguage(5));
            Console.WriteLine("taal van (baa | bb)+:\n");
            printLanguage(expr4.GetLanguage(5));
            Console.WriteLine("taal van (baa | bb)+ (a|b)*:\n");
            printLanguage(expr5.GetLanguage(6));
        }
    }
}