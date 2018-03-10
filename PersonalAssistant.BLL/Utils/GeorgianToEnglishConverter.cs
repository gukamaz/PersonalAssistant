using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.BLL.Utils
{
    public class GeorgianToEnglishConverter : ILangugageConverter
    {
        readonly IDictionary<char, string> LETTERS = new Dictionary<char, string>
        {
            { 'ა', "a" }, { 'ბ', "b" }, { 'გ', "g" }, { 'დ', "d" },
            { 'ე', "e" }, { 'ვ', "v" }, { 'ზ', "z" }, { 'თ', "t" },
            { 'ი', "i" }, { 'კ', "k" }, { 'ლ', "l" }, { 'მ', "m" },
            { 'ნ', "n" }, { 'ო', "o" }, { 'პ', "p" }, { 'ჟ', "j" },
            { 'რ', "r" }, { 'ს', "s" }, { 'ტ', "t" }, { 'უ', "u" },
            { 'ფ', "f" }, { 'ქ', "q" }, { 'ღ', "g" }, { 'ყ', "k" },
            { 'შ', "sh" }, { 'ც', "c" }, { 'ჩ', "ch" }, { 'ძ', "dz" },
            { 'წ', "ts" }, { 'ჭ', "tch" }, { 'ხ', "kh" }, { 'ჯ', "j" },
            { 'ჰ', "h" },
        };

        public string Convert(string text)
        {
            StringBuilder result = new StringBuilder();
            foreach (var letter in text)
            {
                if (LETTERS.ContainsKey(letter))
                    result.Append(LETTERS[letter]);
                else
                    result.Append(letter);
            }

            return result.ToString();
        }
    }
}
