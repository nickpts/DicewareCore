using System;
using System.Diagnostics;
using System.Threading;
using DicewareCore;
using NUnit.Framework;
using Ventura.Interfaces;

namespace Tests
{
	public class DicewareTests
	{
		Diceware generator = new Diceware();

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Diceware_Constructor_Throws_Exception_If_InputNull()
		{
			IRNGVenturaProvider prng = null;

			Assert.Throws<ArgumentNullException>(() => new Diceware(prng));
		}

		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_Zero() =>
			Assert.Throws<ArgumentException>(() => generator.Create(0));

		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_Less_Than_Zero() =>
			Assert.Throws<ArgumentException>(() => generator.Create(-1));

		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_More_Than_Twenty() =>
			Assert.Throws<ArgumentException>(() => generator.Create(21));

		[Test]
		public void Diceware_Create_Includes_Specified_Separator()
		{
			var input = '!';
			generator.Create(5, Language.English, input).Contains(input);
		}

		[Test]
		public void Diceware_Create_Contains_Specified_Number_Of_Words()
		{
			var numberOfWords = 5;

			var count = generator.Create(5, Language.English, '-').Split('-').Length;

			Assert.AreEqual(numberOfWords, count);
		}


		[Test, Explicit]
		public void Diceware_Create_Generates_Correct_Passphrase()
		{
			foreach (var value in Enum.GetValues(typeof(Language)))
			{
				Debug.WriteLine(value.ToString());
				for (int i = 0; i < 100; i++)
				{
					var pass = generator.Create(10, (Language)value, '-');
					Debug.WriteLine(pass);
					Thread.Sleep(250);
				}
			}
		}
	}
}