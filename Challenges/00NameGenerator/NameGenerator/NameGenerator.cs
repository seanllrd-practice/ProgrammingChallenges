using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Challenges.NameGenerator
{
    public class NameGenerator
    {
        public static void Main()
        {
            int length = GetNameLength();
            
            string result = RandomName(Convert.ToInt32(length));
            Console.WriteLine(result);
            Console.ReadLine();
        }

        static int GetNameLength()
        {
            int number;

            while (true)
            {
                Console.Write("Enter length of username: ");
                string input = Console.ReadLine();

                if (!Int32.TryParse(input, out number))
                {
                    Console.WriteLine("Invalid input. Please enter a number only.");
                    continue;
                }

                break;
            }

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
