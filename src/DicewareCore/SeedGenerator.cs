using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Ventura.Accumulator;
using Ventura.Accumulator.EntropyExtractors;
using Ventura.Accumulator.EntropyExtractors.Remote;
using Ventura.Interfaces;

namespace DicewareCore
{
	/// <summary>
	/// 
	/// </summary>
	public class SeedGenerator
	{
		public static byte[] GetSeed()
		{
			var newHash = ComputeNew(new byte[32], EnvironmentTickCount());
			newHash = ComputeNew(newHash, RngComputed());
			newHash = ComputeNew(newHash, ProcessEntropy());

			return ComputeNew(newHash, HotBitsEntropy());
		}

		public static byte[] ComputeNew(byte[] existingBytes, byte[] newBytes)
		{
			var concatenatedData = existingBytes.Concat(newBytes).ToArray();

			using (var algorithm = SHA512.Create())
			{
				return algorithm.ComputeHash(concatenatedData);
			}
		}

		public static byte[] EnvironmentTickCount() => BitConverter.GetBytes(Environment.TickCount);

		public static byte[] RngComputed()
		{
			var rngBytes = new byte[32];
			var provider = RNGCryptoServiceProvider.Create();
			provider.GetBytes(rngBytes);

			return rngBytes;
		}

		public static byte[] ProcessEntropy()
		{
			var extractor = new ProcessSeedExtractor(new StubEventEmitter {SourceNumber = 0});
			return extractor.Data().Invoke();
		}

		public static byte[] HotBitsEntropy()
		{
			var extractor = new HotBitsSeedExtractor(new StubEventEmitter() { SourceNumber = 1});
			return extractor.Data().Invoke();
		}
	}

	public class ProcessSeedExtractor : ProcessEntropyExtractor
	{
		public ProcessSeedExtractor(IEventEmitter eventEmitter) : base(eventEmitter)
		{
		}

		public Func<byte[]> Data() => base.ExtractEntropicData();
	}

	public class HotBitsSeedExtractor : HotBitsExtractor
	{
		public HotBitsSeedExtractor(IEventEmitter eventEmitter) : base(eventEmitter)
		{
		}

		public Func<byte[]> Data() => base.ExtractEntropicData();
	}

	public class StubEventEmitter : IEventEmitter {
		public Task<Event> Execute(Func<byte[]> extractionLogic)
		{
			throw new NotImplementedException();
		}

		public int SourceNumber { get; set; }
	}

}
