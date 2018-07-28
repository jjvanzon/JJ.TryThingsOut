using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonoCross.Utilities
{
    public static class StringExtensions
    {
        public static string AppendPath(this string basepath, string relativepath)
        {
            if ( basepath == null || relativepath == null)
                return null;

            char[] chars = new char[2];
            chars[0] = '/';
            chars[1] = '\\';

            if (basepath.Contains('\\'))
                return Path.Combine(basepath.TrimEnd(chars), relativepath.TrimStart(chars)).Replace('/', '\\');
            return Path.Combine(basepath.TrimEnd(chars), relativepath.TrimStart(chars)).Replace('\\', '/');
        }

        public static string Clean(this string str)
        {
            return String.IsNullOrEmpty(str) ? string.Empty : str.ToLower();
        }
        public static string ToTitleCase(this string str)
        {
            return Regex.Replace(str, @"\w+", (m) =>
            {
                string tmp = m.Value;
                return char.ToUpper(tmp[0]) + tmp.Substring(1, tmp.Length - 1).ToLower();
            });
        }

        #region TryParse String Extensions

        public static int TryParseInt32(this string value)
        {
            return TryParse<int>(value, int.TryParse);
        }
        public static int TryParseInt32(this string value, int defaultValue)
        {
            return TryParse<int>(value, int.TryParse, defaultValue);
        }

        public static Int16 TryParseInt16(this string value)
        {
            return TryParse<Int16>(value, Int16.TryParse);
        }
        public static Int16 TryParseInt16(this string value, Int16 defaultValue)
        {
            return TryParse<Int16>(value, Int16.TryParse, defaultValue);
        }

        public static Int64 TryParseInt64(this string value)
        {
            return TryParse<Int64>(value, Int64.TryParse);
        }
        public static Int64 TryParseInt64(this string value, Int64 defaultValue)
        {
            return TryParse<Int64>(value, Int64.TryParse, defaultValue);
        }

        public static byte TryParseByte(this string value)
        {
            return TryParse<byte>(value, byte.TryParse);
        }
        public static byte TryParseByte(this string value, byte defaultValue)
        {
            return TryParse<byte>(value, byte.TryParse, defaultValue);
        }

        public static bool TryParseBoolean(this string value)
        {
            return TryParse<bool>(value, bool.TryParse);
        }
        public static bool TryParseBoolean(this string value, bool defaultValue)
        {
            return TryParse<bool>(value, bool.TryParse, defaultValue);
        }

        public static Single TryParseSingle(this string value)
        {
            return TryParse<Single>(value, Single.TryParse);
        }
        public static Single TryParseSingle(this string value, Single defaultValue)
        {
            return TryParse<Single>(value, Single.TryParse, defaultValue);
        }

        public static Double TryParseDouble(this string value)
        {
            return TryParse<Double>(value, Double.TryParse);
        }
        public static Double TryParseDouble(this string value, Double defaultValue)
        {
            return TryParse<Double>(value, Double.TryParse, defaultValue);
        }

        public static Decimal TryParseDecimal(this string value)
        {
            return TryParse<Decimal>(value, Decimal.TryParse);
        }
        public static Decimal TryParseDecimal(this string value, Decimal defaultValue)
        {
            return TryParse<Decimal>(value, Decimal.TryParse, defaultValue);
        }

        public static DateTime TryParseDateTime(this string value)
        {
            return TryParse<DateTime>(value, DateTime.TryParse);
        }
        public static DateTime TryParseDateTime(this string value, DateTime defaultValue)
        {
            return TryParse<DateTime>(value, DateTime.TryParse, defaultValue);
        }

        public static DateTime TryParseDateTimeUtc(this string value)
        {
            return TryParse<DateTime>(value, DateTime.TryParse).ToUniversalTime();
        }
        public static DateTime TryParseDateTimeUtc(this string value, DateTime defaultValue)
        {
            return TryParse<DateTime>(value, DateTime.TryParse, defaultValue).ToUniversalTime();
        }

        #region Private Members

        private delegate bool ParseDelegate<T>(string s, out T result);

        private static T TryParse<T>(this string value, ParseDelegate<T> parse) where T : struct
        {
            T result;
            parse(value, out result);
            return result;
        }

        private static T TryParse<T>(this string value, ParseDelegate<T> parse, T defaultValue) where T : struct
        {
            T result;
            //if ( string.IsNullOrEmpty( value ) )
            //    return defaultValue;

            if (!parse(value, out result))
                return defaultValue;
            else
                return result;
        }

        #endregion

        #endregion
    }
}
