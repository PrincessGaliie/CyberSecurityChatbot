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
            PrintWithColor("=============================", ConsoleColor.Cyan);
            PrintWithColor("   _____  __  __ ____  _____ ", ConsoleColor.Cyan);
            PrintWithColor("  / ____||  \\/  |  _ \\|  __ \\", ConsoleColor.Cyan);
            PrintWithColor(" | |     | \\  / | |_) | |  | |", ConsoleColor.Cyan);
            PrintWithColor(" | |     | |\\/| |  _ <| |  | |", ConsoleColor.Cyan);
            PrintWithColor(" | |____ | |  | | |_) | |__| |", ConsoleColor.Cyan);
            PrintWithColor("  \\_____|_|  |_|____/|_____/ ", ConsoleColor.Cyan);
            PrintWithColor("=============================", ConsoleColor.Cyan);
            PrintWithColor("Welcome to CYBER AWARE CHATBOT!", ConsoleColor.Green);

            // Play name prompt sound synchronously
            try
            {
                using (SoundPlayer player = new SoundPlayer("Name.wav"))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                PrintWithColor("[Warning] Could not play the name prompt sound: " + ex.Message, ConsoleColor.Yellow);
            }

            // Prompt for user's name
            string userName;
            do
            {   
                PrintWithColor($"HI MY NAME IS CHATTY !", ConsoleColor.Green);
                PrintWithColor("Please enter your name: ", ConsoleColor.White);
                userName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userName))
                {
                    PrintWithColor("Name cannot be empty. Please try again.", ConsoleColor.Red);
                }
            } while (string.IsNullOrWhiteSpace(userName));

            // Play greeting sound synchronously
            try
            {
                using (SoundPlayer player = new SoundPlayer("greeting.wav"))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                PrintWithColor("[Warning] Could not play the greeting sound: " + ex.Message, ConsoleColor.Yellow);
            }

            // Personalized text greeting
            PrintWithColor($"Hello, {userName}! Stay vigilant and keep your data safe.", ConsoleColor.Green);
            PrintWithColor("Type your question about cybersecurity or type 'exit' to quit.", ConsoleColor.White);

            // Load responses from dataset file
            Dictionary<string, string> responses = LoadResponses("Dataset.txt");

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

                string response = GetResponse(userInput, responses);
                PrintWithTypingEffect("Bot: " + response, ConsoleColor.Cyan);
            }
        }

        static Dictionary<string, string> LoadResponses(string filePath)
        {
            Dictionary<string, string> responses = new Dictionary<string, string>();

            try
            {
                foreach (string line in File.ReadAllLines(filePath))
                {
                    if (line.Contains("|"))
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            responses.Add(parts[0].Trim().ToLower(), parts[1].Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PrintWithColor("[Error] Unable to load responses: " + ex.Message, ConsoleColor.Red);
            }

            return responses;
        }

        static string GetResponse(string input, Dictionary<string, string> responses)
        {
            foreach (var pair in responses)
            {
                if (input.Contains(pair.Key))
                {
                    return pair.Value;
                }
            }

            string availableQueries = string.Join(", ", responses.Keys);
            return $"I can't provide details on that, but what I can help you with is: {availableQueries}";
        }

        static void PrintWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void PrintWithTypingEffect(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(50); // Adjust the delay to simulate typing effect
            }
            Console.WriteLine();
            Console.ResetColor();
            
        }
    }
}


