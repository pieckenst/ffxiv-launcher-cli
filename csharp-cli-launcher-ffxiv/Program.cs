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
            foreach(string line in arr ) {
                Console.WriteLine(line);
            }
            //Console.WriteLine("FFXIV Launcher "); // it has to begin somewhere lol
            
            
            
            Console.WriteLine("0 - Japanese , 1 - English , 2 - German , 3 - French , 4 - Russian ( The client will still be in english)");
            
            Console.Write("Enter your language - ");
            
            int language = int.Parse(Console.ReadLine());

            if ( language == 0 ) {
             Console.WriteLine("-------------------------------------");
             Console.WriteLine("何をしたいですか? ");
             Console.WriteLine("  1) ログイン ");
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
             Console.WriteLine("-------------------------------------");
             Console.WriteLine("Was würdest du gern tun?");
             Console.WriteLine("  1) Anmeldung");
             Console.WriteLine("  2) Ausgang");

             Console.Write("Eingang - ");
             var ansys = Console.ReadKey();
             Console.WriteLine();
             Console.WriteLine("-------------------------------------");
            
             if (ansys.KeyChar == '1')
             {
                //Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                Console.Write("Bitte geben Sie Ihren Spielpfad ein - ");
                string gamePath = Console.ReadLine();
                Console.WriteLine("-------------------------------------");
                bool isSteam = false;
                Console.Write("Ist Ihr Spiel eine Steam-Version des Clients? - ");
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
                Console.Write("Nutzername - ");
                string username = Console.ReadLine();
                //Console.WriteLine("Provided username {0}", username);
                Console.Write("Passwort - ");
                string password = ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Zwei-Faktor-Authentifizierungsschlüssel - ");
                string otp = Console.ReadLine();
                Console.WriteLine("Bitte geben Sie Ihr Erweiterungspaket-Level ein - Derzeit gültige sind \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
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
                    Console.WriteLine("Die Anmeldung ist fehlgeschlagen, überprüfen Sie Ihre Anmeldeinformationen oder versuchen Sie es erneut. \n" + exc.Message);
                }
                Console.ReadLine();
             }
             else {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Beenden des Launchers");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
             }    
            }

            if ( language == 3 ) {
             Console.WriteLine("-------------------------------------");
             Console.WriteLine("Qu'est-ce que tu aimerais faire?");
             Console.WriteLine("  1) Connexion");
             Console.WriteLine("  2) Sortir");

             Console.Write("Entrée - ");
             var ansys = Console.ReadKey();
             Console.WriteLine();
             Console.WriteLine("-------------------------------------");
            
             if (ansys.KeyChar == '1')
             {
                //Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                Console.Write("Veuillez entrer votre chemin de jeu - ");
                string gamePath = Console.ReadLine();
                Console.WriteLine("-------------------------------------");
                bool isSteam = false;
                Console.Write("Votre jeu est-il une version Steam du client? - ");
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
                Console.Write("Nom d'utilisateur - ");
                string username = Console.ReadLine();
                //Console.WriteLine("Provided username {0}", username);
                Console.Write("Mot de passe - ");
                string password = ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Clé d'authentification à deux facteurs - ");
                string otp = Console.ReadLine();
                Console.WriteLine("Veuillez saisir le niveau de votre pack d'extension - Les versions actuellement valides sont \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
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
                    Console.WriteLine("Échec de la connexion, vérifiez vos informations de connexion ou réessayez.\n" + exc.Message);
                }
                Console.ReadLine();
             }
             else {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Quitter le lanceur");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
             }    
            }
            if (language == 4)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Что бы вы хотели сделать?");
                Console.WriteLine("  1) Вход в игру");
                Console.WriteLine("  2) Выйти из лаунчера");

                Console.Write("Ввод - ");
                var ansys = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine("-------------------------------------");

                if (ansys.KeyChar == '1')
                {
                    //Console.WriteLine("-------------------------------------");
                    Console.WriteLine();
                    Console.Write("Введите путь до клиента игры - ");
                    string gamePath = Console.ReadLine();
                    Console.WriteLine("-------------------------------------");
                    bool isSteam = false;
                    Console.Write("Является ли ваш клиент версией клиента для Steam? - ");
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
                    Console.Write("Имя пользователя - ");
                    string username = Console.ReadLine();
                    //Console.WriteLine("Provided username {0}", username);
                    Console.Write("Пароль - ");
                    string password = ReadPassword();
                    //string maskpassword = "";
                    //for (int i = 0; i < password.Length; i++) { 
                    //maskpassword += "*"; 
                    //}


                    //Console.Write("Your Password is:" + maskpassword);
                    Console.WriteLine();

                    Console.Write("Код Двух-Факторной аутентификации - ");
                    string otp = Console.ReadLine();
                    Console.WriteLine("Пожалуйста, введите уровень доступного вам дополнения - на текущий момент валидными являются следущие \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                    int expansionLevel = int.Parse(Console.ReadLine());

                    try
                    {
                        var sid = GetRealSid(gamePath, username, password, otp, isSteam);
                        if (sid.Equals("BAD"))
                            return;

                        var ffxivGame = LaunchGame(gamePath, sid, 1, true, expansionLevel, isSteam);



                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Не удалось войти в систему, проверьте данные для входа или попробуйте еще раз .\n" + exc.Message);
                    }
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Выходим из лаунчера");
                    Console.WriteLine("-------------------------------------");
                    Console.ReadLine();
                }
            }

            

        }

        public static Process LaunchGame(string gamePath, string realsid, int language, bool dx11, int expansionlevel, bool isSteam)
        {
            try
            {
                Process ffxivgame = new Process();
                ffxivgame.StartInfo.FileName = gamePath + "/game/ffxiv_dx11.exe";
                ffxivgame.StartInfo.Arguments = $"DEV.TestSID={realsid} DEV.MaxEntitledExpansionID={expansionlevel} language={language} region=3";
                if (isSteam)
                {
                    ffxivgame.StartInfo.Environment.Add("IS_FFXIV_LAUNCH_FROM_STEAM", "1");
                    ffxivgame.StartInfo.Arguments += " IsSteam=1";
                    ffxivgame.StartInfo.UseShellExecute = false;
                }
                ffxivgame.Start();
                return ffxivgame;
            }
            catch (Exception exc)
            {
                if (language == 0)
                {
                    Console.WriteLine("実行可能ファイルを起動できませんでした。 ゲームパスは正しいですか? " + exc);
                }
                if (language == 1)
                {
                    Console.WriteLine("Could not launch executable. Is your game path correct? " + exc);
                }
                if (language == 2)
                {
                    Console.WriteLine("Die ausführbare Datei konnte nicht gestartet werden. Ist dein Spielpfad korrekt? " + exc);
                }
                if (language == 3)
                {
                    Console.WriteLine("Impossible de lancer l'exécutable. Votre chemin de jeu est-il correct? " + exc);
                }
                if (language == 4)
                {
                    Console.WriteLine("Не удалось запустить файл. Ввели ли вы корректный путь к игре? " + exc);
                }

            }

            return null;
        }

        public static string GetRealSid(string gamePath, string username, string password, string otp, bool isSteam)
        {
            string hashstr = "";
            try
            {
                // make the string of hashed files to prove game version//make the string of hashed files to prove game version
                hashstr = "ffxivboot.exe/" + GenerateHash(gamePath + "/boot/ffxivboot.exe") +
                          ",ffxivboot64.exe/" + GenerateHash(gamePath + "/boot/ffxivboot64.exe") +
                          ",ffxivlauncher.exe/" + GenerateHash(gamePath + "/boot/ffxivlauncher.exe") +
                          ",ffxivlauncher64.exe/" + GenerateHash(gamePath + "/boot/ffxivlauncher64.exe") +
                          ",ffxivupdater.exe/" + GenerateHash(gamePath + "/boot/ffxivupdater.exe") +
                          ",ffxivupdater64.exe/" + GenerateHash(gamePath + "/boot/ffxivupdater64.exe");
            }
            catch (Exception exc)
            {
                Console.WriteLine("Could not generate hashes. Is your game path correct? " + exc);
            }

            WebClient sidClient = new WebClient();
            sidClient.Headers.Add("X-Hash-Check", "enabled");
            sidClient.Headers.Add("user-agent", UserAgent);
            sidClient.Headers.Add("Referer", "https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn=3");
            sidClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            InitiateSslTrust();

            try
            {
                var localGameVer = GetLocalGamever(gamePath);
                var localSid = GetSid(username, password, otp, isSteam);

                if (localGameVer.Equals("BAD") || localSid.Equals("BAD"))
                {
                    return "BAD";
                }

                var url = "https://patch-gamever.ffxiv.com/http/win32/ffxivneo_release_game/" + localGameVer + "/" + localSid;
                sidClient.UploadString(url, hashstr); //request real session id
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Unable to retrieve a session ID from the server.\n" + exc);
            }

            return sidClient.ResponseHeaders["X-Patch-Unique-Id"];
        }

        private static string GetStored(bool isSteam) //this is needed to be able to access the login site correctly
        {
            WebClient loginInfo = new WebClient();
            loginInfo.Headers.Add("user-agent", UserAgent);
            string reply = loginInfo.DownloadString(string.Format("https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn=3&isft=0&issteam={0}", isSteam ? 1 : 0));

            Regex storedre = new Regex(@"\t<\s*input .* name=""_STORED_"" value=""(?<stored>.*)"">");

            var stored = storedre.Matches(reply)[0].Groups["stored"].Value;
            return stored;
        }

        public static string GetSid(string username, string password, string otp, bool isSteam)
        {
            using (WebClient loginData = new WebClient())
            {
                loginData.Headers.Add("user-agent", UserAgent);
                loginData.Headers.Add("Referer", string.Format("https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn=3&isft=0&issteam={0}", isSteam ? 1 : 0));
                loginData.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                try
                {
                    byte[] response =
                        loginData.UploadValues("https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/login.send", new NameValueCollection() //get the session id with user credentials
                        {
                            { "_STORED_", GetStored(isSteam) },
                            { "sqexid", username },
                            { "password", password },
                            { "otppw", otp }
                        });

                    string reply = System.Text.Encoding.UTF8.GetString(response);
                    //Console.WriteLine(reply);
                    Regex sidre = new Regex(@"sid,(?<sid>.*),terms");
                    var matches = sidre.Matches(reply);
                    if (matches.Count == 0)
                    {
                        if (reply.Contains("ID or password is incorrect"))
                        {
                            Console.WriteLine("Incorrect username or password.");
                            return "BAD";
                        }
                    }

                    var sid = sidre.Matches(reply)[0].Groups["sid"].Value;
                    return sid;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Something failed when attempting to request a session ID.\n" + exc);
                    return "BAD";
                }
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
