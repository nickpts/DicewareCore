using System;
using DicewareCore;
using NUnit.Framework;

namespace Tests
{
	public class ConverterTests
	{
		[Test]
		public void Converter_Throws_Exception_If_Language_Not_Found() => 
			Assert.Throws<ArgumentOutOfRangeException>(() => Converter.ExtractPairs((Language)600));
		
	}
}