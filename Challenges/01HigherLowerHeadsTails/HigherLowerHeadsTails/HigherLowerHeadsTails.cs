using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace HigherLowerHeadsTailsNS
{
    public class HigherLowerHeadsTails
    {
        private static void Main()
        {
            SelectGame();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Thanks for playing!");
            Console.ResetColor();
            Environment.Exit(0);
        }

        public static void SelectGame()
        {
            List<Func<Boolean>> gameList = new List<Func<Boolean>> { HigherLower, HeadsTails, Quit };
            bool quit = false;
            
            while (!quit)
            {
                int gameChoice = Menu(gameList);
                quit = gameList[gameChoice]();
            }
        }

        // Menu
        static int Menu(List<Func<Boolean>> games)
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

                    foreach (var game in games.Select((Value, Index) => new { Value, Index }))
                    {
                        Console.WriteLine($"{game.Index + 1}. {game.Value.Method.Name}");
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
                    else if (number > games.Count || number < 1)
                    {
                        error = "\nInvalid Number. Please enter a valid game number.";
                        continue;
                    }
                    break;
                }

                return number - 1;
            }

        // Ensure user input is valid
        // Used when converting to a number or character
        static bool checkInput(string value)
            {
                bool success = false;

                int number;
                success = Int32.TryParse(value, out number);

                return success;
            }

        // Higher/Lower
        static bool HigherLower() 
        {
            Console.Clear();

            

            List<int> numberRange = difficultySelection();
            Random random = new Random();
            int minimum = numberRange[0];
            int maximum = numberRange[1];

            int winningNumber = random.Next(minimum, maximum);
            int guess = 0;
            int totalGuesses = 0;

            string error = "";
            string result = "";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("HigherLower\n\n");
            Console.ResetColor();
            Console.WriteLine("Guess the number using the hints provided.\nThe number is between 1 and 100.\n");

            while (winningNumber != guess)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\n{error}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(result);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Make a guess: ");
                string input = Console.ReadLine();
                Console.ResetColor();

                if (!checkInput(input))
                {
                    error = "Invalid guess. Please guess a number.\n";
                    continue;
                }
                else
                {
                    guess = Convert.ToInt32(input);
                }

                if (winningNumber > guess)
                {
                    result = "Higher!\n";
                }
                else if (winningNumber < guess)
                {
                    result = "Lower!\n";
                }

                totalGuesses += 1;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nCorrect! The winning number was {winningNumber}\n");
            Console.WriteLine($"You guessed it in {totalGuesses} guesses!\n");
            Console.ResetColor();
            Console.Write("Press enter to continue...");
            Console.ReadLine();

            return false;
        }

        // Higher/Lower Difficulty Selection
        static List<int> difficultySelection()
        {
            string minimum;
            string maximum;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Difficulty Selection");
            Console.ResetColor();

            Console.WriteLine("Select the range of values you would like the winning number to be selected from.\n");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Minimum: ");
                minimum = Console.ReadLine();
                Console.Write("Maximum: ");
                maximum = Console.ReadLine();
                Console.ResetColor();

                if (!checkInput(minimum) || !checkInput(maximum))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please provide numbers only.\n");
                    Console.ResetColor();
                }
                break;
            }

            List<int> range = new List<int> { Convert.ToInt32(minimum), Convert.ToInt32(maximum) };
            
            Console.Clear();
            return range;
        }

        // Heads/Tails
        static bool HeadsTails()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("HeadsTails\n\n");
            Console.ResetColor();

            Console.WriteLine("Guess the result of the coin flip.\n");

            int roundsNeeded = 1;
            int roundsPlayed = 0;
            int roundsWon = 0;
            string error = "";
            string result = "";
            Random random = new Random();

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("You can select the number of rounds you must win consecutively.\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(error);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Enter the number of rounds: ");
                string input = Console.ReadLine();
                Console.ResetColor();

                if (!checkInput(input))
                {
                    error = "Invalid input. Please enter a number.\n";
                }

                roundsNeeded = Convert.ToInt32(input);
                break;
            }

            while (roundsWon < roundsNeeded)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("HeadsTails\n\n");
                Console.ResetColor();

                int flipResult = random.Next(2);
                string coinFlip = flipResult == 0 ? "heads" : "tails";

                Console.WriteLine($"Rounds Played: {roundsPlayed}\nRounds Won: {roundsWon}\nRounds Needed: {roundsNeeded}\n\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(error);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(result);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[H]eads/[T]ails: ");
                string guess = Console.ReadLine().ToLower();
                Console.ResetColor();

                if (guess == coinFlip || guess == coinFlip[0].ToString())
                {
                    roundsWon += 1;
                    result = $"Correct! The Coin landed on {coinFlip}\n";
                }
                else
                {
                    roundsWon = 0;
                    result = $"Incorrect! The Coin landed on {coinFlip}\n";
                }
                roundsPlayed += 1;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nYou won!");
            Console.Write("Press enter to continue...");
            Console.ReadLine();
            Console.ResetColor();

            return false;
            }
    
        static bool Quit()
        {
            Console.WriteLine("\nThank you for playing!");
            return true;
        }
    }
}
