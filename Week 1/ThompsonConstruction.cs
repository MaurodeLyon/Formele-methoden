using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    class ThompsonConstruction
    {

        //public static Automata<string> convertRegExpToAutomata(string regExp)
        //{
        //    char[] alphabet = { 'a', 'b' };
        //    Automata<string> m = new Automata<string>(alphabet);


        //    char[] splitRegExp = regExp.ToCharArray();
        //    int pointer = 0;
        //    for (int i = 0; i < splitRegExp.Length; i++)
        //    {
        //        switch(splitRegExp[i])
        //        {
        //            case 'a':
        //                break;
        //            case 'b':
        //                break;
        //            case '|':
        //                string exp1 = "";
        //                string exp2 = "";
        //                for(int j = pointer+1 ; j < i; j++)
        //                {
        //                    exp1 += splitRegExp[j];
        //                }
        //                while(splitRegExp[i]!=')')
        //                {
        //                    exp2 += splitRegExp[i];
        //                }
        //                break;
        //            case '(':
        //                pointer = i;

        //                break;
                    
        //            default:
        //                break;
        //        }  
        //    }

        //    return m;


        //}
        
        public static Automata<string> convertRegExpToAutomata(RegExp regExp)
        {
            if(regExp.Terminals!="")
            {

            }


        }
    }
}
