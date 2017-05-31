using System;
using System.Collections.Generic;
using System.Linq;

namespace Week_1
{
    public class NdfaToDfaConverter
    {
        public static Automaat<string> Convert(Automaat<string> ndfa)
        {
            Automaat<string> dfa = new Automaat<string>(ndfa.Symbols);

            string combinedStartState = "";

            //New way of generating starting point for recursive method, needs test method before removing deprecated code
            foreach (string startState in ndfa.StartStates)
            {
                //ConvertState(startState, ref dfa, ref ndfa);
                //dfa.DefineAsStartState(startState);
                combinedStartState += startState;
                combinedStartState += "_";

            }

            combinedStartState= combinedStartState.TrimEnd('_');
            ConvertState(combinedStartState, ref dfa, ref ndfa);
            dfa.DefineAsStartState(combinedStartState);

            if (dfa.States.Contains("F"))
            {
                foreach (char route in dfa.Symbols)
                {
                    dfa.AddTransition(new Transition<string>("F", route, "F"));
                }
            }
            return dfa;
        }





        private static bool checkExistingRouteForChar(string currentState, char symbol, Automaat<string> dfa)
        {
            List<Transition<string>> currentTrans = dfa.GetTransition(currentState);
            foreach (Transition<string> t in currentTrans)
            {
                if (t.Symbol == symbol)
                {
                    return true;
                }
            }

            return false;
        }

        private static int checkAvailableRoutes(string[] states, char symbol, Automaat<string> ndfa)
        {
            //array which shows how many possible routes there are for each sub-state
            int[] possibleRoutesPerState = new int[states.Length];

            //// value that shows the amount of routes the ndfa has for all the substates combined.
            int correctAmountOfRoutes = 0;
            //reads ndfa for possible routes, saves maximum amount of accessible routes to correctAmountOfRoutes
            for (int i = 0; i < states.Length; i++)
            {
                if(ndfa.GetTransition(states[i]).Count(transition => transition.Symbol == symbol) > correctAmountOfRoutes)
                    correctAmountOfRoutes= ndfa.GetTransition(states[i]).Count(transition => transition.Symbol == symbol);

            }

            return correctAmountOfRoutes;
        }

        //Fills toState string with correct TOSTATE, returns true or false whether or not this new TOSTATE should be a final state
        private static bool generateToState(ref string toState, string[] states, char symbol, Automaat<string> ndfa)
        {
            
            //boolean that will save whether this new TOSTATE needs to be a finalstate
            bool isFinalState = false;
            //Set of all the substates that need to be combined.
            SortedSet<String> newState = new SortedSet<string>();
        
                //Loop through all the substates 
                foreach (string state in states)
                {
                    //ndfa transitions for state
                    List<Transition<string>> trans = ndfa.GetTransition(state);

                    //This loop goes through all the aforementioned transitions
                    //to see if there are routes with the correct symbol that need to be added to the new TOSTATE
                    foreach (Transition<string> t in trans)
                    {
                        if (t.Symbol == symbol)
                        {
                            newState.Add(t.ToState);
                            //Check if this state is final, if one of the substates for the new TOSTATE is final, TOSTATE becomes final as a whole.
                            if (ndfa.FinalStates.Contains(t.ToState))
                            {
                                isFinalState = true;
                            }
                        }
                    }
                }

                //combines substates into one string (TOSTATE)
                foreach (string subState in newState)
                {
                    toState += subState;
                    toState += "_";
                }
                toState = toState.TrimEnd('_');



                return isFinalState;
        }



        private static void ConvertState(string currentState, ref Automaat<string> dfa, ref Automaat<string> ndfa)
        {
            //If this state is already completely processed, return to avoid stackoverflow exception
            if (dfa.GetTransition(currentState).Count == ndfa.Symbols.Count)
                return;
            

            //split given state for comparison
            string[] states = currentState.Split('_');

            //Loop through all symbols aka all the necessary routes
            foreach (char symbol in ndfa.Symbols)
            {
                //checks if this symbol already has a route in the new DFA
                if(checkExistingRouteForChar(currentState, symbol, dfa))
                    return;

                int correctAmountOfRoutes = checkAvailableRoutes(states, symbol, ndfa);
                
                
                //the TOSTATE of the to be added implementation
                string toState = "";


                switch (correctAmountOfRoutes)
                {
                    case 0:
                        dfa.AddTransition(new Transition<string>(currentState, symbol, "F"));
                        break;
                    default:
                        bool isFinalState =generateToState(ref toState, states, symbol, ndfa);

                        dfa.AddTransition(new Transition<string>(currentState, symbol, toState));

                        //Checks if currentState is should be final aswell (could be done better)
                        if (ndfa.FinalStates.Contains(currentState))
                        {
                            dfa.DefineAsFinalState(currentState);
                        }

                        if (isFinalState)
                            dfa.DefineAsFinalState(toState);

                        //checks if its not a loop to itself
                        if (currentState != toState)
                            ConvertState(toState, ref dfa, ref ndfa);
                        break;
                }

               
            }
        }


        public static Automaat<string> Reverse(Automaat<string> automaat)
        {
            SortedSet<string> finalstates = automaat.FinalStates;
            SortedSet<string> startStates = automaat.StartStates;

            foreach (Transition<String> t in automaat.Transitions)
            {
                string fromState = t.FromState;
                string toState = t.ToState;

                t.FromState = toState;
                t.ToState = fromState;
            }

            automaat.FinalStates = startStates;
            automaat.StartStates = finalstates;

            return automaat;
        }
    }
}