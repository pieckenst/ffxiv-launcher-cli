using System;
using static networklogic;
using static LaunchMethods;
using csharp_cli_launcher_ffxiv;
using System.IO;

/// <summary>
/// French Launch Logic
/// </summary>
public class FrenchLaunchMethod
{
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
                string username;
                
                //Console.WriteLine("Provided username {0}", username);
                
                string password;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\password.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\username.txt")) {
                  bool promter = false;
                  Console.Write("Souhaitez-vous utiliser le login et le mot de passe enregistrés existants? - ");
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
                    Console.Write("Nom d'utilisateur - ");
                    username = Console.ReadLine();
                    Console.Write("Mot de passe - ");
                    password = Program.ReadPassword();
                  }
                }
                else
			    {
                  Console.Write("Nom d'utilisateur - ");
                  username = Console.ReadLine();
                  TextWriter twx = new StreamWriter("username.txt");
                  twx.WriteLine(username);
                  twx.Close();
                  Console.Write("Mot de passe - ");
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

                Console.Write("Clé d'authentification à deux facteurs - ");
                string otp = Console.ReadLine();
                string dx1prompt;
                bool dx11 = false;
                int expansionLevel;
                int region;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\booleansandvars.txt"))
			    {
                   bool promterx = false;
                   Console.Write("Souhaitez-vous charger les paramètres existants? - ");
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
                     Console.Write("Voulez-vous lancer le jeu avec DirectX 11 activé? - ");
                     dx1prompt = Console.ReadLine();
                     if (dx1prompt.ToLower() == "yes")
                     {
                     dx11 = true;
                     }
                     else
			         {
                     dx11 = false; 
			         }
                     Console.WriteLine("Veuillez saisir le niveau de votre pack d'extension - Les versions actuellement valides sont \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                     expansionLevel = int.Parse(Console.ReadLine());
                     Console.Write("Veuillez indiquer une région pour l'installation de votre client - Les régions actuellement valides sont \n 1- Japan , 2 - America , 3 - International: - ");
                     region = int.Parse(Console.ReadLine());
				   }
			    }
                else
			    {
                  Console.Write("Voulez-vous lancer le jeu avec DirectX 11 activé? - ");
                  dx1prompt = Console.ReadLine();
                  if (dx1prompt.ToLower() == "yes")
                  {
                    dx11 = true;
                  }
                  else
			      {
                    dx11 = false; 
			      }
                  Console.WriteLine("Veuillez saisir le niveau de votre pack d'extension - Les versions actuellement valides sont \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                  expansionLevel = int.Parse(Console.ReadLine());
                  TextWriter twxx = new StreamWriter("booleansandvars.txt");
                  Console.Write("Veuillez indiquer une région pour l'installation de votre client - Les régions actuellement valides sont \n 1- Japan , 2 - America , 3 - International: - ");
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
                Console.WriteLine("Quitter le lanceur");
                Console.WriteLine("-------------------------------------");
                Console.ReadLine();
            }
        }
}
