using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DicewareCore
{
	public class Converter
	{
		public static Dictionary<int, string> ExtractPairs(Language option) => option switch
		{
			Language.Basque => ParseWordList(Lists.basque),
			Language.Catalan => ParseWordList(Lists.catalan),
			Language.Chinese => ParseWordList(Lists.pinyin),
			Language.Czech => ParseWordList(Lists.czech),
			Language.Danish => ParseWordList(Lists.danish),
			Language.Dutch => ParseWordList(Lists.dutch),
			Language.English => ParseWordList(Lists.diceware_wordlist),
			Language.Esperanto => ParseWordList(Lists.esperanto),
			Language.Estonian => ParseWordList(Lists.estonian),
			Language.French => ParseWordList(Lists.francais_wordlist),
			Language.German => ParseWordList(Lists.german),
			Language.Hungarian => ParseWordList(Lists.hungarian_diceware),
			Language.Italian => ParseWordList(Lists.italian),
			Language.Japanese => ParseWordList(Lists.japanese),
			Language.Latin => ParseWordList(Lists.diceware_latin_txt),
			Language.Spanish => ParseWordList(Lists.spanish),
			Language.Russian => ParseWordList(Lists.russian),
			Language.Swedish => ParseWordList(Lists.swedish),
			Language.Turkish => ParseWordList(Lists.turkish),
			_ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
		};

		public static Dictionary<int, string> ParseWordList(byte[] input)
		{
			var result = new Dictionary<int, string>();

			using var reader = new StreamReader(new MemoryStream(input));
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();

				if (string.IsNullOrEmpty(line))
					continue;

				var stringIndex = line.Substring(0, 5);

				if (!int.TryParse(stringIndex, out var index))
					continue; // this contains Hash/PGP information

				var word = line.Substring(5, line.Length - 5).Remove(0, 1).Trim();

				result.Add(index, word);
			}

			return result;
		}

		public static Dictionary<int, string> ParseWordList(string input)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(input);

			return ParseWordList(byteArray);
		}
	}
}