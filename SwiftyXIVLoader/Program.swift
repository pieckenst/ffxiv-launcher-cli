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

public class Program {
	public static func ReadPassword() -> String! {
		var password: String! = ""
		var info: ConsoleKeyInfo! = Console.ReadKey(true)
		while info.Key != ConsoleKey.Enter {
			if info.Key != ConsoleKey.Backspace {
				Console.Write("*")
				password = password + info.KeyChar
			} else {
				if info.Key == ConsoleKey.Backspace {
					if !String.IsNullOrEmpty(password) {
						//  remove one character from the list of password characters
						password = password.Substring(0, password.Length - 1)
						//  get the location of the cursor
						var pos: Int32 = Console.CursorLeft
						//  move the cursor to the left by one character
						Console.SetCursorPosition(pos - 1, Console.CursorTop)
						//  replace it with space
						Console.Write(" ")
						//  move the cursor to the left by one character again
						Console.SetCursorPosition(pos - 1, Console.CursorTop)
					}
				}
			}
			info = Console.ReadKey(true)
		}//  add a new line because user pressed enter at the end of their password
		Console.WriteLine()
		return password
	}
	static func Main() {
		Console.Title = "XIVLOADER"
		Console.OutputEncoding = System.Text.Encoding.Unicode
		var arr = ["                                             ", " __  _______   ___                 _         ", " \\ \\/ /_ _\\ \\ / / |   ___  __ _ __| |___ _ _ ", "  >  < | | \\ V /| |__/ _ \\/ _` / _` / -_) \'_| ", " /_/\\_\\___| \\_/ |____\\___/\\__,_\\__,_\\___|_|  ", "                                             "]

		Console.WriteLine("\n\n")
		for line in arr {
			Console.WriteLine(line)
		}
		// Console.WriteLine("FFXIV Launcher "); // it has to begin somewhere lol
		Console.WriteLine("0 - Japanese , 1 - English , 2 - German , 3 - French , 4 - Russian ( The client will still be in english)")
		Console.Write("Enter your language - ")
		var language: Int32 = Int32.Parse(Console.ReadLine())
		if language == 0 {
			JapaneseLaunchMethod.JapanLaunch(language)
		}
		if language == 1 {
			LaunchMethods.EnglishLaunch(language)
		}
		if language == 2 {
			GermanLaunchMethod.GermanLaunch(language)
		}
		if language == 3 {
			FrenchLaunchMethod.FrenchLaunch(language)
		}
		if language == 4 {
			RussianLaunchMethod.RussianLaunch(language)
		}
	}


}