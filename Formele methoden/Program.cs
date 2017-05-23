using System;


namespace Week_1
{
    class Program
    {
        static void Main()
        {
            bool looping = true;
            while (looping)
            {
                string word = Console.ReadLine();
                switch (word)
                {
                    case "quit":
                        looping = false;
                        break;
                    case "print":
                        break;
                    default:
                        break;
                }
            }
        }
    }
}