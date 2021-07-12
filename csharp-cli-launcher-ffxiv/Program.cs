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
        private static readonly string UserAgentTemplate = "SQEXAuthor/2.0.0(Windows 6.2; ja-jp; {0})";

        private static readonly string UserAgent = GenerateUserAgent();

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
            Console.WriteLine("FFXIV Launcher "); // it has to begin somewhere lol
            
            int language = int.Parse(Console.ReadLine());

            if ( language == 0 ) {
             Console.WriteLine("-------------------------------------");
             Console.WriteLine("何をしたいですか?");
             Console.WriteLine("  1) ログイン");
             Console.WriteLine("  2) 出口");

             Console.Write("入力 - ");
             var ansys = Console.ReadKey();
             Console.WriteLine();
             Console.WriteLine("-------------------------------------");
            
             if (ansys.KeyChar == '1')
             {
                //Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                Console.Write("ゲームパスを入力してください - ");
                string gamePath = Console.ReadLine();
                Console.WriteLine("-------------------------------------");
                bool isSteam = false;
                Console.Write("あなたのゲームはクライアントのSteamバージョンですか? - ");
                string promtw = Console.ReadLine();
                if (promtw.ToLower() == "yes")
                {
                    isSteam = true;
                }
                else
                {
                    isSteam = false;
                }
                Console.WriteLine("-------------------------------------");
                Console.Write("ユーザーID - ");
                string username = Console.ReadLine();
                //Console.WriteLine("Provided username {0}", username);
                Console.Write("パスワード - ");
                string password = ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("2要素認証キ - ");
                string otp = Console.ReadLine();
                Console.WriteLine("拡張パックのレベルを入力してください-現在有効なものは \n 0-ARR-1-ヘブンスワード-2-ストームブラッド-3-シャドウブリンガー");
                int expansionLevel = int.Parse(Console.ReadLine());

                try
                {
                    var sid = GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = LaunchGame(gamePath, sid, language, true, expansionLevel, isSteam);



                }
                catch (Exception exc)
                {
                    Console.WriteLine("ログインに失敗しました。ログイン情報を確認するか、再試行してください.\n" + exc.Message);
                }
                Console.ReadLine();
             }
             else {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("ランチャーを終了する");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
             }    
            }

            if ( language == 1 ) {
             Console.WriteLine("-------------------------------------");
             Console.WriteLine("What would you like to do?");
             Console.WriteLine("  1) Login");
             Console.WriteLine("  2) Exit");

             Console.Write("Input - ");
             var ansys = Console.ReadKey();
             Console.WriteLine();
             Console.WriteLine("-------------------------------------");
            
             if (ansys.KeyChar == '1')
             {
                //Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                Console.Write("Please enter your gamepath - ");
                string gamePath = Console.ReadLine();
                Console.WriteLine("-------------------------------------");
                bool isSteam = false;
                Console.Write("Is your game a steam version of the client? - ");
                string promtw = Console.ReadLine();
                if (promtw.ToLower() == "yes")
                {
                    isSteam = true;
                }
                else
                {
                    isSteam = false;
                }
                Console.WriteLine("-------------------------------------");
                Console.Write("Username - ");
                string username = Console.ReadLine();
                //Console.WriteLine("Provided username {0}", username);
                Console.Write("Password - ");
                string password = ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Two-Factor Authefication Key - ");
                string otp = Console.ReadLine();
                Console.WriteLine("Please enter your expansion pack level - Currently valid ones are \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                int expansionLevel = int.Parse(Console.ReadLine());

                try
                {
                    var sid = GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = LaunchGame(gamePath, sid, language, true, expansionLevel, isSteam);



                }
                catch (Exception exc)
                {
                    Console.WriteLine("Logging in failed, check your login information or try again.\n" + exc.Message);
                }
                Console.ReadLine();
             }
             else {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Exiting the launcher");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
             }    
            }

            if ( language == 2 ) {

            }

            if ( language == 3 ) {

            }

            

        }

        private static string GetLocalGamever(string gamePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(gamePath + @"/game/ffxivgame.ver"))
                {
                    string line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Unable to get local game version.\n" + exc);
                return "BAD";
            }
        }

        private static string GenerateHash(string file)
        {
            byte[] filebytes = File.ReadAllBytes(file);

            var hash = (new SHA1Managed()).ComputeHash(filebytes);
            string hashstring = string.Join("", hash.Select(b => b.ToString("x2")).ToArray());

            long length = new FileInfo(file).Length;

            return length + "/" + hashstring;
        }

        public static bool GetGateStatus()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string reply = client.DownloadString("http://frontier.ffxiv.com/worldStatus/gate_status.json");

                    return Convert.ToBoolean(int.Parse(reply[10].ToString()));
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Failed getting gate status. " + exc);
                return false;
            }

        }

        private static void InitiateSslTrust()
        {
            //Change SSL checks so that all checks pass, squares gamever server does strange things
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(
                    delegate
                    { return true; }
                );
        }


        private static string GenerateUserAgent()
        {
            return string.Format(UserAgentTemplate, MakeComputerId());
        }

        private static string MakeComputerId()
        {
            var hashString = Environment.MachineName + Environment.UserName + Environment.OSVersion +
                             Environment.ProcessorCount;

            using (var sha1 = HashAlgorithm.Create("SHA1"))
            {
                var bytes = new byte[5];

                Array.Copy(sha1.ComputeHash(Encoding.Unicode.GetBytes(hashString)), 0, bytes, 1, 4);

                var checkSum = (byte)-(bytes[1] + bytes[2] + bytes[3] + bytes[4]);
                bytes[0] = checkSum;

                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
