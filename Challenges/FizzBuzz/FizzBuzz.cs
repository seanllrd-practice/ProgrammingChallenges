using Microsoft.Win32.SafeHandles;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenges
{
    public class FizzBuzz
    {
        private static void Main(string[] args)
        {
            Play();

            Console.ReadLine();
            Environment.Exit(0);
        }

        public static void Play()
        {
            string error = "";
            bool quit = false;
            while (!quit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("FizzBuzz\n\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(error);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Enter Starting Number\n> ");
                string start = Console.ReadLine();
                Console.Write("Enter Ending Number\n> ");
                string end = Console.ReadLine();

                if (!VerifyInput(start) && !VerifyInput(end))
                {
                    error = "Invalid input. Input should be integers.\n";
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n{PatternGenerator(Convert.ToInt32(start), Convert.ToInt32(end))}");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Continue [y/n]?\n> ");
                string input = Console.ReadLine();

                if (input == "n" || input == "no")
                {
                    quit = true;
                }
            }

            Console.ResetColor();
            Console.WriteLine("\nThank you for playing!");
        }

        private static string PatternGenerator(int minimum = 1, int maximum = 100)
        {
            string result = "";
            for (int i = minimum; i <= maximum; i++)
            {
                result += i % 15 == 0 ? "FizzBuzz" : i % 3 == 0 ? "Fizz" : i % 5 == 0 ? "Buzz" : i.ToString();
                result += "\n";
            }

            return result;
        }

        private static bool VerifyInput(string input)
        {
            int number;
            bool result = Int32.TryParse(input, out number);

            return result;
        }
    }
}
