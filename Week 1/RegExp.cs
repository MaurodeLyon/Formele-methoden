using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    class RegExp
    {
        private Operator _operator;
        public Operator getOperator() { return _operator; }
        public void setOperator(Operator op) { _operator = op; }

        private String _terminals;
        public String getTerminals() { return _terminals; }
        public void setTerminals(String term) { _terminals = term; }

        public enum Operator { PLUS, STAR, OR, DOT, ONE }

        private RegExp _left;
        public RegExp getLeftRegExp() { return _left; }
        public void setLeftRegExp(RegExp exp) { _left = exp; }

        private RegExp _right;
        public RegExp getRightRegExp() { return _right; }
        public void setRightRegExp(RegExp exp) { _right = exp; }

        private static readonly IComparer<string> compareByLength = new RegExpComparator();

        private class RegExpComparator : IComparer<string>
        {
            public RegExpComparator()
            {}
            public int Compare(string s1, string s2)
            {
                if (s1.Length == s2.Length)
                {
                    return s1.CompareTo(s2);
                }
                else
                {
                    return s1.Length - s2.Length;
                }
            }
           
        }
        

        public RegExp()
        {
            _operator = Operator.ONE;
            _terminals = "";
            _left = null;
            _right = null;
        }

        public RegExp(String p)
        {
            _operator = Operator.ONE;
            _terminals = p;
            _left = null;
            _right = null;
        }

        public RegExp plus()
        {
            RegExp result = new RegExp();
            result.setOperator(Operator.PLUS);
            result.setLeftRegExp(this);
            return result;
        }

        public RegExp star()
        {
            RegExp result = new RegExp();
            result.setOperator(Operator.STAR);
            result.setLeftRegExp(this);
            return result;
        }

        public RegExp or(RegExp e2)
        {
            RegExp result = new RegExp();
            result.setOperator(Operator.OR);
            result.setLeftRegExp(this);
            result.setRightRegExp(e2);
            return result;
        }

        public RegExp dot(RegExp e2)
        {
            RegExp result = new RegExp();
            result.setOperator(Operator.DOT);
            result.setLeftRegExp(this);
            result.setRightRegExp(e2);
            return result;
        }

        public SortedSet<String> getLanguage(int maxSteps)
        {
            SortedSet<string> emptyLanguage = new SortedSet<string>(compareByLength);
            SortedSet<string> languageResult = new SortedSet<string>(compareByLength);

            SortedSet<string> languageLeft, languageRight;

            if (maxSteps < 1) return emptyLanguage;

            switch (this._operator)
            {
                case RegExp.Operator.ONE:
                    { languageResult.Add(_terminals); goto case RegExp.Operator.OR; }

                case RegExp.Operator.OR:
                    languageLeft = _left == null ? emptyLanguage : _left.getLanguage(maxSteps - 1);
                    languageRight = _right == null ? emptyLanguage : _right.getLanguage(maxSteps - 1);
                    foreach (String s in languageLeft)
                    {
                        languageResult.Add(s);
                    }
                    foreach (String s in languageRight)
                    {
                        languageResult.Add(s);
                    }

                    break;

                case RegExp.Operator.DOT:
                    languageLeft = _left == null ? emptyLanguage : _left.getLanguage(maxSteps - 1);
                    languageRight = _right == null ? emptyLanguage : _right.getLanguage(maxSteps - 1);
                    foreach (string s1 in languageLeft)
                    {
                        foreach (string s2 in languageRight)
                        {
                            languageResult.Add(s1 + s2);
                        }
                    }
                    break;
                case RegExp.Operator.STAR:
                case RegExp.Operator.PLUS:
                    languageLeft = _left == null ? emptyLanguage : _left.getLanguage(maxSteps - 1);
                    foreach (String s in languageLeft)
                    {
                        languageResult.Add(s);
                    }

                    for (int i = 1; i < maxSteps; i++)
                    {
                        HashSet<string> languageTemp = new HashSet<string>(languageResult);
                        foreach (string s1 in languageLeft)
                        {
                            foreach (string s2 in languageTemp)
                            {
                                languageResult.Add(s1 + s2);
                            }
                        }
                    }
                    if (this._operator == Operator.STAR)
                    {
                        languageResult.Add("");
                    }
                    break;
                default:
                    Console.WriteLine("getLanguage does not support operator " + this._operator);
                    break;

            }
            return languageResult;
        }



            }
}
