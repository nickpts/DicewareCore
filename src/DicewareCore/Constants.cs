namespace DicewareCore
{
	public enum Language
	{
		Basque,
		Catalan,
		Chinese,
		Czech,
		Danish,
		Dutch,
		English,
		Esperanto,
		Estonian,
		French,
		German,
		Hungarian,
		Italian,
		Japanese,
		Latin,
		Spanish,
		Russian,
		Swedish,
		Turkish
	}

	public static class Constants
	{
		/// <summary>
		/// The look up digit length in the Diceware Word List
		/// </summary>
		public const int LookupDigitLength = 5;

		/// <summary>
		/// Lower possible roll value according to Diceware rules
		/// </summary>
		public const int LowestPossibleRoll = 1;

		/// <summary>
		/// Highest possible roll value according to Diceware rules
		/// </summary>
		public const int HighestPossibleRoll = 6;
	}
}