using System.Collections.Generic;

namespace Week_1
{
    public class RegExp
    {
        public OperatorEnum Operator { get; set; }

        public string Terminals { get; set; }

        public enum OperatorEnum
        {
            Plus,
            Star,
            Or,
            Dot,
            One
        }

        public RegExp Left { get; set; }

        public RegExp Right { get; set; }

        private static readonly IComparer<string> CompareByLength = new RegExpComparator();

        private class RegExpComparator : IComparer<string>
        {
            public int Compare(string s1, string s2)
            {
                if (s1.Length == s2.Length)
                    return s1.CompareTo(s2);
                return s1.Length - s2.Length;
            }
        }

        public RegExp()
        {
            Operator = OperatorEnum.One;
            Terminals = "";
            Left = null;
            Right = null;
        }

        public RegExp(string regex)
        {
            RegExp regExp = String2Regex(new RegExp(), regex);
            Operator = regExp.Operator;
            Terminals = regExp.Terminals;
            Left = regExp.Left;
            Right = regExp.Right;
        }

        public RegExp String2Regex(RegExp regex, string stringToRegex)
        {
            for (int index = 0; index < stringToRegex.Length; index++)
            {
                char currentChar = stringToRegex[index];
                if (currentChar == '(')
                {
                    int closingBracketPosition = -1;
                    int bracketCount = 0;
                    for (int i = index + 1; i < stringToRegex.Length; i++)
                    {
                        if (stringToRegex[i] == '(') bracketCount++;
                        if (stringToRegex[i] == ')' && bracketCount == 0)
                        {
                            closingBracketPosition = i;
                            break;
                        }
                        if (stringToRegex[i] == ')' && bracketCount != 0)
                        {
                            bracketCount--;
                        }
                    }
                    string between = stringToRegex.Substring(index + 1, closingBracketPosition - 1 - index);
                    RegExp regExp = String2Regex(new RegExp(), between);
                    if (closingBracketPosition + 1 < stringToRegex.Length)
                    {
                        index = closingBracketPosition + 1;
                        currentChar = stringToRegex[index];
                        if (currentChar == '+')
                        {
                            regExp = regExp.Plus();
                        }
                        else if (currentChar == '*')
                        {
                            regExp = regExp.Star();
                        }
                    }
                    if (regex.Terminals == "" && regex.Operator == RegExp.OperatorEnum.One)
                    {
                        regex = regExp;
                    }
                    else
                    {
                        regex = regex.Dot(regExp);
                    }
                }
                else if (currentChar == '+')
                {
                    regex = regex.Plus();
                }
                else if (currentChar == '*')
                {
                    regex = regex.Star();
                }
                else if (currentChar == '|')
                {
                    regex = regex.Or(new RegExp(stringToRegex[index + 1].ToString()));
                    index++;
                }
                else
                {
                    if (regex.Terminals == "" && regex.Operator == RegExp.OperatorEnum.One)
                    {
                        regex.Terminals = currentChar.ToString();
                    }
                    else
                    {
                        regex = regex.Dot(new RegExp(currentChar.ToString()));
                    }
                }
            }
            return regex;
        }

        public RegExp Plus()
        {
            RegExp result = new RegExp();
            result.Operator = OperatorEnum.Plus;
            result.Left = this;
            return result;
        }

        public RegExp Star()
        {
            RegExp result = new RegExp();
            result.Operator = OperatorEnum.Star;
            result.Left = this;
            return result;
        }

        public RegExp Or(RegExp e2)
        {
            RegExp result = new RegExp();
            result.Operator = OperatorEnum.Or;
            result.Left = this;
            result.Right = e2;
            return result;
        }

        public RegExp Dot(RegExp e2)
        {
            RegExp result = new RegExp();
            result.Operator = OperatorEnum.Dot;
            result.Left = this;
            result.Right = e2;
            return result;
        }

        public SortedSet<string> GetLanguage(int maxSteps)
        {
            SortedSet<string> emptyLanguage = new SortedSet<string>(CompareByLength);
            SortedSet<string> languageResult = new SortedSet<string>(CompareByLength);

            SortedSet<string> languageLeft, languageRight;

            if (maxSteps < 1) return emptyLanguage;

            switch (Operator)
            {
                case OperatorEnum.One:
                    languageResult.Add(Terminals);
                    goto case OperatorEnum.Or;

                case OperatorEnum.Or:
                    languageLeft = Left == null ? emptyLanguage : Left.GetLanguage(maxSteps - 1);
                    languageRight = Right == null ? emptyLanguage : Right.GetLanguage(maxSteps - 1);
                    foreach (string s in languageLeft)
                        languageResult.Add(s);
                    foreach (string s in languageRight)
                        languageResult.Add(s);
                    break;

                case OperatorEnum.Dot:
                    languageLeft = Left == null ? emptyLanguage : Left.GetLanguage(maxSteps - 1);
                    languageRight = Right == null ? emptyLanguage : Right.GetLanguage(maxSteps - 1);
                    foreach (string s1 in languageLeft)
                    foreach (string s2 in languageRight)
                        languageResult.Add(s1 + s2);
                    break;

                case OperatorEnum.Star:
                case OperatorEnum.Plus:
                    languageLeft = Left == null ? emptyLanguage : Left.GetLanguage(maxSteps - 1);
                    foreach (string s in languageLeft)
                        languageResult.Add(s);

                    for (int i = 1; i < maxSteps; i++)
                    {
                        HashSet<string> languageTemp = new HashSet<string>(languageResult);
                        foreach (string s1 in languageLeft)
                        foreach (string s2 in languageTemp)
                            languageResult.Add(s1 + s2);
                    }
                    if (Operator == OperatorEnum.Star)
                        languageResult.Add("");
                    break;
            }
            return languageResult;
        }

        public override string ToString()
        {
            string text = "";
            if (Terminals != "")
            {
                text = Terminals;
            }
            else
            {
                if (Right != null)
                    text += "(";
                text += Left.ToString();
                switch (Operator)
                {
                    case OperatorEnum.Plus:
                        text += "+";
                        break;
                    case OperatorEnum.Star:
                        text += "*";
                        break;
                    case OperatorEnum.Or:
                        text += "|";
                        break;
                    case OperatorEnum.Dot:
                        text += ".";
                        break;
                }
                if (Right != null)
                {
                    text += Right.ToString();
                    text += ")";
                }
            }
            return text;
        }
    }
}