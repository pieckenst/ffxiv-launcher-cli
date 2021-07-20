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
                string username;
                
                //Console.WriteLine("Provided username {0}", username);
                
                string password;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\password.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\username.txt")) {
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
                    TextReader tr = new StreamReader("password.txt");
                    string passwordread = tr.ReadLine();
                    password = passwordread;
                    tr.Close();
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
                  username = Console.ReadLine();
                  TextWriter twx = new StreamWriter("username.txt");
                  twx.WriteLine(username);
                  twx.Close();
                  Console.Write("パスワード - ");
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
}
