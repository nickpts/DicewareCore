using System;
using System.Collections.Generic;
using System.Text;

namespace DicewareCore
{
	public interface IDiceware
	{
		/// <summary>
		/// Generates a passphrase using the Diceware technique
		/// </summary>
		/// <param name="wordNo">number of words to generate, hard limit of 20</param>
		/// <param name="language">language </param>
		/// <param name="separator">word separator</param>
		string Create(int wordNo, Language language = Language.English, char separator = ' ');
	}
}
