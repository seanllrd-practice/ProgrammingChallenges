using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            while (!quit)
            {
                string error = "";
                decimal temperature;
                char scale1;
                char scale2;

                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Temperature Converter\n");
                    Console.ResetColor();

                    Console.WriteLine("Provide the temperature, degree scale being used, and degree scale desired.");
                    Console.WriteLine("Example: 70 F C means convert 70 degrees Fahrenheit to Celsius.");
                    Console.WriteLine("Use F for Fahrenheit, C for Celsius, and K for Kelvin.\n\n");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(error);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Enter temperature information: ");
                    string providedConversion = Console.ReadLine();
                    Console.ResetColor();

                    List<string> information = providedConversion.Split().ToList();

                    bool canConvertTemp = checkInput(information[0], typeof(decimal));
                    bool canConvertScale1 = checkInput(information[1], typeof(char));
                    bool canConvertScale2 = checkInput(information[2], typeof(char));

                    if (!canConvertTemp)
                    {
                        Console.WriteLine("Invalid Temperature. Temperature should be an integer.\n");
                        continue;
                    }
                    else if (!canConvertScale1)
                    {
                        Console.WriteLine("Invalid Starting Scale. Starting Scale should be F, C, or K.\n");
                        continue;
                    }
                    else if (!canConvertScale2)
                    {
                        Console.WriteLine("Invalid Conversion Scale. Conversion Scale should be F, C, or K.\n");
                        continue;
                    }

                    temperature = Convert.ToDecimal(information[0]);
                    scale1 = Convert.ToChar(information[1].ToUpper());
                    scale2 = Convert.ToChar(information[2].ToUpper());
                    break;
                }

                decimal newTemperature = convertTemp(temperature, scale1, scale2);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Provided Temperature:{temperature} {scale1}\nConverted Temperature: {newTemperature} {scale2}\n");
                
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Continue [y/n]?: ");
                string input = Console.ReadLine();

                if (input == "n" || input == "no")
                {
                    Console.Write("Press enter to quit...");
                    quit = true;
                }
                else
                {
                    Console.Write("Press enter to continue...");
                }
                Console.ReadLine();
                Console.ResetColor();
            }
            Environment.Exit(0);
        }

        static bool checkInput(string input, Type type)
        {
            bool result = false;

            if (type == typeof(decimal))
            {
                decimal number;
                result = decimal.TryParse(input, out number);
            }
            else if (type == typeof(char))
            {
                List<char> acceptedScales = new List<char> { 'F', 'C', 'K' };
                Char ch;
                result = Char.TryParse(input.ToUpper(), out ch);
                if (result && !acceptedScales.Contains(ch))
                {
                        result = false;
                }
            }
            return result;
        }

        static decimal convertTemp(decimal temp, char firstScale, char secondScale)
        {
            string scales = String.Concat(firstScale, secondScale);
            decimal convertedTemp;

            switch (scales)
            {
                case "FC":
                    convertedTemp = (5m/9m) * (temp - 32m);
                    break;
                case "FK":
                    convertedTemp = (5m/9m) * (temp - 32) + 273.15m;
                    break;
                case "CF":
                    convertedTemp = ((9m/5m) * temp) + 32m;
                    break;
                case "CK":
                    convertedTemp = temp + 273.15m;
                    break;
                case "KF":
                    convertedTemp = ((9m/5m) * (temp - 273.15m)) + 32m;
                    break;
                case "KC":
                    convertedTemp = temp - 273.15m;
                    break;
                default:
                    convertedTemp = temp;
                    break;
            }
            convertedTemp = Math.Round(convertedTemp, 2);
            return convertedTemp;
        }
    }
}
