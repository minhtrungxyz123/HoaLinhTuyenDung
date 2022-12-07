using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace TuyenDung.Core.Models
{
    public static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string EmptyNull(this string value)
        {
            return (value ?? string.Empty).Trim();
        }

        [DebuggerStepThrough]
        public static string[] SplitSafe(this string value, string separator)
        {
            if (string.IsNullOrEmpty(value))
                return new string[0];

            // Do not use separator.IsEmpty() here because whitespace like " " is a valid separator.
            // an empty separator "" returns array with value.
            if (separator == null)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var c = value[i];
                    if (c == ';' || c == ',' || c == '|')
                    {
                        return value.Split(new char[] { c }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (c == '\r' && (i + 1) < value.Length & value[i + 1] == '\n')
                    {
                        return value.GetLines(false, true).ToArray();
                    }
                }

                return new string[] { value };
            }
            else
            {
                return value.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public static IEnumerable<string> GetLines(this string input, bool trimLines = false, bool removeEmptyLines = false)
        {
            if (input.IsEmpty())
            {
                yield break;
            }

            using (var sr = new StringReader(input))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (trimLines)
                    {
                        line = line.Trim();
                    }

                    if (removeEmptyLines && IsEmpty(line))
                    {
                        continue;
                    }

                    yield return line;
                }
            }
        }

        public static IDictionary<string, object> ObjectToDictionary(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return TuyenDung.Core.FormFile.FastProperty.ObjectToDictionary(
                obj,
                key => key.Replace("_", "-").Replace("@", ""));
        }

        [DebuggerStepThrough]
        public static void Dump(this string value, bool appendMarks = false)
        {
            Debug.WriteLine(value);
            Debug.WriteLineIf(appendMarks, "------------------------------------------------");
        }

        [DebuggerStepThrough]
        public static string FormatInvariant(this string format, params object[] objects)
        {
            return string.Format(CultureInfo.InvariantCulture, format, objects);
        }

        [DebuggerStepThrough]
        public static string FormatCurrentUI(this string format, params object[] objects)
        {
            return string.Format(CultureInfo.CurrentUICulture, format, objects);
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string format, params object[] args)
        {
            return FormatWith(format, CultureInfo.CurrentCulture, args);
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider, format, args);
        }

        [DebuggerStepThrough]
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static string NullEmpty(this string value)
        {
            return (string.IsNullOrEmpty(value)) ? null : value;
        }

        [DebuggerStepThrough]
        public static string SplitPascalCase(this string value)
        {
            var sb = new StringBuilder();
            char[] ca = value.ToCharArray();
            sb.Append(ca[0]);

            for (int i = 1; i < ca.Length - 1; i++)
            {
                char c = ca[i];
                if (char.IsUpper(c) && (char.IsLower(ca[i + 1]) || char.IsLower(ca[i - 1])))
                {
                    sb.Append(" ");
                }
                sb.Append(c);
            }

            if (ca.Length > 1)
            {
                sb.Append(ca[ca.Length - 1]);
            }

            return sb.ToString();
        }

        [DebuggerStepThrough]
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}