using System;
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
			IRNGVenturaServiceProvider prng = null;

			Assert.Throws<ArgumentNullException>(() => new Diceware(prng));
		}

		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_Zero()
		{
			Assert.Throws<ArgumentException>(() => generator.Create(0));
		}

		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_Less_Than_Zero()
		{
			Assert.Throws<ArgumentException>(() => generator.Create(-1));
		}


		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_More_Than_Twenty()
		{
			Assert.Throws<ArgumentException>(() => generator.Create(21));
		}

		[Test]
		public void Diceware_Create_Generated_Correct_Passphrase()
		{
			var pass = generator.Create(19);
		}
	}
}