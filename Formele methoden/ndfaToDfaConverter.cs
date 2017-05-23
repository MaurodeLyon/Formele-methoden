using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    class ndfaToDfaConverter
    {

        private static void convertState(string state, ref Automata<string> dest, ref Automata<string> source)
        {
            ///Recursief
            ///
            if(dest.GetTransition(state).Count==2)
            {
                return;
            }
            String[] states = state.Split('_');
           

            foreach (char c in source.Symbols)
            {
                
                int[] counts = new int[states.Length];

                for (int i = 0; i < states.Length; i++) { 
                    List<Transition<string>> transitions = source.GetTransition(states[i]);
                    int count = checkAmountOfRoutesForChar(c, transitions);
                    counts[i] = count;

                }

                
                int amountOfRoutes = 0;
                foreach( int i in counts)
                {
                    if(i > amountOfRoutes)
                    {
                        amountOfRoutes = i;
                    }
                }

                if(amountOfRoutes==0)
                {
                    dest.AddTransition(new Transition<string>(state, c, "FFFF"));
                }
                string toState = "";
                bool isFinalState = false;
                SortedSet<String> newState = new SortedSet<string>();
                if (amountOfRoutes >= 1)
                {
                    foreach (string s in states)
                    {
                        List<Transition<string>> trans = source.GetTransition(s);
                        foreach (Transition<string> t in trans)
                        {
                            if (t.Symbol == c)
                            {
                                newState.Add(t.ToState);
                                if (source.FinalStates.Contains(t.ToState))
                                {
                                    isFinalState = true;
                                }

                            }
                            
                        }


                    }

                    foreach (string subState in newState)
                    {
                        toState += subState;
                        toState += "_";
                    }
                    if (newState.Count >= 1)
                    {
                        toState =toState.TrimEnd('_');
                    }
                        
                    


                    dest.AddTransition(new Transition<string>(state, c, toState));

                    if (isFinalState)
                    dest.DefineAsFinalState(toState);
                    


                    if(state!=toState)
                    convertState(toState, ref dest, ref source);
                }
                

         

            }

        }

        public static Automata<string> convert(Automata<string> automata)
        {

            Automata<string> m= new Automata<string>(automata.Symbols);
            SortedSet<string> states = automata.States;
            convertState(states.ToList<string>()[0], ref m, ref automata);

            return m;

      

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
