import System
import SwiftyXIVLoader
import System.IO

public class FrenchLaunchMethod {
	public static var salt: UInt8[] = [0, 0, 0, 0, 0, 0, 0, 0]
	public static func FrenchLaunch(_ language: Int32) {
		Console.WriteLine("-------------------------------------")
		Console.WriteLine("Qu\'est-ce que tu aimerais faire?")
		Console.WriteLine("  1) Connexion")
		Console.WriteLine("  2) Sortir")
		Console.Write("Entrée - ")
		var ansys = Console.ReadKey()
		var lc:LaunchMethods = LaunchMethods()
		Console.WriteLine()
		Console.WriteLine("-------------------------------------")
		if ansys.KeyChar == "1" {
			// Console.WriteLine("-------------------------------------");
			Console.WriteLine()
			var gamePath: String!

			if File.Exists(Directory.GetCurrentDirectory() + "\\gamepath.txt") {
				gamePath = lc.GamePathLoad()
			} else {
				Console.Write("Veuillez entrer votre chemin de jeu - ")
				gamePath = lc.GamePathWrite()
			}
			Console.WriteLine("-------------------------------------")
			var isSteam: Bool = false
			Console.Write("Votre jeu est-il une version Steam du client? - ")
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
				Console.Write("Souhaitez-vous utiliser le login et le mot de passe enregistrés existants? - ")
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
					Console.Write("Nom d\'utilisateur - ")
					username = Console.ReadLine()
					Console.Write("Mot de passe - ")
					password = Program.ReadPassword()
				}
			} else {
				Console.Write("Nom d\'utilisateur - ")
				username = LaunchMethods.UserNameWrite()
				Console.Write("Mot de passe - ")
				password = LaunchMethods.PasswordWrite()
			}
			// string maskpassword = "";
			// for (int i = 0; i < password.Length; i++) {
			// maskpassword += "*";
			// }
			// Console.Write("Your Password is:" + maskpassword);
			Console.WriteLine()
			Console.Write("Clé d\'authentification à deux facteurs - ")
			var otp: String! = Console.ReadLine()
			var dx1prompt: String!
			var dx11: Bool = false
			var expansionLevel: Int32
			var region: Int32
			if File.Exists(Directory.GetCurrentDirectory() + "\\booleansandvars.txt") {
				var promterx: Bool = false
				Console.Write("Souhaitez-vous charger les paramètres existants? - ")
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
					Console.Write("Voulez-vous lancer le jeu avec DirectX 11 activé? - ")
					dx1prompt = Console.ReadLine()
					if dx1prompt.ToLower() == "yes" {
						dx11 = true
					} else {
						dx11 = false
					}
					Console.WriteLine("Veuillez saisir le niveau de votre pack d\'extension - Les versions actuellement valides sont \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers")
					expansionLevel = Int32.Parse(Console.ReadLine())
					Console.Write("Veuillez indiquer une région pour l\'installation de votre client - Les régions actuellement valides sont \n 1- Japan , 2 - America , 3 - International: - ")
					region = Int32.Parse(Console.ReadLine())
				}
			} else {
				Console.Write("Voulez-vous lancer le jeu avec DirectX 11 activé? - ")
				dx1prompt = Console.ReadLine()
				if dx1prompt.ToLower() == "yes" {
					dx11 = true
				} else {
					dx11 = false
				}
				Console.WriteLine("Veuillez saisir le niveau de votre pack d\'extension - Les versions actuellement valides sont \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers")
				expansionLevel = Int32.Parse(Console.ReadLine())
				var twxx: TextWriter! = StreamWriter("booleansandvars.txt")
				Console.Write("Veuillez indiquer une région pour l\'installation de votre client - Les régions actuellement valides sont \n 1- Japan , 2 - America , 3 - International: - ")
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
			Console.WriteLine("Quitter le lanceur")
			Console.WriteLine("-------------------------------------")
			Console.ReadLine()
		}
	}

}