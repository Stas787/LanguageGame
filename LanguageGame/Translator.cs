using System;
using System.Text;

namespace LanguageGame
{
    public static class Translator
    {
        /// <summary>
        /// Translates from English to Pig Latin. Pig Latin obeys a few simple following rules:
        /// - if word starts with vowel sounds, the vowel is left alone, and most commonly 'yay' is added to the end;
        /// - if word starts with consonant sounds or consonant clusters, all letters before the initial vowel are
        ///   placed at the end of the word sequence. Then, "ay" is added.
        /// Note: If a word begins with a capital letter, then its translation also begins with a capital letter,
        /// if it starts with a lowercase letter, then its translation will also begin with a lowercase letter.
        /// </summary>
        /// <param name="phrase">Source phrase.</param>
        /// <returns>Phrase in Pig Latin.</returns>
        /// <exception cref="ArgumentException">Thrown if phrase is null or empty.</exception>
        /// <example>
        /// "apple" -> "appleyay"
        /// "Eat" -> "Eatyay"
        /// "explain" -> "explainyay"
        /// "Smile" -> "Ilesmay"
        /// "Glove" -> "Oveglay".
        /// </example>
        public static string TranslateToPigLatin(string phrase)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrWhiteSpace(phrase))
            {
                throw new ArgumentException($"{nameof(phrase)} phrase can't be null or empty");
            }

            StringBuilder notWard = new StringBuilder();
            StringBuilder word = new StringBuilder();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < phrase.Length; i++)
            {
                if ((!char.IsLetter(phrase[i])) && phrase[i] != '’')
                {
                    notWard.Append(phrase[i]);
                }
                else if (i + 1 < phrase.Length && char.IsLetter(phrase[i]) && (char.IsLetter(phrase[i + 1]) || phrase[i + 1] == '’'))
                {
                    word.Append(phrase[i]);
                }
                else if (phrase[i] == '’')
                {
                    word.Append(phrase[i]);
                }
                else
                {
                    word.Append(phrase[i]);
                    string wordPiglatin = TranslateTo(word.ToString());
                    result.Append(notWard).Append(wordPiglatin);
                    notWard.Clear();
                    word.Clear();
                }
            }

            if (!char.IsLetter(phrase[^1]))
            {
                result.Append(phrase[^1]);
            }

            return result.ToString();
        }

        private static void FirstToUp(StringBuilder result)
        {
            for (int z = 1; z < result.Length; z++)
            {
                if (result[z] != '\'')
                {
                    result[z] = char.ToLower(result[z], System.Globalization.CultureInfo.CurrentCulture);
                }
            }

            result[0] = char.ToUpper(result[0], System.Globalization.CultureInfo.CurrentCulture);
        }

        private static string TranslateTo(string word)
        {
            StringBuilder result = new StringBuilder();
            string pigLatinAdditionVowel = "yay";
            string pigLatinAdditionConsonent = "ay";
            char[] vowels = new char[] { 'a', 'o', 'e', 'i', 'u', 'A', 'O', 'E', 'I', 'U' };
            bool sizeOfFirstLetter = char.IsUpper(word[0]);
            int index = word.IndexOfAny(vowels);
            if (index == 0)
            {
                result.Append(word).Append(pigLatinAdditionVowel);              
            }
            else
            {
                if (index >= 0)
                {
                    result.Append(word[index..]).Append(word[0..index]).Append(pigLatinAdditionConsonent);
                }
            }

            if (sizeOfFirstLetter)
            {
                FirstToUp(result);
            }

            if (string.IsNullOrEmpty(result.ToString()))
            {
                result.Append(word).Append(pigLatinAdditionConsonent);
            }

            return result.ToString();
        }
    }
}
