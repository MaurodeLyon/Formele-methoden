using System;
using System.Collections.Generic;


namespace Week_1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Formele methoden test applicatie Mauro de Lyon & Arthur Brink");
            Console.WriteLine("-------------------------------------------------------------");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please select a regular expression:");
                Console.WriteLine("1: a+");
                Console.WriteLine("2: (a|b)*");
                Console.WriteLine("3: (d|e)*.(a+.(b|c)+");
                Console.WriteLine("4: Enter own regular expression");
                Console.WriteLine("5: Generate own DFA");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();
                switch (keyInfo.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Option 1 selected : a+");
                        Execute(new RegExp("a").Plus());
                        break;
                    case '2':
                        Console.WriteLine("Option 2 selected : (a|b)*");
                        Execute(new RegExp("a").Or(new RegExp("b")).Star());
                        break;
                    case '3':
                        Console.WriteLine("Option 3 selected : (d|e)*.(a+.(b|c)+");
                        Execute(new RegExp("d").Or(new RegExp("e")).Star().Dot(new RegExp("a").Plus().Dot(new RegExp("b").Or(new RegExp("c")).Plus())));
                        break;
                    case '4':
                        Console.WriteLine("Please enter your own regular expression");
                        Execute(new RegExp(Console.ReadLine()));
                        break;
                    case '5':
                        
                        Console.WriteLine("Please enter DFA alphabet");
                        string alphabet = Console.ReadLine();
                        
                        Console.WriteLine("DFA should:");
                        Console.WriteLine("Select option");
                        Console.WriteLine("1: Begin with");
                        Console.WriteLine("2: Not begin with");
                        Console.WriteLine("3: No begin with rule");
                        ConsoleKeyInfo optionInfo = Console.ReadKey();
                        char beginWithOption = optionInfo.KeyChar;
                        Console.WriteLine("");
                        if (!validDfaOption(beginWithOption))
                        {
                            Console.WriteLine("Invalid/Incorrect parameters entered, try again");
                            break;
                        }
                           

                        
                        Console.WriteLine("Enter line");
                        
                        string beginWith = Console.ReadLine();

                        Console.WriteLine("Select option");
                        Console.WriteLine("1: Contain");
                        Console.WriteLine("2: Not contain");
                        Console.WriteLine("3: No contain rule");
                        optionInfo = Console.ReadKey();
                        char containOption = optionInfo.KeyChar;
                        Console.WriteLine("");
                        if (!validDfaOption(containOption))
                        {
                            Console.WriteLine("Invalid/Incorrect parameters entered, try again");
                            break;
                        }
                            

                        
                        Console.WriteLine("Enter line");
                        
                        string contain = Console.ReadLine();

                        Console.WriteLine("Select option");
                        Console.WriteLine("1: End with");
                        Console.WriteLine("2: Not end with");
                        Console.WriteLine("3: No end with rule");
                        optionInfo = Console.ReadKey();
                        char endWithOption = optionInfo.KeyChar;
                        Console.WriteLine("");
                        if (!validDfaOption(endWithOption))
                        {
                            Console.WriteLine("Invalid/Incorrect parameters entered, try again");
                            break;
                        }
                            

                       
                        Console.WriteLine("Enter line");
                        
                        string endWith = Console.ReadLine();

                        Automaat<string>.DfaGenerateValue param1;
                        Automaat<string>.DfaGenerateValue param2;
                        Automaat<string>.DfaGenerateValue param3;

                        Automaat<string> part1=null;
                        Automaat<string> part2=null;
                        Automaat<string> part3=null;
                       
                        List<Automaat<string>> setupParts = new List<Automaat<string>>();
                        if (beginWith!="" &&beginWithOption != '3')
                        {
                            if (beginWithOption == '1')
                            {
                                param1 = new Automaat<string>.DfaGenerateValue { Parameter = beginWith, IsNot = false, Type = Automaat<string>.GeneratorType.BeginsWith };
                                part1 = Automaat<string>.GenerateDfa(param1, alphabet.ToCharArray());
                            }
                            if(beginWithOption=='2')
                            {
                                param1 = new Automaat<string>.DfaGenerateValue { Parameter = beginWith, IsNot = true, Type = Automaat<string>.GeneratorType.BeginsWith };
                                part1 = Automaat<string>.GenerateDfa(param1, alphabet.ToCharArray());
                            }
                            setupParts.Add(part1);
                        }

                        if (contain != "" && containOption != '3')
                        {
                            if (containOption == '1')
                            {
                                param2 = new Automaat<string>.DfaGenerateValue { Parameter = contain, IsNot = false, Type = Automaat<string>.GeneratorType.Contains };
                                part2 = Automaat<string>.GenerateDfa(param2, alphabet.ToCharArray());
                            }
                            if (containOption == '2')
                            {
                                param2 = new Automaat<string>.DfaGenerateValue { Parameter = contain, IsNot = true, Type = Automaat<string>.GeneratorType.Contains };
                                part2 = Automaat<string>.GenerateDfa(param2, alphabet.ToCharArray());
                            }
                            setupParts.Add(part2);
                        }

                        if (endWith != "" && beginWithOption != '3')
                        {
                            if (endWithOption == '1')
                            {
                                param3 = new Automaat<string>.DfaGenerateValue { Parameter = endWith, IsNot = false, Type = Automaat<string>.GeneratorType.EndsWith };
                                part3 = Automaat<string>.GenerateDfa(param3, alphabet.ToCharArray());
                            }
                            if (beginWithOption == '2')
                            {
                                param3 = new Automaat<string>.DfaGenerateValue { Parameter = endWith, IsNot = true, Type = Automaat<string>.GeneratorType.EndsWith };
                                part3 = Automaat<string>.GenerateDfa(param3, alphabet.ToCharArray());
                            }
                            setupParts.Add(part3);

                        }
                        Automaat<string> takeOne = null; ;
                        Automaat<string> takeTwo = null; ;
                        Automaat<string> toUse = null; ;
                        if(setupParts.Count==1)
                        {
                            toUse = setupParts[0];
                        }
                        else if(setupParts.Count==2)
                        {
                            toUse = Automaat<string>.Union(setupParts[0], setupParts[1]);
                            
                        }
                        else if(setupParts.Count==3)
                        {
                            takeOne = Automaat<string>.Union(setupParts[0], setupParts[1]);
                            takeTwo = Automaat<string>.Union(takeOne, setupParts[2]);
                            toUse = takeTwo;
                        }
                        if(toUse!=null)
                        {
                            Console.WriteLine("----- dfa generated -----");
                            foreach (Transition<string> transition in toUse.Transitions)
                            {
                                Console.WriteLine(transition.ToString());
                            }

                            Console.WriteLine("Printing image");
                            GraphVizParser.PrintGraph(toUse, "customDFA");
                            Console.WriteLine("Image saved as customDFA.jpg");

                            Console.WriteLine();
                            Console.WriteLine("----- Optimizing custom dfa: Brzozowski's algorithm -----");
                            Automaat<string> brzozowskiDfa = NdfaToDfaConverter.OptimizeDfa(toUse);
                            foreach (Transition<string> transition in brzozowskiDfa.Transitions)
                            {
                                Console.WriteLine(transition.ToString());
                            }
                            Console.WriteLine("Printing image");
                            GraphVizParser.PrintGraph(brzozowskiDfa, "CustomBrzozowskiDFA");
                            Console.WriteLine("Image saved as CustomBrzozowskiDFA.jpg");

                            // Optimize dfa Hopcroft
                            Console.WriteLine();
                            Console.WriteLine("----- Optimizing custom dfa: Hopcroft's algorithm -----");
                            Automaat<string> hopcroftDfa = HopcroftAlgorithm.MinimizeDfa(toUse);
                            foreach (Transition<string> transition in hopcroftDfa.Transitions)
                            {
                                Console.WriteLine(transition.ToString());
                            }
                            Console.Write("Amount of states in hopcroft dfa: ");
                            Console.WriteLine(hopcroftDfa.States.Count);
                            Console.WriteLine("Printing image");
                            GraphVizParser.PrintGraph(hopcroftDfa, "CustomHopcroftDFA");
                            Console.WriteLine("Image saved as CustomHopcroftDFA.jpg");
                            if(hopcroftDfa.States.Count>25)
                            {
                                Console.WriteLine("[Warning] This hopcroft dfa has more than 25 states.");
                                Console.WriteLine("Due to char(ASCII) limitations, parsing issues could arise with printing this dfa correctly to an image");
                                Console.WriteLine("The above shown list of transitions from this hopcroft dfa ARE STILL CORRECT however");
                            }
                        }
                        else { Console.WriteLine("Invalid/Incorrect parameters entered, try again"); }

                        //Automaat<string> takeOne = Automaat<string>.Union(part1, part2);
                        
                        //Automaat<string> takeTwo = Automaat<string>.Union(takeOne, part3);

                        //GraphVizParser.PrintGraph(toUse, "customDfa");

                        break;

                    default:
                        Console.WriteLine("incorrect key entered.");
                        break;
                }
            }
        }
        private static bool validDfaOption(char option)
        {
            if (option == '1' || option == '2' || option == '3')
                return true;
            else
                return false;
        }
        public static void Execute(RegExp regExp)
        {
            // Convert regex to ndfa
            Console.WriteLine();
            Console.WriteLine("----- Converting regex to ndfa -----");
            Automaat<string> ndfa = ThompsonConstruction.ConvertRegExp(regExp);
            foreach (Transition<string> transition in ndfa.Transitions)
            {
                Console.WriteLine(transition.ToString());
            }
            Console.WriteLine("Printing image");
            GraphVizParser.PrintGraph(ndfa, "ndfa");
            Console.WriteLine("Image saved as ndfa.jpg");

            // Convert ndfa to dfa
            Console.WriteLine();
            Console.WriteLine("----- Converting ndfa to dfa -----");
            Automaat<string> dfa = NdfaToDfaConverter.Convert(ndfa);
            foreach (Transition<string> transition in dfa.Transitions)
            {
                Console.WriteLine(transition.ToString());
            }
            Console.WriteLine("Printing image");
            GraphVizParser.PrintGraph(dfa, "dfa");
            Console.WriteLine("Image saved as dfa.jpg");

            // Optimizing dfa Brzozowski
            Console.WriteLine();
            Console.WriteLine("----- Optimizing dfa: Brzozowski's algorithm -----");
            Automaat<string> brzozowskiDfa = NdfaToDfaConverter.OptimizeDfa(dfa);
            foreach (Transition<string> transition in brzozowskiDfa.Transitions)
            {
                Console.WriteLine(transition.ToString());
            }
            Console.WriteLine("Printing image");
            GraphVizParser.PrintGraph(brzozowskiDfa, "BrzozowskiDfa");
            Console.WriteLine("Image saved as BrzozowskiDfa.jpg");

            // Optimize dfa Hopcroft
            Console.WriteLine();
            Console.WriteLine("----- Optimizing dfa: Hopcroft's algorithm -----");
            Automaat<string> hopcroftDfa = HopcroftAlgorithm.MinimizeDfa(dfa);
            foreach (Transition<string> transition in hopcroftDfa.Transitions)
            {
                Console.WriteLine(transition.ToString());
            }
            Console.WriteLine("Printing image");
            GraphVizParser.PrintGraph(hopcroftDfa, "HopcroftDfa");
            Console.WriteLine("Image saved as HopcroftDfa.jpg");
        }
    }
}