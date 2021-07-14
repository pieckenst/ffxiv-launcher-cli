using System;
using static networklogic;
using csharp_cli_launcher_ffxiv;
using System.IO;

/// <summary>
/// Basically a class for launch sequence of the launcher
/// </summary>
public class LaunchMethods
{
	
        public static void JapanLaunch(int language)
        {
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
                string gamePath;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\gamepath.txt")) {
                  TextReader tr = new StreamReader("gamepath.txt");
                  string gamePathread = tr.ReadLine();
                  gamePath = gamePathread;
                  tr.Close();
                  Console.WriteLine(gamePath);
                }
                else
			    {
                  Console.Write("ゲームパスを入力してください - ");
                  gamePath = Console.ReadLine();
                  TextWriter tw = new StreamWriter("gamepath.txt");
                  tw.WriteLine(gamePath);
                  tw.Close();
			    }
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
                string password = Program.ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("2要素認証キ - ");
                string otp = Console.ReadLine();
                bool dx11 = false;
                Console.Write("DirectX 11を有効にしてゲームを起動しますか？ - ");
                string dx1prompt = Console.ReadLine();
                if (dx1prompt.ToLower() == "yes")
                {
                    dx11 = true;
                }
                else
			    {
                    dx11 = false; 
			    }
                Console.WriteLine("拡張パックのレベルを入力してください-現在有効なものは \n 0-ARR-1-ヘブンスワード-2-ストームブラッド-3-シャドウブリンガー");
                int expansionLevel = int.Parse(Console.ReadLine());
                Console.Write("クライアントインストール用のリージョンを指定してください-現在有効なリージョンは次のとおりです。 \n 1  - 日本、2  - アメリカ、3  - 国際: - ");
                int region = int.Parse(Console.ReadLine());
                try
                {
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, dx11, expansionLevel, isSteam , region);



                }
                catch (Exception exc)
                {
                    Console.WriteLine("ログインに失敗しました。ログイン情報を確認するか、再試行してください.\n" + exc.Message);
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("ランチャーを終了する");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
            }
        }

        public static void EnglishLaunch(int language)
        {
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
                string gamePath;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\gamepath.txt")) {
                  TextReader tr = new StreamReader("gamepath.txt");
                  string gamePathread = tr.ReadLine();
                  gamePath = gamePathread;
                  tr.Close();
                  Console.WriteLine(gamePath);
                }
                else
			    {
                  Console.Write("Please enter your gamepath - ");
                  gamePath = Console.ReadLine();
                  TextWriter tw = new StreamWriter("gamepath.txt");
                  tw.WriteLine(gamePath);
                  tw.Close();
			    }
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
                string password = Program.ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Two-Factor Authefication Key - ");
                string otp = Console.ReadLine();
                bool dx11 = true;
                Console.Write("Do you want to launch the game with enabled DirectX 11? - ");
                string dx1prompt = Console.ReadLine();
                if (dx1prompt.ToLower() == "yes")
                {
                    dx11 = true;
                }
                else
			    {
                    dx11 = false; 
			    }
                Console.WriteLine("Please enter your expansion pack level - Currently valid ones are \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                int expansionLevel = int.Parse(Console.ReadLine());
                Console.Write("Please provide a region for your client install - Currently valid ones are \n 1- Japan , 2 - America , 3 - International: - ");
                int region = int.Parse(Console.ReadLine());
                try
                {
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, dx11, expansionLevel, isSteam , region);



                }
                catch (Exception exc)
                {
                    Console.WriteLine("Logging in failed, check your login information or try again.\n" + exc.Message);
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Exiting the launcher");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
            }
        }

        public static void GermanLaunch(int language)
        {
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
                string gamePath;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\gamepath.txt")) {
                  TextReader tr = new StreamReader("gamepath.txt");
                  string gamePathread = tr.ReadLine();
                  gamePath = gamePathread;
                  tr.Close();
                  Console.WriteLine(gamePath);
                }
                else
			    {
                  Console.Write("Bitte geben Sie Ihren Spielpfad ein - ");
                  gamePath = Console.ReadLine();
                  TextWriter tw = new StreamWriter("gamepath.txt");
                  tw.WriteLine(gamePath);
                  tw.Close();
			    }
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
                string password = Program.ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Zwei-Faktor-Authentifizierungsschlüssel - ");
                string otp = Console.ReadLine();
                bool dx11 = true;
                Console.Write("Möchten Sie das Spiel mit ak tiviertem DirectX 11 starten? - ");
                string dx1prompt = Console.ReadLine();
                if (dx1prompt.ToLower() == "yes")
                {
                    dx11 = true;
                }
                else
			    {
                    dx11 = false; 
			    }
                Console.WriteLine("Bitte geben Sie Ihr Erweiterungspaket-Level ein - Derzeit gültige sind \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                int expansionLevel = int.Parse(Console.ReadLine());
                Console.Write("Bitte geben Sie eine Region für Ihre Client-Installation an - Derzeit gültige sind \n 1- Japan, 2 - Amerika, 3 - International: -  ");
                int region = int.Parse(Console.ReadLine());
                try
                {
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, dx11, expansionLevel, isSteam , region);



                }
                catch (Exception exc)
                {
                    Console.WriteLine("Die Anmeldung ist fehlgeschlagen, überprüfen Sie Ihre Anmeldeinformationen oder versuchen Sie es erneut. \n" + exc.Message);
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Beenden des Launchers");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
            }
        }

        public static void FrenchLaunch(int language)
        {
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
                string gamePath;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\gamepath.txt")) {
                  TextReader tr = new StreamReader("gamepath.txt");
                  string gamePathread = tr.ReadLine();
                  gamePath = gamePathread;
                  tr.Close();
                  Console.WriteLine(gamePath);
                }
                else
			    {
                  Console.Write("Veuillez entrer votre chemin de jeu - ");
                  gamePath = Console.ReadLine();
                  TextWriter tw = new StreamWriter("gamepath.txt");
                  tw.WriteLine(gamePath);
                  tw.Close();
			    }
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
                string password = Program.ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Clé d'authentification à deux facteurs - ");
                string otp = Console.ReadLine();
                bool dx11 = true;
                Console.Write("Voulez-vous lancer le jeu avec DirectX 11 activé ? - ");
                string dx1prompt = Console.ReadLine();
                if (dx1prompt.ToLower() == "yes")
                {
                    dx11 = true;
                }
                else
			    {
                    dx11 = false; 
			    }
                Console.WriteLine("Veuillez saisir le niveau de votre pack d'extension - Les versions actuellement valides sont \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                int expansionLevel = int.Parse(Console.ReadLine());
                Console.Write("Veuillez indiquer une région pour l'installation de votre client - Les régions actuellement valides sont \n 1- Japon , 2 - Amérique , 3 - International: - ");
                int region = int.Parse(Console.ReadLine());
                try
                {
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, dx11, expansionLevel, isSteam , region);



                }
                catch (Exception exc)
                {
                    Console.WriteLine("Échec de la connexion, vérifiez vos informations de connexion ou réessayez.\n" + exc.Message);
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Quitter le lanceur");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
            }
        }

        public static void RussianLaunch(int language)
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
                string gamePath;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\gamepath.txt")) {
                  TextReader tr = new StreamReader("gamepath.txt");
                  string gamePathread = tr.ReadLine();
                  gamePath = gamePathread;
                  tr.Close();
                  Console.WriteLine(gamePath);
                }
                else
			    {
                  Console.Write("Введите путь до клиента игры - ");
                  gamePath = Console.ReadLine();
                  TextWriter tw = new StreamWriter("gamepath.txt");
                  tw.WriteLine(gamePath);
                  tw.Close();
			    }
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
                string password = Program.ReadPassword();
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Код Двух-Факторной аутентификации - ");
                string otp = Console.ReadLine();
                bool dx11 = true;
                Console.Write("Вы хотите запустить игру с использованием DirectX 11? - ");
                string dx1prompt = Console.ReadLine();
                if (dx1prompt.ToLower() == "yes")
                {
                    dx11 = true;
                }
                else
			    {
                    dx11 = false; 
			    }
                Console.WriteLine("Пожалуйста, введите уровень доступного вам дополнения - на текущий момент валидными являются следущие \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                int expansionLevel = int.Parse(Console.ReadLine());
                Console.Write("Укажите регион установленного клиента. Действующие в настоящее время \n 1- Japan , 2 - America , 3 - International: - ");
                int region = int.Parse(Console.ReadLine());
                try
                {
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, 1, dx11, expansionLevel, isSteam, region);



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
