using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

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
			var ticks = System.Environment.TickCount;
			var seed = SHA256.Create().ComputeHash(BitConverter.GetBytes(ticks));

			var seedStream = SerializeToStream(seed);

			prng = RNGVenturaServiceProviderFactory.Create(
				seedStream, 
				Cipher.Aes, 
				ReseedEntropySourceGroup.Full);
		}

		public Diceware(IRNGVenturaServiceProvider prng) => this.prng = prng;

		/// <summary>
		/// Generates a passphrase using the Diceware technique
		/// </summary>
		/// <param name="wordNo">number of words to generated</param>
		/// <param name="language">language </param>
		public string Create(int wordNo, Language language = Language.English)
		{
			if (wordNo <= 0 || wordNo >= 20)
				throw new ArgumentException(nameof(wordNo));

			return default;
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
