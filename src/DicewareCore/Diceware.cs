using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

using Ventura;
using Ventura.Interfaces;

namespace DicewareCore
{
	/*
	 * Diceware™
	 *
	 *
	 */
	public class Diceware: IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IRNGVenturaServiceProvider prng;

		public Diceware()
		{
			var seedStream = SerializeToStream(SHA256.Create());

			prng = RNGVenturaServiceProviderFactory.Create(
				seedStream, 
				Cipher.Aes, 
				ReseedEntropySourceGroup.Remote);
		}

		public Diceware(IRNGVenturaServiceProvider prng) => this.prng = prng;





		private static MemoryStream SerializeToStream(object objectType)
		{
			MemoryStream stream = new MemoryStream();
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, objectType);
			return stream;
		}

		public void Dispose()
		{
			prng?.Dispose();
		}
	}
}
