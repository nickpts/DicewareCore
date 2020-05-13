using System;
using System.IO;
using System.Text;
using Ventura;
using Ventura.Interfaces;

namespace DicewareCore
{
	public class Diceware: IDiceware, IDisposable
	{
		/// <summary>
		/// Pseudo-random number generator 
		/// </summary>
		private readonly IRNGVenturaProvider prng;

		/// <summary>
		/// Initialises Ventura and seed stream. By default Ventura is
		/// created with a seed from a SHA256 hash in a memory stream,
		/// AES and both local and remote entropy sources
		/// </summary>
		public Diceware() => prng = RNGVenturaProviderFactory.CreateSeeded(Cipher.Aes, ReseedEntropySourceGroup.Full);

		public Diceware(IRNGVenturaProvider prng) => this.prng = prng ?? throw new ArgumentNullException(nameof(prng));

        public string Create(int wordNo, Language language = Language.English, char separator = ' ')
		{
			if (wordNo <= 0 || wordNo >= 20)
				throw new ArgumentException(nameof(wordNo));

			var dictionary = Converter.ExtractPairs(language);
			var password = new StringBuilder();

			for (int i = 0; i < wordNo; i++)
			{
				int roll = MakeRoll();
				password.Append(dictionary[roll]);

                if (i < wordNo - 1)
	                password.Append(separator);
			}

			return password.ToString();
		}

		public void Dispose() => prng?.Dispose();

		#region Private implementation

		private int MakeRoll()
		{
			int index = 0;
			int multiplier = 10_000;

			for (int i = 0; i < Constants.LookupDigitLength; i++)
			{
				index += prng.Next(Constants.LowestPossibleRoll, Constants.HighestPossibleRoll + 1) * multiplier;
				multiplier /= 10;
			}

			return index;
		}

		#endregion
	}
}
