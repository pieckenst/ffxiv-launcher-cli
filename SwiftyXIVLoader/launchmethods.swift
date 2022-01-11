import System
import SwiftyXIVLoader
import System.IO
import System.Text
import System.Text.RegularExpressions
import System.Security
import System.Security.Cryptography
import System.Runtime.InteropServices

public class LaunchMethods
{
	public static var salt: UInt8[] = [0, 0, 0, 0, 0, 0, 0, 0]


	public static func EnglishLaunch(_ language: Int32) {
		Console.WriteLine("-------------------------------------")
		Console.WriteLine("What would you like to do?")
		Console.WriteLine("  1) Login")
		Console.WriteLine("  2) Exit")
		Console.Write("Input - ")
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
				Console.Write("Please enter your gamepath - ")
				gamePath = LaunchMethods.GamePathWrite()
			}
			Console.WriteLine("-------------------------------------")
			var isSteam: Bool = false
			Console.Write("Is your game a steam version of the client? - ")
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
				Console.Write("Do you wish to use existing saved login and password? - ")
				var askaway: String! = Console.ReadLine()
				if askaway.ToLower() == "yes" {
					promter = true
				} else {
					promter = false
				}
				if promter == true {
					username = ReturnUsername()
					var tr: TextReader! = StreamReader("privatekey.txt")
					var keyread: String! = tr.ReadLine()
					DecryptFile("password.XIVloadEnc", "password.txt", keyread,salt,1000)
					var prr: TextReader! = StreamReader("password.txt")
					password = prr.ReadLine()
					prr.Close()

				} else {
					Console.Write("Username - ")
					username = Console.ReadLine()
					Console.Write("Password - ")
					password = Program.ReadPassword()
					// string key = GenerateKey();
					// EncryptFile("password.txt", "password.XIVloadEnc", key);
				}
			} else {
				Console.Write("Username - ")
				username = UserNameWrite()
				Console.Write("Password - ")
				password = PasswordWrite()
				var key: String! = GenerateKey()
				EncryptFile("password.txt", "password.XIVloadEnc", key,salt,1000)
			}
			// string maskpassword = "";
			// for (int i = 0; i < password.Length; i++) {
			// maskpassword += "*";
			// }
			// Console.Write("Your Password is:" + maskpassword);
			Console.WriteLine()
			Console.Write("Two-Factor Authefication Key - ")
			var otp: String! = Console.ReadLine()
			var dx1prompt: String!
			var dx11: Bool = false
			var expansionLevel: Int32
			var region: Int32
			if File.Exists(Directory.GetCurrentDirectory() + "\\booleansandvars.txt") {
				var promterx: Bool = false
				Console.Write("Do you wish to load existing params? - ")
				var askawayx: String! = Console.ReadLine()
				if askawayx.ToLower() == "yes" {
					promterx = true
				} else {
					promterx = false
				}
				if promterx == true {
					dx1prompt = dx1readd()
					if dx1prompt.ToLower() == "yes" {
						dx11 = true
					} else {
						dx11 = false
					}
					expansionLevel = exlevelread()
					region = regionread()
				} else {
					Console.Write("Do you want to launch the game with enabled DirectX 11? - ")
					dx1prompt = Console.ReadLine()
					if dx1prompt.ToLower() == "yes" {
						dx11 = true
					} else {
						dx11 = false
					}
					Console.WriteLine("Please enter your expansion pack level - Currently valid ones are \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers")
					expansionLevel = Int32.Parse(Console.ReadLine())
					Console.Write("Please provide a region for your client install - Currently valid ones are \n 1- Japan , 2 - America , 3 - International: - ")
					region = Int32.Parse(Console.ReadLine())
				}
			} else {
				Console.Write("Do you want to launch the game with enabled DirectX 11? - ")
				dx1prompt = Console.ReadLine()
				if dx1prompt.ToLower() == "yes" {
					dx11 = true
				} else {
					dx11 = false
				}
				Console.WriteLine("Please enter your expansion pack level - Currently valid ones are \n 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers")
				expansionLevel = Int32.Parse(Console.ReadLine())
				var twxx: TextWriter! = StreamWriter("booleansandvars.txt")
				Console.Write("Please provide a region for your client install - Currently valid ones are \n 1- Japan , 2 - America , 3 - International: - ")
				region = Int32.Parse(Console.ReadLine())
				twxx.WriteLine(dx1prompt)
				twxx.WriteLine(expansionLevel)
				twxx.WriteLine(region)
				twxx.Close()
			}
			File.Delete("password.txt")
			LogicLaunchNorm(gamePath, username, password, otp, language, expansionLevel, region, isSteam, dx11)
			Console.ReadLine()
		} else {
			Console.WriteLine("-------------------------------------")
			Console.WriteLine("Exiting the launcher")
			Console.WriteLine("-------------------------------------")
			Console.ReadLine()
		}
	}

	public static func ReturnUsername() -> String! {
		var trx: TextReader! = StreamReader("username.txt")
		var usernameread: String! = trx.ReadLine()
		var username: String! = usernameread
		trx.Close()
		return username
	}

	public static func LogicLaunchNorm(_ gamePath: String!, _ username: String!, _ password: String!, _ otp: String!, _ language: Int32, _ expansionLevel: Int32, _ region: Int32, _ isSteam: Bool, _ dx11: Bool) {
		__try {
			var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam)
			if sid.Equals("BAD") {
				return
			}
			var ffxivGame = networklogic.LaunchGame(gamePath, sid, language, dx11, expansionLevel, isSteam, region)
		}
		__catch exc: Exception {
			if language == 0 {
				Console.WriteLine("ログインに失敗しました。ログイン情報を確認するか、再試行してください.\n" + exc.Message)
			}
			if language == 1 {
				Console.WriteLine("Logging in failed, check your login information or try again.\n" + exc.Message)
			}
			if language == 2 {
				Console.WriteLine("Anmeldung fehlgeschlagen, überprüfe deine Anmeldedaten oder versuche es noch einmal.\n" + exc.Message)
			}
			if language == 3 {
				Console.WriteLine("Échec de la connexion, vérifiez vos informations de connexion ou réessayez.\n" + exc.Message)
			}
			if language == 4 {
				Console.WriteLine("Не удалось войти в систему, проверьте данные для входа или попробуйте еще раз.\n" + exc.Message)
			}
		}
	}
	public static func LogicLaunchRnorm(_ gamePath: String!, _ username: String!, _ password: String!, _ otp: String!, _ language: Int32, _ expansionLevel: Int32, _ region: Int32, _ isSteam: Bool, _ dx11: Bool) {
		__try {
			var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam)
			if sid.Equals("BAD") {
				return
			}
			var ffxivGame = networklogic.LaunchGame(gamePath, sid, 1, dx11, expansionLevel, isSteam, region)
		}
		__catch exc: Exception {
			if language == 0 {
				Console.WriteLine("ログインに失敗しました。ログイン情報を確認するか、再試行してください.\n" + exc.Message)
			}
			if language == 1 {
				Console.WriteLine("Logging in failed, check your login information or try again.\n" + exc.Message)
			}
			if language == 2 {
				Console.WriteLine("Anmeldung fehlgeschlagen, überprüfe deine Anmeldedaten oder versuche es noch einmal.\n" + exc.Message)
			}
			if language == 3 {
				Console.WriteLine("Échec de la connexion, vérifiez vos informations de connexion ou réessayez.\n" + exc.Message)
			}
			if language == 4 {
				Console.WriteLine("Не удалось войти в систему, проверьте данные для входа или попробуйте еще раз.\n" + exc.Message)
			}
		}
	}
	public static func GamePathWrite() -> String! {
		var gamePath: String! = Console.ReadLine()
		var tw: TextWriter! = StreamWriter("gamepath.txt")
		tw.WriteLine(gamePath)
		tw.Close()
		return gamePath
	}
	public static func GamePathLoad() -> String! {
		var tr: TextReader! = StreamReader("gamepath.txt")
		var gamePathread: String! = tr.ReadLine()
		var gamePath: String! = gamePathread
		tr.Close()
		Console.WriteLine(gamePath)
		return gamePath
	}
	public static func dx1readd() -> String! {
		var tr: TextReader! = StreamReader("booleansandvars.txt")
		var dx1reader: String! = tr.ReadLine()
		var dx1prompt: String! = dx1reader
		tr.Close()
		return dx1prompt
	}
	public static func exlevelread() -> Int32 {
		var tr: TextReader! = StreamReader("booleansandvars.txt")
		var blankreader: String! = tr.ReadLine()
		var exlevelreader: String! = tr.ReadLine()
		var expansionLevel: Int32 = Int32.Parse(exlevelreader)
		tr.Close()
		return expansionLevel
	}
	public static func regionread() -> Int32 {
		var tr: TextReader! = StreamReader("booleansandvars.txt")
		var blankreaderone: String! = tr.ReadLine()
		var blankreadertwo: String! = tr.ReadLine()
		var regionreader: String! = tr.ReadLine()
		var region: Int32 = Int32.Parse(regionreader)
		tr.Close()
		return region
	}
	public static func UserNameWrite() -> String! {
		var username: String! = Console.ReadLine()
		var twx: TextWriter! = StreamWriter("username.txt")
		twx.WriteLine(username)
		twx.Close()
		return username
	}
	public static func PasswordWrite() -> String! {
		var password: String! = Program.ReadPassword()
		var filnamex: String! = "password.txt"
		var tw: TextWriter! = StreamWriter(filnamex)
		tw.WriteLine(password)
		tw.Close()
		return password
	}

	private static func GetStringFromHash(_ hash: UInt8[]) -> String! {
		var result: StringBuilder! = StringBuilder()
		for i in 0 ... hash.Length - 1 {
			result.Append(hash[i].ToString("X2"))
		}
		return result.ToString()
	}


	static func GenerateKey() -> String! {
		var sha512: SHA512! = SHA512Managed.Create()

		var bytes: UInt8[] = Encoding.UTF8.GetBytes("2fep2ifeip2qf[23jofpq2jfp2j3fp2oi3jf32j3fp32pi3jf23ijf2ifj2p3ji")

		var hash: UInt8[] = sha512.ComputeHash(bytes)
		Console.WriteLine(GetStringFromHash(hash))
		var filnamex: String! = "privatekey.txt"

		var tw: TextWriter! = StreamWriter(filnamex)
		tw.WriteLine(GetStringFromHash(hash))
        tw.Close()



		return GetStringFromHash(hash)




	}
	public static func EncryptFile(_ sourceFilename: String!, _ destinationFilename: String!, _ password: String!, _ salt: UInt8[], _ iterations: Int32) {
		var aes: AesManaged! = AesManaged()
		aes.BlockSize = aes.LegalBlockSizes[0].MaxSize
		aes.KeySize = aes.LegalKeySizes[0].MaxSize
		//  NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
		var key: Rfc2898DeriveBytes! = Rfc2898DeriveBytes(password, salt, iterations)
		aes.Key = key.GetBytes(aes.KeySize / 8)
		aes.IV = key.GetBytes(aes.BlockSize / 8)
		aes.Mode = CipherMode.CBC
		var transform: ICryptoTransform! = aes.CreateEncryptor(aes.Key, aes.IV)
		__using let destination = FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None) {
			__using let cryptoStream = CryptoStream(destination, transform, CryptoStreamMode.Write) {
				__using let source = FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read) {
					source.CopyTo(cryptoStream)
				}
			}
		}
		 File.Delete(sourceFilename);
	}

	public static func DecryptFile(_ sourceFilename: String!, _ destinationFilename: String!, _ password: String!, _ salt: UInt8[], _ iterations: Int32) {
		var aes: AesManaged! = AesManaged()
		aes.BlockSize = aes.LegalBlockSizes[0].MaxSize
		aes.KeySize = aes.LegalKeySizes[0].MaxSize
		//  NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
		var key: Rfc2898DeriveBytes! = Rfc2898DeriveBytes(password, salt, iterations)
		aes.Key = key.GetBytes(aes.KeySize / 8)
		aes.IV = key.GetBytes(aes.BlockSize / 8)
		aes.Mode = CipherMode.CBC
		var transform: ICryptoTransform! = aes.CreateDecryptor(aes.Key, aes.IV)
		if destinationFilename.Exists() {
			File.Delete("password.txt")
        }
		__using let destination = FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None) {
			__using let cryptoStream = CryptoStream(destination, transform, CryptoStreamMode.Write) {
				__try {
					__using let source = FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read) {
						source.CopyTo(cryptoStream)
					}
				}
				__catch exception: CryptographicException {
					if exception.Message == "Padding is invalid and cannot be removed." {
						throw ApplicationException("Universal Microsoft Cryptographic Exception (Not to be believed!)", exception)
						
					} else {
						throw
					}
				}
			}
		}
	}




}