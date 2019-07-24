using System;
using DicewareCore;
using NUnit.Framework;

namespace Tests
{
	public class DicewareTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_Zero()
		{
			var generator = new Diceware();
			
			Assert.Throws<ArgumentException>(() => generator.Create(0));
		}

		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_Less_Than_Zero()
		{
			var generator = new Diceware();
			
			Assert.Throws<ArgumentException>(() => generator.Create(-1));
		}


		[Test]
		public void Diceware_Generated_Throws_Argument_Exception_If_Number_Of_Words_More_Than_Twenty()
		{
			var generator = new Diceware();
			
			Assert.Throws<ArgumentException>(() => generator.Create(21));
		}

	}
}