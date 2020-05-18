using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using CommandLine;

namespace DicewareCore.Cli
{
	[Verb("dice", HelpText = "Generates a passphrase according to the diceware method")]
	public class Options
	{
		[Option('w', "words", Required = true, HelpText = "How many words should the passphrase contain")]
		public int WordCount { get; set; }

		[Option('l', "language", Required = false, HelpText = "Which language to use for the passphrase")]
		public Language Language { get; set; }

		[Option('s', "separator", Required = false, HelpText = "Character to use as separator between words")]
		public char Separator { get; set; }
	}
}
