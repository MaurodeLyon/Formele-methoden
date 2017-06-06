using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    //When I wrote this, only God and I understood what I was doing
    //Now, God only knows
    
    //You may think you know what the following code does.
    //But you dont. Trust me.
    //Fiddle with it, and youll spend many a sleepless
    //night cursing the moment you thought youd be clever
    //enough to "optimize" the code below.
    //Now close this file and go play with something else.
 
    class HopcroftAlgorithm
    {

        public static Automaat<string> minimizeDFA(Automaat<string> automaat)
        {
            SortedSet<Partition> partitions = new SortedSet<Partition>();
            Partition nonFinals = new Partition('A');
            Partition finals = new Partition('B');

            SortedSet<string> states = automaat.States;
            foreach (string state in states)
            {
                List<Transition<string>> trans = automaat.GetTransition(state);
                Row row = new Row();
                foreach (Transition<string> t in trans)
                {
                    row.addRoute(t.Symbol, t.ToState);
                }

                if (automaat.FinalStates.Contains(state))
                {
                    finals.addRow(state, row);
                }
                else
                {
                    nonFinals.addRow(state, row);
                }
            }

            partitions.Add(nonFinals);
            partitions.Add(finals);

            partitions = minimizePartitions(partitions, automaat.Symbols);
            printPartitions(partitions);

            ///Non-recursive way of handling minimizing
            //int oldSize = partitions.Count;
            //int newSize = 0;

            //while (oldSize!=newSize)
            //{
            //    oldSize = partitions.Count;
            //    partitions = minimizePartitions(partitions, automaat.Symbols);
            //    newSize = partitions.Count;
            //    printPartitions(partitions);
            //    Console.WriteLine("------------------------------------------");
            //}
            //printPartitions(partitions);
            
            foreach(Partition p in partitions)
            {
                foreach(string finalState in automaat.FinalStates)
                {
                    if(p.containsState(finalState))
                    {
                        p.isFinal = true;
                    }
                }
            }

            return retrieveDFAFromPartitions(partitions,automaat.Symbols);
        }


        private static SortedSet<Partition> minimizePartitions(SortedSet<Partition> partitions,SortedSet<char> symbols)
        {
            //Clusterfuck
            List<Dictionary<string, SortedSet<string>>> splittedPartitions = new List<Dictionary<string, SortedSet<string>>>();
            SortedSet<Partition> newPartitions = new SortedSet<Partition>();

            foreach (Partition p in partitions)
            {
                Dictionary<string, SortedSet<string>> newPartition = new Dictionary<string, SortedSet<string>>();
                foreach (KeyValuePair<string, Row> row in p.rows)
                {
                    string phrase = "";
                    foreach (KeyValuePair<char, string> innerRow in row.Value.innerRows)
                    {
                        foreach (Partition p2 in partitions)
                        {
                            if (p2.containsState(innerRow.Value))
                            {
                                phrase += p2.identifier + "-";
                            }   
                        }
                    }
                    phrase = phrase.TrimEnd('-');
                    if (!newPartition.ContainsKey(phrase))
                        newPartition.Add(phrase, new SortedSet<string>());

                    newPartition[phrase].Add(row.Key);
                }
                splittedPartitions.Add(newPartition);
            }


            int index = 0;
            foreach(Dictionary<string, SortedSet<string>> newPartition in splittedPartitions)
            {
                
                foreach (KeyValuePair<string, SortedSet<string>> entry in newPartition)
                {
                    Partition partition = new Partition((char)(index + 65));
                    foreach (string s in entry.Value)
                    {
                        foreach (Partition p2 in partitions)
                        {
                            if (p2.containsState(s))
                            {
                                partition.addRow(s,p2.getRow(s));
                                break;
                            }
                            //break;
                        }
                    }
                    newPartitions.Add(partition);
                    index++;
                }
                
            }

            if(newPartitions.Count!=partitions.Count)
            {
               printPartitions(newPartitions);
               newPartitions = minimizePartitions(newPartitions, symbols);
            }
            return newPartitions;

        }


        private static Automaat<string> retrieveDFAFromPartitions(SortedSet<Partition> partitions, SortedSet<char> symbols)
        {
            Automaat<string> automaat = new Automaat<string>(symbols);
            foreach (Partition p in partitions)
            {
                if (p.isFinal)
                    automaat.DefineAsFinalState(p.identifier.ToString());
                foreach (KeyValuePair<string, Row> row in p.rows)
                {

                    foreach(KeyValuePair<char,string> innerRow in row.Value.innerRows)
                    {
                        string toState = ""; ; 
                        foreach (Partition p2 in partitions)
                        {
                            if (p2.containsState(innerRow.Value))
                            {
                                toState = p2.identifier.ToString();
                                break;
                            }
                        }
                        automaat.AddTransition(new Transition<string>(p.identifier.ToString(), innerRow.Key, toState));
                    }
                }
            }

            return automaat;
        }

        public static void printPartitions(SortedSet<Partition> partitions)
        {
            Console.WriteLine("-----------------------------------------------------------");
            foreach (Partition p in partitions)
            {
                Console.WriteLine(p.identifier+":");

                foreach (KeyValuePair<string, Row> row in p.rows)
                {
                    Console.Write("     "+row.Key+":");

                    foreach (KeyValuePair<char,string> innerRow in row.Value.innerRows)
                    {
                        Console.Write(" " + innerRow.Key + "-" + innerRow.Value + " ");


                    }

                    Console.WriteLine("");
                }
            }
        }
    }

    
    class Partition : IComparable<Partition>
    {
        //State and row
        //Example 1: a-2 b-3
        public Dictionary<string, Row> rows { get; }
        public char identifier { get; }
        public bool isFinal { get; set; }
       // private SortedSet<char> symbols;

        public Partition(char ID)
        {
            this.identifier = ID;
            this.isFinal = false;
            //this.symbols = symbols;
            this.rows = new Dictionary<string, Row>();
        }

        public void addRow(string state, Row row)
        {
            rows[state] = row;
        }

        public Row getRow(string state)
        {
            return rows[state];
        }

        public bool containsState(string state)
        {
            foreach (KeyValuePair<string,Row> row in rows)
            {
                if(row.Key==state)
                {
                    return true;
                }
            }

            return false;
        }

        public int CompareTo(Partition other)
        {
            if (other == null) return 1;

            return this.identifier.CompareTo(other.identifier);
        }

    }

    class Row
    {
        //innerRow means a char of the alphabet + its corresponding state
        //Example char:a string:2
        public Dictionary<char, string> innerRows { get; }

        public Row()
        {
            innerRows = new Dictionary<char, string>();
        }
        public void addRoute(char symbol, string toState)
        {
            innerRows[symbol] = toState;
        }

    }


}
