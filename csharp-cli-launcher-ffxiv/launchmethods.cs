using System;
using static networklogic;
using csharp_cli_launcher_ffxiv;
using System.IO;

/// <summary>
/// Basically a class for launch sequence of the launcher
/// </summary>
public class LaunchMethods
{
	
        

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
                  gamePath = GamePathLoad();
                }
                else
			    {
                  Console.Write("Please enter your gamepath - ");
                  gamePath = GamePathWrite();
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
                
                string username;
                
                //Console.WriteLine("Provided username {0}", username);
                
                string password;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\password.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\username.txt")) {
                  bool promter = false;
                  Console.Write("Do you wish to use existing saved login and password? - ");
                  string askaway = Console.ReadLine();
                  if (askaway.ToLower() == "yes")
                  {
                    promter = true;
                  }
                  else
                  {
                    promter = false;
                  }
                  if (promter == true) {
                    username = ReturnUsername();
                    TextReader tr = new StreamReader("password.txt");
                    string passwordread = tr.ReadLine();
                    password = passwordread;
                    tr.Close();
                  }
                  else
				  {
                    Console.Write("Username - ");
                    username = Console.ReadLine();
                    Console.Write("Password - ");
                    password = Program.ReadPassword();
                  }
                }
                else
			    {
                  Console.Write("Username - ");
                  username = UserNameWrite();
                  Console.Write("Password - ");
                  password = PasswordWrite();

                }
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Two-Factor Authefication Key - ");
                string otp = Console.ReadLine();
                string dx1prompt;
                bool dx11 = false;
                int expansionLevel;
                int region;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\booleansandvars.txt"))
			    {
                   bool promterx = false;
                   Console.Write("Do you wish to load existing params? - ");
                   string askawayx = Console.ReadLine();
                   if (askawayx.ToLower() == "yes")
                   {
                     promterx = true;
                   }
                   else
                   {
                     promterx = false;
                   }
                   if (promterx == true) { 
                     dx1prompt = dx1readd();
                     if (dx1prompt.ToLower() == "yes")
                     {
                       dx11 = true;
                     }
                     else
			         {
                       dx11 = false; 
			         }
                     expansionLevel = exlevelread();
                     region = regionread();
                   }
                   else
				   {
                     Console.Write("Do you want to launch the game with enabled DirectX 11? - ");
                     dx1prompt = Console.ReadLine();
                     if (dx1prompt.ToLower() == "yes")
                     {
                     dx11 = true;
                     }
                     else
			         {
                     dx11 = false; 
			         }
                     Console.WriteLine("Please enter your expansion pack level - Currently valid ones are \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                     expansionLevel = int.Parse(Console.ReadLine());
                     Console.Write("Please provide a region for your client install - Currently valid ones are \n 1- Japan , 2 - America , 3 - International: - ");
                     region = int.Parse(Console.ReadLine());
				   }
			    }
                else
			    {
                  Console.Write("Do you want to launch the game with enabled DirectX 11? - ");
                  dx1prompt = Console.ReadLine();
                  if (dx1prompt.ToLower() == "yes")
                  {
                    dx11 = true;
                  }
                  else
			      {
                    dx11 = false; 
			      }
                  Console.WriteLine("Please enter your expansion pack level - Currently valid ones are \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                  expansionLevel = int.Parse(Console.ReadLine());
                  TextWriter twxx = new StreamWriter("booleansandvars.txt");
                  Console.Write("Please provide a region for your client install - Currently valid ones are \n 1- Japan , 2 - America , 3 - International: - ");
                  region = int.Parse(Console.ReadLine());
                  twxx.WriteLine(dx1prompt);
                  twxx.WriteLine(expansionLevel);
                  twxx.WriteLine(region);
                  twxx.Close();
                  
			    }
                LogicLaunchNorm(gamePath,username,password,otp ,language , expansionLevel  ,region,isSteam ,dx11);
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

        public static string ReturnUsername()
	    {
           TextReader trx = new StreamReader("username.txt");
           string usernameread = trx.ReadLine();
           string username = usernameread;
           trx.Close();
           return username;
	    }
        public static void LogicLaunchNorm(string gamePath, string username, string password,string otp ,int language , int expansionLevel  ,int region, bool isSteam , bool dx11)
	    {
                try
                {
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, dx11, expansionLevel, isSteam , region);



                }
                catch (Exception exc)
                {
                    if (language == 0)
                    {
                      Console.WriteLine("ログインに失敗しました。ログイン情報を確認するか、再試行してください.\n" + exc.Message);
                    }
                    if (language == 1)
                    {
                      Console.WriteLine("Logging in failed, check your login information or try again.\n" + exc.Message);
                    }
                    if (language == 2)
                    {
                      Console.WriteLine("Anmeldung fehlgeschlagen, überprüfe deine Anmeldedaten oder versuche es noch einmal.\n" + exc.Message);
                    }
                    if (language == 3)
                    {
                      Console.WriteLine("Échec de la connexion, vérifiez vos informations de connexion ou réessayez.\n" + exc.Message);
                    }
                    if (language == 4)
                    {
                      Console.WriteLine("Не удалось войти в систему, проверьте данные для входа или попробуйте еще раз.\n" + exc.Message);
                    }
                    
                }
	    }
        public static void LogicLaunchRnorm(string gamePath, string username, string password,string otp ,int language , int expansionLevel  ,int region, bool isSteam , bool dx11)
	    {
                try
                {
                    var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                    if (sid.Equals("BAD"))
                        return;

                    var ffxivGame = networklogic.LaunchGame(gamePath, sid, 1, dx11, expansionLevel, isSteam , region);



                }
                catch (Exception exc)
                {
                    if (language == 0)
                    {
                      Console.WriteLine("ログインに失敗しました。ログイン情報を確認するか、再試行してください.\n" + exc.Message);
                    }
                    if (language == 1)
                    {
                      Console.WriteLine("Logging in failed, check your login information or try again.\n" + exc.Message);
                    }
                    if (language == 2)
                    {
                      Console.WriteLine("Anmeldung fehlgeschlagen, überprüfe deine Anmeldedaten oder versuche es noch einmal.\n" + exc.Message);
                    }
                    if (language == 3)
                    {
                      Console.WriteLine("Échec de la connexion, vérifiez vos informations de connexion ou réessayez.\n" + exc.Message);
                    }
                    if (language == 4)
                    {
                      Console.WriteLine("Не удалось войти в систему, проверьте данные для входа или попробуйте еще раз.\n" + exc.Message);
                    }
                    
                }
	    }
        public static string GamePathWrite()
	    {
           string gamePath = Console.ReadLine();
           TextWriter tw = new StreamWriter("gamepath.txt");
           tw.WriteLine(gamePath);
           tw.Close();
           return gamePath;
	    }
        public static string GamePathLoad()
	    {
           TextReader tr = new StreamReader("gamepath.txt");
           string gamePathread = tr.ReadLine();
           string gamePath = gamePathread;
           tr.Close();
           Console.WriteLine(gamePath);
           return gamePath;
	    }
        public static string dx1readd()
	    {
           TextReader tr = new StreamReader("booleansandvars.txt");
           string dx1reader = tr.ReadLine();
           string dx1prompt = dx1reader;
           tr.Close();
           return dx1prompt;
	    }
        public static int exlevelread()
	    {
          TextReader tr = new StreamReader("booleansandvars.txt");
          string blankreader = tr.ReadLine();
          string exlevelreader = tr.ReadLine();
          int expansionLevel = int.Parse(exlevelreader);
          tr.Close();
          return expansionLevel;
	    }
        public static int regionread()
	    {
          TextReader tr = new StreamReader("booleansandvars.txt");
          string blankreaderone = tr.ReadLine();
          string blankreadertwo = tr.ReadLine();
          string regionreader = tr.ReadLine();
          int region =  int.Parse(regionreader);
          tr.Close();
          return region;
	    }
        public static string UserNameWrite()
	    {
          string username = Console.ReadLine();
          TextWriter twx = new StreamWriter("username.txt");
          twx.WriteLine(username);
          twx.Close();
          return username;
	    }
        public static string PasswordWrite()
	    {
          string password = Program.ReadPassword();
          TextWriter tw = new StreamWriter("password.txt");
          tw.WriteLine(password);
          tw.Close();
          return password;
	    }
}
