using System;
using static networklogic;
using csharp_cli_launcher_ffxiv;

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
                string password = Program.ReadPassword();
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
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, true, expansionLevel, isSteam);



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
                string password = Program.ReadPassword();
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
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, true, expansionLevel, isSteam);



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
                string password = Program.ReadPassword();
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
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, true, expansionLevel, isSteam);



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
                string password = Program.ReadPassword();
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
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, true, expansionLevel, isSteam);



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
                string password = Program.ReadPassword();
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
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, 1, true, expansionLevel, isSteam);



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
