using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;


namespace DicewareCore
{
	public class Converter
	{
		public static Dictionary<int, string> ExtractPairs(Language option)
		{
			switch (option)
			{
				case Language.Basque:
					return ParseWordList(Lists.basque);

				case Language.Catalan:
					return ParseWordList(Lists.catalan);

				//case Language.Chinese:
				//    return ParseWordList(Lists.pinyin);

				//case Language.Czech:
				//    return ParseWordList(Lists.czech);

				case Language.Danish:
					return ParseWordList(Lists.danish);

				case Language.Dutch:
					return ParseWordList(Lists.dutch);

				case Language.English:
					return ParseWordList(Lists.diceware_wordlist);

				case Language.Esperanto:
					return ParseWordList(Lists.esperanto);

				case Language.Estonian:
					return ParseWordList(Lists.estonian);

				case Language.French:
					return ParseWordList(Lists.francais_wordlist);

				case Language.German:
					return ParseWordList(Lists.german);

				case Language.Hungarian:
					return ParseWordList(Lists.hungarian_diceware);

				case Language.Italian:
					return ParseWordList(Lists.italian);

				case Language.Latin:
					return ParseWordList(Lists.diceware_latin_txt);

				case Language.Japanese:
					return ParseWordList(Lists.japanese);

				case Language.Spanish:
					return ParseWordList(Lists.spanish);

				case Language.Russian:
					return ParseWordList(Lists.russian);

				case Language.Swedish:
					return ParseWordList(Lists.swedish);

				case Language.Turkish:
					return ParseWordList(Lists.turkish);
			}

			throw new InvalidOperationException();
		}

		public static Dictionary<int, string> ParseWordList(byte[] input)
		{
			var result = new Dictionary<int, string>();

			try
			{
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
			}
			catch (FormatException)
			{

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
