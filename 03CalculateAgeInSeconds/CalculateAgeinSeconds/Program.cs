using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateAgeinSeconds
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            while (!quit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Calculate Age\n");
                Console.ResetColor();

                Console.WriteLine("Enter your birthday in the format DD MMM YYYY.");
                Console.WriteLine("Example: 01 JAN 1990\n");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Enter Your Birthday: ");
                string input = Console.ReadLine();
                Console.ResetColor();

                DateTime birthday = DateTime.Parse(input);
                DateTime today = DateTime.Now;

                TimeSpan timeElapsed = today - birthday;

                double yearsElapsed = (today.Month > birthday.Month) ? (today.Year - birthday.Year) : ((today.Year - birthday.Year) - 1);
                double monthsElapsed = (yearsElapsed * 12) - (today.Month - birthday.Month) + 1;
                double daysElapsed = timeElapsed.TotalDays;
                double hoursElapsed = timeElapsed.TotalHours;
                double minutesElapsed = timeElapsed.TotalMinutes;
                double secondsElapsed = timeElapsed.TotalSeconds;

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nTime Elapsed Since Birthday\n");
                Console.WriteLine($"Years: {yearsElapsed:n0}");
                Console.WriteLine($"Months: {monthsElapsed:n0}");
                Console.WriteLine($"Days: {daysElapsed:n0}");
                Console.WriteLine($"Hours: {hoursElapsed:n0}");
                Console.WriteLine($"Minutes: {minutesElapsed:n0}");
                Console.WriteLine($"Seconds: {secondsElapsed:n0}\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Continue [y/n]? ");
                string cont = Console.ReadLine();
                Console.ResetColor();

                if (cont == "n" || cont == "no")
                {
                    break;
                }

                Console.Write("Press enter to continue...");
                Console.ReadLine();
            }

            Console.Write("Press enter to quit...");
            Console.ReadLine();

            Environment.Exit(0);
        }
    }
}
