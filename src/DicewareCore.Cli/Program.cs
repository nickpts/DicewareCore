using System;
using System.IO;
using System.Threading;
using CommandLine;

namespace DicewareCore.Cli
{
	class Program
	{
		public static void Main(string[] args)
		{
			Parser.Default.ParseArguments<Options>(args)
				.MapResult(
					(Options opts) =>
					{
						using var dice = new Diceware();

						var pass = dice.Create(opts.WordCount, opts.Language, opts.Separator);
						Console.WriteLine(pass);

						return 0;
					},
					errs => 1);

			Console.ReadLine();

		}
	}
}
