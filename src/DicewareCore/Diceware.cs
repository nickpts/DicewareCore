using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
		private readonly IRNGVenturaServiceProvider prng;

		/// <summary>
		/// Initialises Ventura and seed stream. By default Ventura is
		/// created with a seed from a SHA256 hash in a memory stream,
		/// AES and both local and remote entropy sources
		/// </summary>
		public Diceware()
		{
			var seedStream = SerializeToStream(SeedGenerator.GetSeed());

			prng = RNGVenturaServiceProviderFactory.Create(
				seedStream, 
				Cipher.Aes, 
				ReseedEntropySourceGroup.Full);
		}

		public Diceware(IRNGVenturaServiceProvider prng)
		{
			if (prng == null)
				throw new ArgumentNullException(nameof(prng));

			this.prng = prng;
		}

		/// <summary>
		/// Generates a passphrase using the Diceware technique
		/// </summary>
		/// <param name="wordNo">number of words to generated</param>
		/// <param name="language">language </param>
		public string Create(int wordNo, Language language = Language.English)
		{
			if (wordNo <= 0 || wordNo >= 20)
				throw new ArgumentException(nameof(wordNo));

			var dictionary = Converter.ExtractPairs(language);
			var password = new StringBuilder();

			for (int i = 0; i < wordNo; i++)
			{
				int roll = MakeRoll();
				password.Append(dictionary[roll]);
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

		private MemoryStream SerializeToStream(object objectType)
		{
			MemoryStream stream = new MemoryStream();
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, objectType);
			return stream;
		}

		#endregion
	}
}
