using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DicewareCore
{
	public class SeedGenerator
	{
		public static byte[] GetSeed()
		{
			var newHash = ComputeNew(new byte[32], EnvironmentTickCount());
			newHash = ComputeNew(newHash, RngComputed());

			return ComputeNew(newHash, ProcessEntropy());
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
			Process process = Process.GetCurrentProcess();
			process.Refresh();

			var pTime = BitConverter.GetBytes(process.TotalProcessorTime.Ticks).Take(6);
			var uTime = BitConverter.GetBytes(process.UserProcessorTime.Ticks).Take(6);
			var memory = BitConverter.GetBytes(process.VirtualMemorySize64).Take(6);
			var pMemory = BitConverter.GetBytes(process.PagedMemorySize64).Take(6);
			var wMemory = BitConverter.GetBytes(process.WorkingSet64).Take(6);

			var total = pTime.Concat(uTime).Concat(memory).Concat(pMemory).Concat(wMemory).ToArray();

			return total;
		}
	}
}
