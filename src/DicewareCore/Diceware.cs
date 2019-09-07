using System;
using System.IO;
using System.Text;
using Ventura;
using Ventura.Interfaces;

namespace DicewareCore
{
	/* Diceware™
	 *
	 *
	 */
	public class Diceware: IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IRNGVenturaProvider prng;

		/// <summary>
		/// Initialises Ventura and seed stream. By default Ventura is
		/// created with a seed from a SHA256 hash in a memory stream,
		/// AES and both local and remote entropy sources
		/// </summary>
		public Diceware() => prng = RNGVenturaProviderFactory.Create(new MemoryStream(), Cipher.Aes, ReseedEntropySourceGroup.Local);
		
		public Diceware(IRNGVenturaProvider prng) => this.prng = prng ?? throw new ArgumentNullException(nameof(prng));

		/// <summary>
        /// Generates a passphrase using the Diceware technique
        /// </summary>
        /// <param name="wordNo">number of words to generate, hard limit of 20</param>
        /// <param name="language">language </param>
        /// <param name="separator">word separator</param>
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
                {
                    password.Append(separator);
                }
            }

			return password.ToString();
		}

		private int MakeRoll()
		{
			int index = 0;
			int multiplier = 10_000;

			for (int i = 0; i < 5; i++)
			{
				index += prng.Next(1, 7) * multiplier;
				multiplier /= 10;
			}

			return index;
		}

		public void Dispose()
		{
			prng?.Dispose();
		}

		#region Private implementation

        #endregion
	}
}
