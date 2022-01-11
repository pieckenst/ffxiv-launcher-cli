import Microsoft.Win32
import System
import System.Collections.Generic
import System.Collections.Specialized
import System.Diagnostics
import System.IO
import System.Linq
import System.Net
import System.Net.Security
import System.Security.Cryptography
import System.Text
import System.Text.RegularExpressions

public class networklogic
{
	private static var UserAgentTemplate: String! = "SQEXAuthor/2.0.0(Windows 6.2; ja-jp; {0})"

	private static var UserAgent: String! = GenerateUserAgent()

	public static func LaunchGame(_ gamePath: String!, _ realsid: String!, _ language: Int32, _ dx11: Bool, _ expansionlevel: Int32, _ isSteam: Bool, _ region: Int32) -> Process! {
		__try {
			var ffxivgame: Process! = Process()
			if dx11 == true {
				ffxivgame.StartInfo.FileName = gamePath + "/game/ffxiv_dx11.exe"
			} else {
				ffxivgame.StartInfo.FileName = gamePath + "/game/ffxiv.exe"
			}
			ffxivgame.StartInfo.Arguments = String.Format("DEV.TestSID={0} DEV.MaxEntitledExpansionID={1} language={2} region={3}", realsid, expansionlevel, language, region)
			if isSteam {
				ffxivgame.StartInfo.Environment.Add("IS_FFXIV_LAUNCH_FROM_STEAM", "1")
				ffxivgame.StartInfo.Arguments = ffxivgame.StartInfo.Arguments + " IsSteam=1"
				ffxivgame.StartInfo.UseShellExecute = false
			}
			ffxivgame.Start()
			return ffxivgame
		}
		__catch exc: Exception {
			if language == 0 {
				Console.WriteLine("実行可能ファイルを起動できませんでした。 ゲームパスは正しいですか? " + exc)
			}
			if language == 1 {
				Console.WriteLine("Could not launch executable. Is your game path correct? " + exc)
			}
			if language == 2 {
				Console.WriteLine("Die ausführbare Datei konnte nicht gestartet werden. Ist dein Spielpfad korrekt? " + exc)
			}
			if language == 3 {
				Console.WriteLine("Impossible de lancer l\'exécutable. Votre chemin de jeu est-il correct? " + exc)
			}
			if language == 4 {
				Console.WriteLine("Не удалось запустить файл. Ввели ли вы корректный путь к игре? " + exc)
			}
		}
		return nil
	}
	public static func GetRealSid(_ gamePath: String!, _ username: String!, _ password: String!, _ otp: String!, _ isSteam: Bool) -> String! {
		var hashstr: String! = ""
		__try {
			//  make the string of hashed files to prove game version//make the string of hashed files to prove game version
			hashstr = "ffxivboot.exe/" + GenerateHash(gamePath + "/boot/ffxivboot.exe") + ",ffxivboot64.exe/" + GenerateHash(gamePath + "/boot/ffxivboot64.exe") + ",ffxivlauncher.exe/" + GenerateHash(gamePath + "/boot/ffxivlauncher.exe") + ",ffxivlauncher64.exe/" + GenerateHash(gamePath + "/boot/ffxivlauncher64.exe") + ",ffxivupdater.exe/" + GenerateHash(gamePath + "/boot/ffxivupdater.exe") + ",ffxivupdater64.exe/" + GenerateHash(gamePath + "/boot/ffxivupdater64.exe")
		}
		__catch exc: Exception {
			Console.WriteLine("Could not generate hashes. Is your game path correct? " + exc)
		}
		var sidClient: WebClient! = WebClient()
		sidClient.Headers.Add("X-Hash-Check", "enabled")
		sidClient.Headers.Add("user-agent", UserAgent)
		sidClient.Headers.Add("Referer", "https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn=3")
		sidClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
		InitiateSslTrust()
		__try {
			var localGameVer = GetLocalGamever(gamePath)
			var localSid = GetSid(username, password, otp, isSteam)
			if localGameVer.Equals("BAD") || localSid.Equals("BAD") {
				return "BAD"
			}
			var url = "https://patch-gamever.ffxiv.com/http/win32/ffxivneo_release_game/" + localGameVer + "/" + localSid
			sidClient.UploadString(url, hashstr)
			// request real session id
		}
		__catch exc: Exception {
			Console.WriteLine(String.Format("Unable to retrieve a session ID from the server.\n") + exc)
			return 0
		}
		return sidClient.ResponseHeaders["X-Patch-Unique-Id"]
	}
	private static func GetStored(_ isSteam: Bool) -> String! {
		var loginInfo: WebClient! = WebClient()
		loginInfo.Headers.Add("user-agent", UserAgent)
		var reply: String! = loginInfo.DownloadString(String.Format("https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn=3&isft=0&issteam={0}", (isSteam ? 1 : 0)))
		var storedre: Regex! = Regex("\\t<\\s*input .* name=\"_STORED_\" value=\"(?<stored>.*)\">")
		var stored = storedre.Matches(reply)[0].Groups["stored"].Value
		return stored
	}

	public static func GetSid(_ username: String!, _ password: String!, _ otp: String!, _ isSteam: Bool) -> String! {
		__using let loginData = WebClient() {
			loginData.Headers.Add("user-agent", UserAgent)
			loginData.Headers.Add("Referer", String.Format("https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn=3&isft=0&issteam={0}", (isSteam ? 1 : 0)))
			loginData.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
			__try {
				var myCol:NameValueCollection= NameValueCollection()
				myCol.Add( "_STORED_", GetStored(isSteam) );
				myCol.Add( "sqexid", username  )
				myCol.Add( "password", password  )
				myCol.Add( "otppw", otp  )


				var response: UInt8[] = loginData.UploadValues("https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/login.send",myCol)
				var reply: String! = System.Text.Encoding.UTF8.GetString(response)
				// Console.WriteLine(reply);
				var sidre: Regex! = Regex("sid,(?<sid>.*),terms")
				var matches = sidre.Matches(reply)
				if matches.Count == 0 {
					if reply.Contains("ID or password is incorrect") {
						Console.WriteLine("Incorrect username or password.")
						return "BAD"
					}
				}
				var sid = sidre.Matches(reply)[0].Groups["sid"].Value
				return sid
			}
			__catch exc: Exception {
				Console.WriteLine(String.Format("Something failed when attempting to request a session ID.\n") + exc)
				return "BAD"
			}
		}
	}

	private static func GetLocalGamever(_ gamePath: String!) -> String! {
		__try {
			__using let sr = StreamReader(gamePath + "/game/ffxivgame.ver") {
				var line: String! = sr.ReadToEnd()
				return line
			}
		}
		__catch exc: Exception {
			Console.WriteLine("Unable to get local game version.\n" + exc)
			return "BAD"
		}
	}

	private static func GenerateHash(_ file: String!) -> String! {
		var filebytes: UInt8[] = File.ReadAllBytes(file)
		var hash = SHA1Managed().ComputeHash(filebytes)
		var hashstring: String! = String.Join("", hash.Select({ (b) in
			b.ToString("x2")
		}).ToArray())
		var length: Int64 = FileInfo(file).Length
		return length + "/" + hashstring
	}

	public static func GetGateStatus() -> Bool {
		__try {
			__using let client = WebClient() {
				var reply: String! = client.DownloadString("http://frontier.ffxiv.com/worldStatus/gate_status.json")
				return Convert.ToBoolean(Int32.Parse(reply[10].ToString()))
			}
		}
		__catch exc: Exception {
			Console.WriteLine("Failed getting gate status. " + exc)
			return false
		}
	}

	private static func InitiateSslTrust() {
	// Change SSL checks so that all checks pass, squares gamever server does strange things
		ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidationCallback({
			return true
		})
	}

	private static func GenerateUserAgent() -> String! {
		return String.Format(UserAgentTemplate, MakeComputerId())
	}

	private static func MakeComputerId() -> String! {
		var hashString = Environment.MachineName + Environment.UserName + Environment.OSVersion + Environment.ProcessorCount
		__using let sha1 = HashAlgorithm.Create("SHA1") {
			var bytes = UInt8[](count: 5)
			Array.Copy(sha1.ComputeHash(Encoding.Unicode.GetBytes(hashString)), 0, bytes, 1, 4)
			var checkSum = (-bytes[1] + bytes[2] + bytes[3] + bytes[4] as? UInt8)
			bytes[0] = checkSum
			return BitConverter.ToString(bytes).Replace("-", "").ToLower()
		}
	}
}