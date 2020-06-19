using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Challenges
{
    public class EncryptionDecryption
    {
        private static void Main()
        {
            Menu();

            Console.Clear();
            Console.WriteLine("Thank you for using my program!");
            Console.Write("Press enter to quit...");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public static void Menu()
        {
            bool quit = false;
            List<string> menuOptions = new List<string> { "Encrypt Message", "Decrypt Message", "Quit" };
            string error = "";

            while (!quit)
            {
                Console.Clear();
                int optionNumber = GetOption(menuOptions, error);

                List<object> result = RunOption(optionNumber);
                error = result[0].ToString();
                quit = Convert.ToBoolean(result[1]);
            }

            Console.WriteLine("\nThank you for playing!");
        }

        private static int GetOption(List<string> options, string error)
        {
            int optionNumber;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Encrypt/Decrypt Algorithm\n\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Please select an option:\n");

                foreach (var option in options.Select((Value, Index) => new { Value, Index }))
                {
                    Console.WriteLine($"{option.Index + 1} {option.Value}");
                };

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{error}");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("> ");
                string input = Console.ReadLine();
                Console.ResetColor();

                if (!Int32.TryParse(input, out optionNumber))
                {
                    error = "Invalid input. Please enter the option number you wish to choose.";
                    continue;
                }
                break;
            }

            return optionNumber;
        }

        private static List<object> RunOption(int option)
        {
            string plaintext;
            string ciphertext;
            string message;
            string error = "";
            int cipherkey;
            bool quit;

            List<object> encryptResult;
            string decryptResult;

            switch (option)
            {
                case 1:
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Encrypt Message\n");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("\nEnter message to encrypt\n> ");

                    plaintext = Console.ReadLine();
                    encryptResult = Encrypt(plaintext);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nEncrypted Message: {encryptResult[0]}\nCipher Key: {encryptResult[1]}\n");
                    Console.ResetColor();

                    Console.Write("Press enter to continue...");
                    Console.ReadLine();

                    quit = false;
                    break;
                case 2:
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Decrpyt Message\n");

                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nEnter message to decrypt and the cipher key\nExample: Bla Bla 5\n");

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(error);

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("> ");

                        message = Console.ReadLine();
                        ciphertext = GetCiphertext(message);
                        cipherkey = GetCipherkey(message);

                        if (cipherkey == 0)
                        {
                            error = "Invalid input. Make sure your message includes the cipher key at the end.";
                            continue;
                        }
                        Console.ResetColor();
                        break;
                    }

                    decryptResult = Decrypt(ciphertext, cipherkey);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nDecrypted Message: {decryptResult}\n");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();

                    quit = false;
                    break;
                case 3:
                    quit = true;
                    break;
                default:
                    error = "Invalid option number.";
                    quit = false;
                    break;
            }
            List<object> result = new List<object> { error, quit };

            return result;
        }

        private static List<object> Encrypt(string plaintext)
        {
            Cipher cipher = new Cipher();

            string ciphertext = "";
            foreach (char letter in plaintext.ToUpper())
            {
                char cipherLetter;
                if (cipher.Alphabet.Contains(letter))
                {
                    int plainIndex = cipher.Alphabet.IndexOf(letter);
                    cipherLetter = cipher.Cipherbet[plainIndex];
                }
                else
                {
                    cipherLetter = letter;
                }
                ciphertext += cipherLetter.ToString();
            }

            List<object> result = new List<object> { ciphertext, cipher.Cipherkey };

            return result;
        }
    
        private static string Decrypt(string ciphertext, int cipherkey)
        {
            string result = "";

            Cipher decypher = new Cipher(cipherkey);

            foreach (char letter in ciphertext.ToUpper())
            {
                char plainLetter;
                if (decypher.Cipherbet.Contains(letter))
                {
                    int cipherIndex = decypher.Cipherbet.IndexOf(letter);
                    plainLetter = decypher.Alphabet[cipherIndex];
                }
                else
                {
                    plainLetter = letter;
                }
                result += plainLetter.ToString();
            }
            
            return result;
        }

        private static string GetCiphertext(string input)
        {
            string ciphertext = input.Substring(0, input.Length - 2);
            return ciphertext;
        }

        private static int GetCipherkey(string input)
        {
            int cipherkey;
            string[] splitinput = input.Split(' ');
            string testkey = splitinput[splitinput.Length - 1];

            if (!Int32.TryParse(testkey, out cipherkey))
            {
                cipherkey = 0;
            }
            return cipherkey;
        }
    }

    class Cipher
    {
        public List<char> Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
        public List<char> Cipherbet;
        public int Cipherkey;
        
        public Cipher()
        {
            Random random = new Random();
            Cipherkey = random.Next(0, 25);

            GenerateCiphertext();
        }

        public Cipher(int cipherkey) 
        {
            Cipherkey = cipherkey;

            GenerateCiphertext();
        }

        private void GenerateCiphertext()
        {
            Cipherbet = Alphabet.GetRange(Cipherkey, Alphabet.Count - Cipherkey);
            Cipherbet.AddRange(Alphabet.GetRange(0, Cipherkey));
        }
    }
}
