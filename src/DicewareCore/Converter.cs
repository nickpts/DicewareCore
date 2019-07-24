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
			string inputSting; 

			switch (option)
			{
				case Language.English:
					inputSting = Encoding.UTF8.GetString(Lists.diceware_wordlist);
					break;
				case Language.Bulgarian:
					inputSting = Encoding.UTF8.GetString(Lists.bulgarian);
					break;
			}
			
			return new Dictionary<int, string>();
		}

	}
}
