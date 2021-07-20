using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace csharp_cli_launcher_ffxiv
{
    class Program
    {


        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }

        static void Main(string[] args)
        {
            Console.Title = "XIVLOADER";
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var arr = new[]
            {
                @"                                             ",
                @" __  _______   ___                 _         ",
                @" \ \/ /_ _\ \ / / |   ___  __ _ __| |___ _ _ ",
                @"  >  < | | \ V /| |__/ _ \/ _` / _` / -_) '_| ",
                @" /_/\_\___| \_/ |____\___/\__,_\__,_\___|_|  ",
                @"                                             ",
            };
            Console.WindowWidth = 160;
            Console.WriteLine("\n\n");
            foreach (string line in arr)
            {
                Console.WriteLine(line);
            }
            //Console.WriteLine("FFXIV Launcher "); // it has to begin somewhere lol



            Console.WriteLine("0 - Japanese , 1 - English , 2 - German , 3 - French , 4 - Russian ( The client will still be in english)");

            Console.Write("Enter your language - ");

            int language = int.Parse(Console.ReadLine());

            if (language == 0)
            {
                JapaneseLaunchMethod.JapanLaunch(language);
            }

            if (language == 1)
            {
                LaunchMethods.EnglishLaunch(language);
            }

            if (language == 2)
            {
                GermanLaunchMethod.GermanLaunch(language);
            }

            if (language == 3)
            {
                FrenchLaunchMethod.FrenchLaunch(language);
            }
            if (language == 4)
            {
                RussianLaunchMethod.RussianLaunch(language);
            }



        }





    }   
}
