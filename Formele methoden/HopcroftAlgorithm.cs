using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    
    class HopcroftAlgorithm
    {

        public static Automaat<string> minimizeDFA(Automaat<string> automaat)
        {
            SortedSet<Partition> partitions = new SortedSet<Partition>();
            Partition nonFinals = new Partition('A', automaat.Symbols);
            Partition finals = new Partition('B', automaat.Symbols);

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

            printPartitions(partitions);
            return null;
        }

        public static void printPartitions(SortedSet<Partition> partitions)
        {
            foreach (Partition p in partitions)
            {
                Console.WriteLine(p.identifier+":");

                foreach (KeyValuePair<string, Row> row in p.rows)
                {
                    Console.Write(row.Key+":");

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
        public Dictionary<string, Row> rows { get; }
        public char identifier { get; }
        private SortedSet<char> symbols;

        public Partition(char ID, SortedSet<char> symbols)
        {
            this.identifier = ID;
            this.symbols = symbols;
            this.rows = new Dictionary<string, Row>();
        }

        public void addRow(string state, Row row)
        {
            rows[state] = row;
        }

        public int CompareTo(Partition other)
        {
            if (other == null) return 1;

            return this.identifier.CompareTo(other.identifier);
        }

    }

    class Row
    {
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

        

    struct routeDef
    {
        private char symbol { get; set; }
        private char toState;
    }

}
