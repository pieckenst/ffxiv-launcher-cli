using System;
using static networklogic;
using static LaunchMethods;
using csharp_cli_launcher_ffxiv;
using System.IO;

/// <summary>
/// Japan Launch Logic
/// </summary>
public class JapaneseLaunchMethod
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
                  gamePath = GamePathLoad();
                }
                else
			    {
                  Console.Write("ゲームパスを入力してください - ");
                  gamePath = GamePathWrite();
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
                string username;
                
                //Console.WriteLine("Provided username {0}", username);
                
                string password;
            if (File.Exists(Directory.GetCurrentDirectory() + @"\password.txt") || File.Exists(Directory.GetCurrentDirectory() + @"\password.XIVloadEnc") && File.Exists(Directory.GetCurrentDirectory() + @"\username.txt"))
            {
                  bool promter = false;
                  Console.Write("保存されている既存のログインとパスワードを使用しますか? - ");
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
                    TextReader tr = new StreamReader("privatekey.txt");
                    string keyread = tr.ReadLine();
                    DecryptFile("password.XIVloadEnc", "password.txt", keyread);
                    TextReader prr = new StreamReader("password.txt");
                    password = prr.ReadLine();
                    prr.Close();
                }
                  else
				  {
                    Console.Write("ユーザーID - ");
                    username = Console.ReadLine();
                    Console.Write("パスワード - ");
                    password = Program.ReadPassword();
                  }
                }
                else
			    {
                  Console.Write("ユーザーID - ");
                  username = UserNameWrite();
                  Console.Write("パスワード - ");
                  password = PasswordWrite();

                }
                //string maskpassword = "";
                //for (int i = 0; i < password.Length; i++) { 
                //maskpassword += "*"; 
                //}


                //Console.Write("Your Password is:" + maskpassword);
                Console.WriteLine();

                Console.Write("2要素認証キ - ");
                string otp = Console.ReadLine();
                string dx1prompt;
                bool dx11 = false;
                int expansionLevel;
                int region;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\booleansandvars.txt"))
			    {
                   bool promterx = false;
                   Console.Write("既存のパラメータをロードしますか? - ");
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
                     Console.Write("DirectX11を有効にしてゲームを起動しますか? - ");
                     dx1prompt = Console.ReadLine();
                     if (dx1prompt.ToLower() == "yes")
                     {
                     dx11 = true;
                     }
                     else
			         {
                     dx11 = false; 
			         }
                     Console.WriteLine("拡張パックのレベルを入力してください-ここに現在利用可能で有効なものがあります \n 0-ARR-1-ヘブンスワード-2-ストームブラッド-3-シャドウブリンガー");
                     expansionLevel = int.Parse(Console.ReadLine());
                     Console.Write("クライアントインストール用のリージョンを指定してください-現在有効なリージョンは次のとおりです \n 1-日本、2-アメリカ、3-国際: - ");
                     region = int.Parse(Console.ReadLine());
				   }
			    }
                else
			    {
                  Console.Write("DirectX11を有効にしてゲームを起動しますか? - ");
                  dx1prompt = Console.ReadLine();
                  if (dx1prompt.ToLower() == "yes")
                  {
                    dx11 = true;
                  }
                  else
			      {
                    dx11 = false; 
			      }
                  Console.WriteLine("拡張パックのレベルを入力してください-ここに現在利用可能で有効なものがあります \n 0-ARR-1-ヘブンスワード-2-ストームブラッド-3-シャドウブリンガー");
                  expansionLevel = int.Parse(Console.ReadLine());
                  TextWriter twxx = new StreamWriter("booleansandvars.txt");
                  Console.Write("クライアントインストール用のリージョンを指定してください-現在有効なリージョンは次のとおりです \n 1-日本、2-アメリカ、3-国際: - ");
                  region = int.Parse(Console.ReadLine());
                  twxx.WriteLine(dx1prompt);
                  twxx.WriteLine(expansionLevel);
                  twxx.WriteLine(region);
                  twxx.Close();
                  
			    }
                File.Delete("password.txt");
                LogicLaunchRnorm(gamePath, username, password, otp, language, expansionLevel, region, isSteam, dx11);
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
}
