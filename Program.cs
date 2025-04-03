using System;
using System.Collections.Generic;
using System.Media;
using System.IO;
using System.Threading;

namespace CyberAwareChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            // ASCII Art Logo
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"                   ____      _          _                 ");
            Console.WriteLine(@"                  / ___|   _| |__   ___| |_ __ _ ___ _ __ ");
            Console.WriteLine(@"                 | |  | | | | '_ \ / _ \ __/ _` / _ \ '__|");
            Console.WriteLine(@"                 | |__| |_| | |_) |  __/ || (_| |  __/ |   ");
            Console.WriteLine(@"                  \____\__, |_.__/ \___|\__\__, |\___|_|   ");
            Console.WriteLine(@"                       |___/               |___/           ");
            Console.WriteLine("Cybersecurity Awareness Bot");
            Console.ResetColor();

            PrintWithColor("Welcome to CYBER AWARE CHATBOT!", ConsoleColor.Green);

            try
            {
                using (SoundPlayer player = new SoundPlayer("Name.wav"))
                {
                    player.PlaySync();
                }
            }
            catch
            {
                PrintWithColor("(Sound effect unavailable.)", ConsoleColor.Yellow);
            }

            string userName;
            do
            {
                PrintWithColor("HI MY NAME IS CHATTY!", ConsoleColor.Green);
                PrintWithColor("Please enter your name: ", ConsoleColor.White);
                userName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userName))
                {
                    PrintWithColor("Name cannot be empty. Please try again.", ConsoleColor.Red);
                }
            } while (string.IsNullOrWhiteSpace(userName));

            try
            {
                using (SoundPlayer player = new SoundPlayer("greeting.wav"))
                {
                    player.PlaySync();
                }
            }
            catch
            {
                PrintWithColor("(Sound effect unavailable.)", ConsoleColor.Yellow);
            }

            PrintWithColor($"Hello, {userName}! Stay vigilant and keep your data safe.", ConsoleColor.Green);
            PrintWithColor("Type your question about cybersecurity or type 'exit' to quit.", ConsoleColor.White);

            Dictionary<string, List<string>> responses = new ChatBot().responses;
            Random rnd = new Random();

            while (true)
            {
                PrintWithColor("You: ", ConsoleColor.White);
                string userInput = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    PrintWithColor("Bot: I didn’t quite get that. Could you rephrase that, please?", ConsoleColor.Yellow);
                    continue;
                }

                if (userInput == "exit")
                {
                    PrintWithColor("Goodbye, stay cyber safe!", ConsoleColor.Green);
                    break;
                }

                string response = "I can't provide details on that. Try asking about passwords, phishing, or two-factor authentication.";
                foreach (var pair in responses)
                {
                    if (userInput.Contains(pair.Key))
                    {
                        response = pair.Value[rnd.Next(pair.Value.Count)];
                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (char c in "Bot: " + response)
                {
                    Console.Write(c);
                    Thread.Sleep(30);
                }
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        static void PrintWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

