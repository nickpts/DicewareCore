using System;
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
			var seedStream = SerializeToStream(SHA256.Create());

			prng = RNGVenturaServiceProviderFactory.Create(
				seedStream, 
				Cipher.Aes, 
				ReseedEntropySourceGroup.Full);
		}

		public Diceware(IRNGVenturaServiceProvider prng) => this.prng = prng;


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
