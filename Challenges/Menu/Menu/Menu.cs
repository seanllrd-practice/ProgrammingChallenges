using System;
using System.Collections.Generic;
using Challenges.NameGenerator;
using HigherLowerHeadsTailsNS;
using TemperatureConverterNS;
using CalculateAgeinSecondsNS;
using System.Linq;

namespace Menu
{
    class Menu
    {
        static void Main(string[] args)
        {
            List<Action> programs = new List<Action>
            {
                NameGenerator.Main,
                HigherLowerHeadsTails.Main,
                TemperatureConverter.Main,
                CalculateAgeinSeconds.Main
            };

            int gameChoice = DisplayMenu(programs);
        }

        static int DisplayMenu(List<Action> programs)
        {
            string input;
            int number;
            string error = "";

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main Menu\n");
                Console.ResetColor();

                Console.WriteLine("Select a game:\n");

                foreach (var game in programs.Select((Value, Index) => new { Value, Index }))
                {
                    Console.WriteLine($"{game.Index + 1}. {game.Value.Method.Module}");
                }
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("> ");
                input = Console.ReadLine();
                Console.ResetColor();

                if (!Int32.TryParse(input, out number))
                {
                    error = "\nInvalid Input. Enter the game number you want to play.";
                    continue;
                }
                else if (number > programs.Count || number < 1)
                {
                    error = "\nInvalid Number. Please enter a valid game number.";
                    continue;
                }
                break;
            }

            Console.Clear();
            return number - 1;
        }
    }
}
