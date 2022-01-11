import System
import SwiftyXIVLoader
import System.IO
public class GermanLaunchMethod {
	public static var salt: UInt8[] = [0, 0, 0, 0, 0, 0, 0, 0]
	public static func GermanLaunch(_ language: Int32) {
		Console.WriteLine("-------------------------------------")
		Console.WriteLine("Was würdest du gern tun?")
		Console.WriteLine("  1) Anmeldung")
		Console.WriteLine("  2) Ausgang")
		Console.Write("Eingang - ")
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
				Console.Write("Bitte geben Sie Ihren Spielpfad ein - ")
				gamePath = LaunchMethods.GamePathWrite()
			}
			Console.WriteLine("-------------------------------------")
			var isSteam: Bool = false
			Console.Write("Ist Ihr Spiel eine Steam-Version des Clients? - ")
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
				Console.Write("Möchten Sie das vorhandene gespeicherte Login und Passwort verwenden? - ")
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
					Console.Write("Nutzername - ")
					username = Console.ReadLine()
					Console.Write("Passwort - ")
					password = Program.ReadPassword()
				}
			} else {
				Console.Write("Nutzername - ")
				username = LaunchMethods.UserNameWrite()
				Console.Write("Passwort - ")
				password = LaunchMethods.PasswordWrite()
			}
			// string maskpassword = "";
			// for (int i = 0; i < password.Length; i++) {
			// maskpassword += "*";
			// }
			// Console.Write("Your Password is:" + maskpassword);
			Console.WriteLine()
			Console.Write("Zwei-Faktor-Authentifizierungsschlüssel - ")
			var otp: String! = Console.ReadLine()
			var dx1prompt: String!
			var dx11: Bool = false
			var expansionLevel: Int32
			var region: Int32
			if File.Exists(Directory.GetCurrentDirectory() + "\\booleansandvars.txt") {
				var promterx: Bool = false
				Console.Write("Möchten Sie vorhandene Parameter laden? - ")
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
					Console.Write("Möchten Sie das Spiel mit aktiviertem DirectX 11 starten? - ")
					dx1prompt = Console.ReadLine()
					if dx1prompt.ToLower() == "yes" {
						dx11 = true
					} else {
						dx11 = false
					}
					Console.WriteLine("Bitte geben Sie Ihr Erweiterungspaket-Level ein - Derzeit gültig sind \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers")
					expansionLevel = Int32.Parse(Console.ReadLine())
					Console.Write("Bitte geben Sie eine Region für Ihre Client-Installation an - Derzeit gültige sind \n 1- Japan , 2 - America , 3 - International: - ")
					region = Int32.Parse(Console.ReadLine())
				}
			} else {
				Console.Write("Möchten Sie das Spiel mit aktiviertem DirectX 11 starten? - ")
				dx1prompt = Console.ReadLine()
				if dx1prompt.ToLower() == "yes" {
					dx11 = true
				} else {
					dx11 = false
				}
				Console.WriteLine("Bitte geben Sie Ihr Erweiterungspaket-Level ein - Derzeit gültig sind \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers")
				expansionLevel = Int32.Parse(Console.ReadLine())
				var twxx: TextWriter! = StreamWriter("booleansandvars.txt")
				Console.Write("Bitte geben Sie eine Region für Ihre Client-Installation an - Derzeit gültige sind \n 1- Japan , 2 - America , 3 - International: - ")
				region = Int32.Parse(Console.ReadLine())
				twxx.WriteLine(dx1prompt)
				twxx.WriteLine(expansionLevel)
				twxx.WriteLine(region)
				twxx.Close()
			}
			File.Delete("password.txt")
			LaunchMethods.LogicLaunchNorm(gamePath, username, password, otp, language, expansionLevel, region, isSteam, dx11)
			Console.ReadLine()
		} else {
			Console.WriteLine("-------------------------------------")
			Console.WriteLine("Beenden des Launchers")
			Console.WriteLine("-------------------------------------")
			Console.ReadLine()
		}
	}


}