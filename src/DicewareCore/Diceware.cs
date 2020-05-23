using System;
using System.Security.Cryptography;
using System.Text;

namespace DicewareCore
{
    public class Diceware: IDiceware, IDisposable
    {
        /// <summary>
        /// Pseudo-random number generator 
        /// </summary>
        private readonly RandomNumberGenerator prng;

        /// <summary>
        /// Initialises RNG
        /// </summary>
        public Diceware() => prng = new RNGCryptoServiceProvider();

        public Diceware(RandomNumberGenerator prng) => this.prng = prng ?? throw new ArgumentNullException(nameof(prng));
        
        public string Create(int wordNo, Language language = Language.English, char separator = ' ')
        {
            if (wordNo <= 0 || wordNo >= 20)
                throw new ArgumentException(nameof(wordNo));

            var dictionary = Converter.ExtractPairs(language);
            var password = new StringBuilder();

            for (int i = 0; i < wordNo; i++)
            {
                int roll = MakeRoll();
                password.Append(dictionary[roll]);

                if (i < wordNo - 1)
                    password.Append(separator);
            }

            return password.ToString();
        }

        public void Dispose() => prng?.Dispose();

        #region Private implementation

        private int MakeRoll()
        {
            int index = 0;
            int multiplier = 10_000;

            for (int i = 0; i < Constants.LookupDigitLength; i++)
            {
                index += Next(Constants.LowestPossibleRoll, Constants.HighestPossibleRoll + 1) * multiplier;
                multiplier /= 10;
            }

            return index;
        }

        private int Next(int min, int max)
        {
            var data = new byte[4];
            prng.GetBytes(data);

            int num = Math.Abs(BitConverter.ToInt32(data, 0));

            return (num % (max - min)) + min;
        }

        #endregion
    }
}
