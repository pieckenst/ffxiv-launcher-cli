import System
import SwiftyXIVLoader
import System.IO
public class JapaneseLaunchMethod {
	public static var salt: UInt8[] = [0, 0, 0, 0, 0, 0, 0, 0]
	public static func JapanLaunch(_ language: Int32) {
		Console.WriteLine("-------------------------------------")
		Console.WriteLine("何をしたいですか? ")
		Console.WriteLine("  1) ログイン ")
		Console.WriteLine("  2) 出口")
		Console.Write("入力 - ")
		var ansys = Console.ReadKey()
		Console.WriteLine()
		Console.WriteLine("-------------------------------------")
		if ansys.KeyChar == "1" {
			// Console.WriteLine("-------------------------------------");
			Console.WriteLine()
			var gamePath: String!
			if File.Exists(Directory.GetCurrentDirectory() + "\\gamepath.txt") {
				gamePath = LaunchMethods.GamePathLoad()
			} else {
				Console.Write("ゲームパスを入力してください - ")
				gamePath = LaunchMethods.GamePathWrite()
			}
			Console.WriteLine("-------------------------------------")
			var isSteam: Bool = false
			Console.Write("あなたのゲームはクライアントのSteamバージョンですか? - ")
			var promtw: String! = Console.ReadLine()
			if promtw.ToLower() == "yes" {
				isSteam = true
			} else {
				isSteam = false
			}
			Console.WriteLine("-------------------------------------")
			var username: String!
			// Console.WriteLine("Provided username {0}", username);
			var password: String!
			if File.Exists(Directory.GetCurrentDirectory() + "\\password.txt") || (File.Exists(Directory.GetCurrentDirectory() + "\\password.XIVloadEnc") && File.Exists(Directory.GetCurrentDirectory() + "\\username.txt")) {
				var promter: Bool = false
				Console.Write("保存されている既存のログインとパスワードを使用しますか? - ")
				var askaway: String! = Console.ReadLine()
				if askaway.ToLower() == "yes" {
					promter = true
				} else {
					promter = false
				}
				if promter == true {
					username = LaunchMethods.ReturnUsername()
					var tr: TextReader! = StreamReader("privatekey.txt")
					var keyread: String! = tr.ReadLine()
					LaunchMethods.DecryptFile("password.XIVloadEnc", "password.txt", keyread,salt,1000)
					var prr: TextReader! = StreamReader("password.txt")
					password = prr.ReadLine()
					prr.Close()
				} else {
					Console.Write("ユーザーID - ")
					username = Console.ReadLine()
					Console.Write("パスワード - ")
					password = Program.ReadPassword()
				}
			} else {
				Console.Write("ユーザーID - ")
				username = LaunchMethods.UserNameWrite()
				Console.Write("パスワード - ")
				password = LaunchMethods.PasswordWrite()
			}
			// string maskpassword = "";
			// for (int i = 0; i < password.Length; i++) {
			// maskpassword += "*";
			// }
			// Console.Write("Your Password is:" + maskpassword);
			Console.WriteLine()
			Console.Write("2要素認証キ - ")
			var otp: String! = Console.ReadLine()
			var dx1prompt: String!
			var dx11: Bool = false
			var expansionLevel: Int32
			var region: Int32
			if File.Exists(Directory.GetCurrentDirectory() + "\\booleansandvars.txt") {
				var promterx: Bool = false
				Console.Write("既存のパラメータをロードしますか? - ")
				var askawayx: String! = Console.ReadLine()
				if askawayx.ToLower() == "yes" {
					promterx = true
				} else {
					promterx = false
				}
				if promterx == true {
					dx1prompt = LaunchMethods.dx1readd()
					if dx1prompt.ToLower() == "yes" {
						dx11 = true
					} else {
						dx11 = false
					}
					expansionLevel = LaunchMethods.exlevelread()
					region = LaunchMethods.regionread()
				} else {
					Console.Write("DirectX11を有効にしてゲームを起動しますか? - ")
					dx1prompt = Console.ReadLine()
					if dx1prompt.ToLower() == "yes" {
						dx11 = true
					} else {
						dx11 = false
					}
					Console.WriteLine("拡張パックのレベルを入力してください-ここに現在利用可能で有効なものがあります \n 0-ARR-1-ヘブンスワード-2-ストームブラッド-3-シャドウブリンガー")
					expansionLevel = Int32.Parse(Console.ReadLine())
					Console.Write("クライアントインストール用のリージョンを指定してください-現在有効なリージョンは次のとおりです \n 1-日本、2-アメリカ、3-国際: - ")
					region = Int32.Parse(Console.ReadLine())
				}
			} else {
				Console.Write("DirectX11を有効にしてゲームを起動しますか? - ")
				dx1prompt = Console.ReadLine()
				if dx1prompt.ToLower() == "yes" {
					dx11 = true
				} else {
					dx11 = false
				}
				Console.WriteLine("拡張パックのレベルを入力してください-ここに現在利用可能で有効なものがあります \n 0-ARR-1-ヘブンスワード-2-ストームブラッド-3-シャドウブリンガー")
				expansionLevel = Int32.Parse(Console.ReadLine())
				var twxx: TextWriter! = StreamWriter("booleansandvars.txt")
				Console.Write("クライアントインストール用のリージョンを指定してください-現在有効なリージョンは次のとおりです \n 1-日本、2-アメリカ、3-国際: - ")
				region = Int32.Parse(Console.ReadLine())
				twxx.WriteLine(dx1prompt)
				twxx.WriteLine(expansionLevel)
				twxx.WriteLine(region)
				twxx.Close()
			}
			File.Delete("password.txt")
			LaunchMethods.LogicLaunchRnorm(gamePath, username, password, otp, language, expansionLevel, region, isSteam, dx11)
			Console.ReadLine()
		} else {
			Console.WriteLine("-------------------------------------")
			Console.WriteLine("ランチャーを終了する")
			Console.WriteLine("-------------------------------------")
			Console.ReadLine()
		}
	}

}