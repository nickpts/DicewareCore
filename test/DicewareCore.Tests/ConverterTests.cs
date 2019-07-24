using DicewareCore;
using NUnit.Framework;

namespace Tests
{
	public class ConverterTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void Converter_Returns_Proper_WordList()
		{
			var list = Converter.ExtractPairs(Language.Spanish);

		}
	}
}