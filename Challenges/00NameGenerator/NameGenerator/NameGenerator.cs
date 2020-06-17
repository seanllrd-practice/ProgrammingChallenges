using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace NameGeneratorNS
{
    public class NameGenerator
    {
        private static void Main()
        {
            GenerateName();
        }

        public static void GenerateName()
        {
            bool quit = false;
            while (!quit)
            {
                Console.Clear();

                int length = GetNameLength();

                string result = RandomName(Convert.ToInt32(length));

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n{result}\n");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Generate another name [y/n]?\n> ");
                string input = Console.ReadLine();
                Console.ResetColor();

                if (input == "n" || input == "no")
                {
                    quit = true;
                }
            }
        }

        static int GetNameLength()
        {
            int number;
            string error = "";

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Name Generator\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(error);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Enter length of username\n> ");
                string input = Console.ReadLine();

                if (!Int32.TryParse(input, out number))
                {
                    error = "Invalid input. Please enter a number only.\n";
                    continue;
                }

                break;
            }

            Console.ResetColor();
            return number;
        }

        static string RandomName(int count)
        {
            char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            string result = "";
            Random random = new Random();
            TextInfo mTI = new CultureInfo("en-US", false).TextInfo;

            for (int i = 0; i < count; i++)
            {
                int randomLetter = random.Next(letters.Length);
                result += letters.GetValue(randomLetter);
            }

            result = mTI.ToTitleCase(result);
            return result;
        }
    }
}
