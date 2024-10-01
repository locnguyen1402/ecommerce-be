using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Shared.Libs.Extensions;

public static class StringExtensions
{
    private static readonly Dictionary<string, string> foreignCharacters = new()
    {
        { "äæǽ", "ae" },
        { "öœ", "oe" },
        { "ü", "ue" },
        { "Ä", "Ae" },
        { "Ü", "Ue" },
        { "Ö", "Oe" },
        { "ÀÁÂÃÄÅǺĀĂĄǍΑΆẢẠẦẪẨẬẰẮẴẲẶА", "A" },
        { "àáâãåǻāăąǎªαάảạầấẫẩậằắẵẳặа", "a" },
        { "Б", "B" },
        { "б", "b" },
        { "ÇĆĈĊČ", "C" },
        { "çćĉċč", "c" },
        { "Д", "D" },
        { "д", "d" },
        { "ÐĎĐΔ", "Dj" },
        { "ðďđδ", "dj" },
        { "ÈÉÊËĒĔĖĘĚΕΈẼẺẸỀẾỄỂỆЕЭ", "E" },
        { "èéêëēĕėęěέεẽẻẹềếễểệеэ", "e" },
        { "Ф", "F" },
        { "ф", "f" },
        { "ĜĞĠĢΓГҐ", "G" },
        { "ĝğġģγгґ", "g" },
        { "ĤĦ", "H" },
        { "ĥħ", "h" },
        { "ÌÍÎÏĨĪĬǏĮİΗΉΊΙΪỈỊИЫ", "I" },
        { "ìíîïĩīĭǐįıηήίιϊỉịиыї", "i" },
        { "Ĵ", "J" },
        { "ĵ", "j" },
        { "ĶΚК", "K" },
        { "ķκк", "k" },
        { "ĹĻĽĿŁΛЛ", "L" },
        { "ĺļľŀłλл", "l" },
        { "М", "M" },
        { "м", "m" },
        { "ÑŃŅŇΝН", "N" },
        { "ñńņňŉνн", "n" },
        { "ÒÓÔÕŌŎǑŐƠØǾΟΌΩΏỎỌỒỐỖỔỘỜỚỠỞỢО", "O" },
        { "òóôõōŏǒőơøǿºοόωώỏọồốỗổộờớỡởợо", "o" },
        { "П", "P" },
        { "п", "p" },
        { "ŔŖŘΡР", "R" },
        { "ŕŗřρр", "r" },
        { "ŚŜŞȘŠΣС", "S" },
        { "śŝşșšſσςс", "s" },
        { "ȚŢŤŦτТ", "T" },
        { "țţťŧт", "t" },
        { "ÙÚÛŨŪŬŮŰŲƯǓǕǗǙǛŨỦỤỪỨỮỬỰУ", "U" },
        { "ùúûũūŭůűųưǔǖǘǚǜυύϋủụừứữửựу", "u" },
        { "ÝŸŶΥΎΫỲỸỶỴЙ", "Y" },
        { "ýÿŷỳỹỷỵй", "y" },
        { "В", "V" },
        { "в", "v" },
        { "Ŵ", "W" },
        { "ŵ", "w" },
        { "ŹŻŽΖЗ", "Z" },
        { "źżžζз", "z" },
        { "ÆǼ", "AE" },
        { "ß", "ss" },
        { "Ĳ", "IJ" },
        { "ĳ", "ij" },
        { "Œ", "OE" },
        { "ƒ", "f" },
        { "ξ", "ks" },
        { "π", "p" },
        { "β", "v" },
        { "μ", "m" },
        { "ψ", "ps" },
        { "Ё", "Yo" },
        { "ё", "yo" },
        { "Є", "Ye" },
        { "є", "ye" },
        { "Ї", "Yi" },
        { "Ж", "Zh" },
        { "ж", "zh" },
        { "Х", "Kh" },
        { "х", "kh" },
        { "Ц", "Ts" },
        { "ц", "ts" },
        { "Ч", "Ch" },
        { "ч", "ch" },
        { "Ш", "Sh" },
        { "ш", "sh" },
        { "Щ", "Shch" },
        { "щ", "shch" },
        { "ЪъЬь", "" },
        { "Ю", "Yu" },
        { "ю", "yu" },
        { "Я", "Ya" },
        { "я", "ya" },
    };

    public static string ToSnakeCase(this string input, bool useUpperCase = false)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var startUnderscores = Regex.Match(input, @"^_+");
        var slug = startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2");

        if (!useUpperCase)
            return slug.ToLower();

        return slug.ToUpper();
    }

    public static string ToSnakeCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var builder = new StringBuilder();
        builder.Append(char.ToLowerInvariant(text[0]));

        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                builder.Append('_');
                builder.Append(char.ToLowerInvariant(c));
            }
            else
            {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }

    public static string ToCamelCase(this string input)
    {
        var text = input.Replace("_", "");

        if (text.Length == 0) return string.Empty;

        text = Regex.Replace(text, "([A-Z])([A-Z]+)($|[A-Z])",
            m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);

        text = char.ToLower(text[0]) + text[1..];

        return text;
    }

    public static string ToSlug(this string text)
    {
        string slug = RemoveAccent(RemoveDiacritics(text)); // Remove Accent & Diacritics chars

        slug = slug.ToLower();                              // Lowercase
        slug = Regex.Replace(slug, @"\s+", " ").Trim();     // convert multiple spaces into one space
        slug = Regex.Replace(slug, @"\s", "-");             // hyphens

        return slug;
    }

    public static string ToASCII(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        string slug = RemoveAccent(RemoveDiacritics(text)); // Remove Accent & Diacritics chars

        slug = Regex.Replace(slug, @"\s+", " ").Trim();     // convert multiple spaces into one space

        return slug;
    }

    private static string RemoveAccent(this string text)
    {
        byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);

        return Encoding.ASCII.GetString(bytes);
    }

    public static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        ReadOnlySpan<char> normalizedString = text.Normalize(NormalizationForm.FormKD);

        // Use stackalloc for small strings (less than 1000 characters), otherwise heap allocation.
        Span<char> span = text.Length < 1000
            ? stackalloc char[text.Length] // Stack allocation for smaller strings
            : new char[text.Length];       // Heap allocation for larger strings

        int i = 0;

        foreach (char c in normalizedString)
        {
            bool replaced = false;

            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
                continue;

            foreach (var entry in foreignCharacters)
            {
                if (entry.Key.IndexOf(c) != -1)
                {
                    foreach (char replacement in entry.Value)
                    {
                        span[i++] = replacement;
                    }
                    replaced = true;
                    break;
                }
            }

            if (!replaced)
            {
                span[i++] = c;
            }
        }

        return new string(span[..i]).Normalize(NormalizationForm.FormC);
    }

    // private static string RemoveDiacritics(this string text)
    // {
    //     var normalizedString = text.Normalize(NormalizationForm.FormD);
    //     var stringBuilder = new StringBuilder();

    //     foreach (var c in normalizedString)
    //     {
    //         var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

    //         switch (unicodeCategory)
    //         {
    //             case UnicodeCategory.LowercaseLetter:
    //             case UnicodeCategory.UppercaseLetter:
    //             case UnicodeCategory.OtherLetter:
    //             case UnicodeCategory.DecimalDigitNumber:
    //                 // Keep letters and digits
    //                 stringBuilder.Append(c);
    //                 break;
    //             case UnicodeCategory.NonSpacingMark:
    //                 // Remove diacritics
    //                 break;
    //             default:
    //                 // Replace all other chars with dash
    //                 stringBuilder.Append(' ');
    //                 break;
    //         }
    //     }

    //     return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    // }

    public static bool Like(this string value, string pattern)
    {
        int patternIndex = 0;
        int valueIndex = 0;
        int patternLength = pattern.Length;
        int valueLength = value.Length;

        while (valueIndex < valueLength && patternIndex < patternLength)
        {
            if (pattern[patternIndex] == '%')
            {
                // Handle '%' as a wildcard (matches any sequence of characters).
                patternIndex++;
                if (patternIndex == patternLength)
                {
                    // If '%' is at the end of the pattern, it matches anything remaining in the value.
                    return true;
                }

                char nextChar = pattern[patternIndex];

                while (valueIndex < valueLength && value[valueIndex] != nextChar)
                {
                    valueIndex++;
                }
            }
            else if (pattern[patternIndex] == '_')
            {
                // Handle '_' as a wildcard (matches any single character).
                patternIndex++;
                valueIndex++;
            }
            else if (pattern[patternIndex] == value[valueIndex])
            {
                patternIndex++;
                valueIndex++;
            }
            else
            {
                return false; // Characters don't match.
            }
        }

        // If we reached the end of both the value and the pattern, it's a match.
        if (valueIndex == valueLength && patternIndex == patternLength)
        {
            return true;
        }

        // If we didn't reach the end of the pattern, it's not a match.
        return false;
    }

    public static bool ILike(this string value, string pattern)
    {
        int patternIndex = 0;
        int valueIndex = 0;
        int patternLength = pattern.Length;
        int valueLength = value.Length;

        while (valueIndex < valueLength && patternIndex < patternLength)
        {
            if (pattern[patternIndex] == '%')
            {
                // Handle '%' as a wildcard (matches any sequence of characters).
                patternIndex++;
                if (patternIndex == patternLength)
                {
                    // If '%' is at the end of the pattern, it matches anything remaining in the value.
                    return true;
                }

                char nextChar = pattern[patternIndex];

                while (valueIndex < valueLength && char.ToLower(value[valueIndex]) != char.ToLower(nextChar))
                {
                    valueIndex++;
                }
            }
            else if (pattern[patternIndex] == '_')
            {
                // Handle '_' as a wildcard (matches any single character).
                patternIndex++;
                valueIndex++;
            }
            else if (char.ToLower(pattern[patternIndex]) == char.ToLower(value[valueIndex]))
            {
                patternIndex++;
                valueIndex++;
            }
            else
            {
                return false; // Characters don't match.
            }
        }

        // If we reached the end of both the value and the pattern, it's a match.
        if (valueIndex == valueLength && patternIndex == patternLength)
        {
            return true;
        }

        // If we didn't reach the end of the pattern, it's not a match.
        return false;
    }

    public static bool ToBoolean(this string value)
    {
        switch (value.Trim().ToLower())
        {
            case "true":
                return true;
            case "t":
                return true;
            case "1":
                return true;
            case "0":
                return false;
            case "false":
                return false;
            case "f":
                return false;
            default:
                return false;
        }
    }

    public static string ToGenerateRandomSlug(this string text)
    {
        var slug = text.ToSlug();

        Random rnd = new Random();
        int num = rnd.Next(100, 1000);

        return slug + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + num.ToString();
    }

    public static string ToGenerateOrderNumber(this string? text)
    {
        Random rnd = new Random();
        int num = rnd.Next(10000, 100000);
        if (string.IsNullOrEmpty(text))
            return DateTime.Now.ToString("yyyyMMddhhmmss") + num.ToString();
        return text + DateTime.Now.ToString("yyyyMMddhhmmss") + num.ToString();
    }

    public static string ToGenerateRandomCode()
    {
        string[] randomChars = [
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // UPPERCASE
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
            ];

        Random rand = new(Environment.TickCount);
        List<char> chars = new();

        chars.Insert(rand.Next(0, chars.Count),
            randomChars[0][rand.Next(0, randomChars[0].Length)]);

        chars.Insert(rand.Next(0, chars.Count),
            randomChars[1][rand.Next(0, randomChars[1].Length)]);

        chars.Insert(rand.Next(0, chars.Count),
            randomChars[2][rand.Next(0, randomChars[2].Length)]);

        for (int i = chars.Count; i < SchemaConstants.MAX_CODE_LENGTH ||
            chars.Distinct().Count() < SchemaConstants.MAX_CODE_LENGTH; i++)
        {
            string rcs = randomChars[rand.Next(0, randomChars.Length)];
            chars.Insert(rand.Next(0, chars.Count),
                rcs[rand.Next(0, rcs.Length)]);
        }

        return new string(chars.ToArray());
    }
}