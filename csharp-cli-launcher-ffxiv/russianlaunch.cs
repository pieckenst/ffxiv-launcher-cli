using System;
using static networklogic;
using static LaunchMethods;
using csharp_cli_launcher_ffxiv;
using System.IO;
/// <summary>
/// Russian Launch Logic
/// </summary>
public class RussianLaunchMethod
{
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
                  gamePath = GamePathLoad();
                }
                else
			    {
                  Console.Write("Введите путь до клиента игры - ");
                  gamePath = GamePathWrite();
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
                string username;
                
                //Console.WriteLine("Provided username {0}", username);
                
                string password;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\password.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\username.txt")) {
                  bool promter = false;
                  Console.Write("Хотите ли вы использовать сохраненные имя пользователя и пароль? - ");
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
                    Console.Write("Имя Пользователя - ");
                    username = Console.ReadLine();
                    Console.Write("Пароль - ");
                    password = Program.ReadPassword();
                  }
                }
                else
			    {
                  Console.Write("Имя Пользователя - ");
                  username = UserNameWrite();
                  Console.Write("Пароль - ");
                  password = PasswordWrite();

                }
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("Код Двух-Факторной аутентификации - ");
                string otp = Console.ReadLine();
                string dx1prompt;
                bool dx11 = false;
                int expansionLevel;
                int region;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\booleansandvars.txt"))
			    {
                   bool promterx = false;
                   Console.Write("Хотитите ли вы запустить игру с сохраненными параметрами? - ");
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
                     Console.Write("Вы хотите запустить игру с использованием DirectX 11? - ");
                     dx1prompt = Console.ReadLine();
                     if (dx1prompt.ToLower() == "yes")
                     {
                     dx11 = true;
                     }
                     else
			         {
                     dx11 = false; 
			         }
                     Console.WriteLine("Пожалуйста, введите уровень доступного вам дополнения - на текущий момент валидными являются следущие \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                     expansionLevel = int.Parse(Console.ReadLine());
                     Console.Write("Укажите регион установленного клиента. Действующие в настоящее время \n 1- Japan , 2 - America , 3 - International: - ");
                     region = int.Parse(Console.ReadLine());
				   }
			    }
                else
			    {
                  Console.Write("Вы хотите запустить игру с использованием DirectX 11? - ");
                  dx1prompt = Console.ReadLine();
                  if (dx1prompt.ToLower() == "yes")
                  {
                    dx11 = true;
                  }
                  else
			      {
                    dx11 = false; 
			      }
                  Console.WriteLine("Пожалуйста, введите уровень доступного вам дополнения - на текущий момент валидными являются следущие \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers");
                  expansionLevel = int.Parse(Console.ReadLine());
                  TextWriter twxx = new StreamWriter("booleansandvars.txt");
                  Console.Write("Укажите регион установленного клиента. Действующие в настоящее время \n 1- Japan , 2 - America , 3 - International: - ");
                  region = int.Parse(Console.ReadLine());
                  twxx.WriteLine(dx1prompt);
                  twxx.WriteLine(expansionLevel);
                  twxx.WriteLine(region);
                  twxx.Close();
                  
			    }
                
                LogicLaunchRnorm(gamePath,username,password,otp ,language , expansionLevel  ,region,isSteam ,dx11);
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
