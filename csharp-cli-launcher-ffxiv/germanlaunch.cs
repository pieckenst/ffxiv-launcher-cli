using System;
using static networklogic;
using static LaunchMethods;
using csharp_cli_launcher_ffxiv;
using System.IO;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GermanLaunchMethod
{
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
                string username;
                
                //Console.WriteLine("Provided username {0}", username);
                
                string password;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\password.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\username.txt")) {
                  bool promter = false;
                  Console.Write("Möchten Sie das vorhandene gespeicherte Login und Passwort verwenden? - ");
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
                    Console.Write("Nutzername - ");
                    username = Console.ReadLine();
                    Console.Write("Passwort - ");
                    password = Program.ReadPassword();
                  }
                }
                else
			    {
                  Console.Write("Nutzername - ");
                  username = Console.ReadLine();
                  TextWriter twx = new StreamWriter("username.txt");
                  twx.WriteLine(username);
                  twx.Close();
                  Console.Write("Passwort - ");
                  password = Program.ReadPassword();
                  TextWriter tw = new StreamWriter("password.txt");
                  tw.WriteLine(password);
                  tw.Close();

                }
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Zwei-Faktor-Authentifizierungsschlüssel - ");
                string otp = Console.ReadLine();
                string dx1prompt;
                bool dx11 = false;
                int expansionLevel;
                int region;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\booleansandvars.txt"))
			    {
                   bool promterx = false;
                   Console.Write("Möchten Sie vorhandene Parameter laden? - ");
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
                     TextReader tr = new StreamReader("booleansandvars.txt");
                     string dx1reader = tr.ReadLine();
                     dx1prompt = dx1reader;
                     if (dx1prompt.ToLower() == "yes")
                     {
                       dx11 = true;
                     }
                     else
			         {
                       dx11 = false; 
			         }
                     string exlevelreader = tr.ReadLine();
                     expansionLevel = int.Parse(exlevelreader);
                     string regionreader = tr.ReadLine();
                     region =  int.Parse(regionreader);
                     tr.Close();
                   }
                   else
				   {
                     Console.Write("Möchten Sie das Spiel mit aktiviertem DirectX 11 starten? - ");
                     dx1prompt = Console.ReadLine();
                     if (dx1prompt.ToLower() == "yes")
                     {
                     dx11 = true;
                     }
                     else
			         {
                     dx11 = false; 
			         }
                     Console.WriteLine("Bitte geben Sie Ihr Erweiterungspaket-Level ein - Derzeit gültig sind \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                     expansionLevel = int.Parse(Console.ReadLine());
                     Console.Write("Bitte geben Sie eine Region für Ihre Client-Installation an - Derzeit gültige sind \n 1- Japan , 2 - America , 3 - International: - ");
                     region = int.Parse(Console.ReadLine());
				   }
			    }
                else
			    {
                  Console.Write("Möchten Sie das Spiel mit aktiviertem DirectX 11 starten? - ");
                  dx1prompt = Console.ReadLine();
                  if (dx1prompt.ToLower() == "yes")
                  {
                    dx11 = true;
                  }
                  else
			      {
                    dx11 = false; 
			      }
                  Console.WriteLine("Bitte geben Sie Ihr Erweiterungspaket-Level ein - Derzeit gültig sind \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                  expansionLevel = int.Parse(Console.ReadLine());
                  TextWriter twxx = new StreamWriter("booleansandvars.txt");
                  Console.Write("Bitte geben Sie eine Region für Ihre Client-Installation an - Derzeit gültige sind \n 1- Japan , 2 - America , 3 - International: - ");
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
                Console.WriteLine("Beenden des Launchers");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
            }
        }
}
