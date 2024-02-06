using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SecurityDriven.Core;

namespace DicewarePassphrase;

internal class Program
{
	private static void Main(int words = 4, int phrases = 10, bool requireSpecial = true)
	{
		Console.WriteLine($"Words: {words}, Phrases: {phrases}, Require Special Characters: {requireSpecial}");
		var wordList = LoadWordList();

		foreach (var phraseNum in Enumerable.Range(1, phrases))
		{
			while (true)
			{
				var passphrase = string.Join(
					' ',
					Enumerable.Range(0, words).Select(_ => wordList[CryptoRandom.Shared.Next(wordList.Count)])
				);

				if (requireSpecial)
				{
					var containsNumbers = passphrase.Any(char.IsDigit);
					var containsSpecial = !passphrase.Where(c => c != ' ').All(char.IsLetterOrDigit);
					if (!containsNumbers || !containsSpecial)
					{
						continue;
					}
				}

				Console.Out.WriteLine(passphrase);
				break;
			}
		}

		static IReadOnlyList<string> LoadWordList() =>
			File.ReadLines("diceware-sv.txt")
				.TakeWhile(line => !string.IsNullOrWhiteSpace(line))
				.Select(line => line.Split(' ')[1])
				.ToList();
	}
}
