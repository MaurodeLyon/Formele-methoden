using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    class ndfaToDfaConverter
    {

        private static void convertState(string state, Automata<string> dest, Automata<string> source)
        {
            ///Recursief
            ///

            if (state.Contains(','))
            {
               //connect each split state
               // foreach(string )
            }
            else
            {

            }
            List<Transition<string>> transitions = source.GetTransition(state);

            foreach (char c in source.Symbols)
            {
                int count = checkAmountOfRoutesForChar(c, transitions);
                string toState = "";
                ///Faulty state implement TODO

                if (count >= 1)
                {
                    //List<Transition<string>> state transitions.Where(e => e.FromState.Equals(state)).ToList();
                    foreach (Transition<string> t in transitions)
                    {
                        if (t.Symbol == c)
                        {
                            toState += t.ToState;
                            toState += ",";
                        }
                    }
                    dest.AddTransition(new Transition<string>(state, c, toState));
                    convertState(toState, dest, source);
                    //foreach (string orState in toState.Split(','))
                    //{
                    //    convertState(orState, dest, source);
                    //}

                }
                else if (count == 0)
                {
                    dest.AddTransition(new Transition<string>(state, c, "FFFF"));
                }

            }

        }

        public static void convert(Automata<string> automata)
        {

            Automata<string> m= new Automata<string>(automata.Symbols);
            SortedSet<string> states = automata.States;
            convertState(states.ToList<string>()[0], m, automata);

            foreach (string s in states)
            {
                List<Transition<string>> transitions = automata.GetTransition(s);

                foreach(char c in automata.Symbols)
                {
                    int count = checkAmountOfRoutesForChar(c, transitions);
                    string toState = "";
                    ///Faulty state implement TODO
                   
                    if(count>=1)
                    {
                        //List<Transition<string>> state transitions.Where(e => e.FromState.Equals(state)).ToList();
                        foreach(Transition<string>  t in transitions)
                        {
                            if(t.Symbol == c)
                            {
                                toState += t.ToState;
                                toState += ",";
                            }
                        }
                        m.AddTransition(new Transition<string>(s, c, toState));
                    }
                    else
                    {

                    }

                }



            }

        }

        private static int checkAmountOfRoutesForChar(char c, List<Transition<string>> transitions)
        {
            int count = 0;

            foreach(Transition<string> t in transitions)
            {
                if(t.Symbol==c)
                {
                    count++;
                }
            }

            return count;
        }

        ///helper function for parsing multiple states out of one string TODO
    }
}
