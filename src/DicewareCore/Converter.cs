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
			string inputString;
			byte[] stream = null;

			switch (option)
			{
				case Language.Basque:
					inputString = Lists.basque;
					break;
				case Language.Catalan:
					inputString = Lists.catalan;
					break;
				case Language.Chinese:
					inputString = Encoding.UTF8.GetString(Lists.pinyin);
					break;
				case Language.Czech:
					inputString = Lists.czech;
					break;
				case Language.Danish:
					inputString = Lists.danish;
					break;
				case Language.Dutch:
					inputString = Lists.dutch;
					break;
				case Language.English:
					stream = Lists.diceware_wordlist;
					break;
				case Language.Esperanto:
					inputString = Lists.esperanto;
					break;
				case Language.Estonian:
					inputString = Lists.estonian;
					break;
				case Language.French:
					inputString = Encoding.UTF8.GetString(Lists.francais_wordlist);
					break;
				case Language.German:
					inputString = Lists.german;
					break;
				case Language.Hungarian:
					inputString = Lists.hungarian_diceware;
					break;
				case Language.Italian:
					inputString = Lists.italian;
					break;
				case Language.Latin:
					inputString = Encoding.UTF8.GetString(Lists.diceware_latin_txt);
					break;
				case Language.Japanese:
					inputString = Lists.japanese;
					break;
				case Language.Spanish:
					inputString = Lists.spanish;
					break;
				case Language.Russian:
					inputString = Lists.russian;
					break;
				case Language.Swedish:
					inputString = Lists.swedish;
					break;
				case Language.Turkish:
					inputString = Lists.turkish;
					break;
			}

			return ParseWordList(stream);
		}

		public static Dictionary<int, string> ParseWordList(byte[] input)
		{
			var result = new Dictionary<int, string>();

			using (var reader = new StreamReader(new MemoryStream(input)))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();

					var index = line.Substring(0, 5);
					var word = line.Substring(5, line.Length - 5).Remove(0, 1).Trim();

					result.Add(int.Parse(index), word);
				}
			}

			return result;
		}
	}
}
