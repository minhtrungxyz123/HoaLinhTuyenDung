using TuyenDung.Core.Models;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TuyenDung.Core.FormFile
{
    public class CommonHelper
    {
        public static IConfiguration Configuration { get; set; }

        public static bool IsTruthy(object value)
        {
            if (value == null)
                return false;

            switch (value)
            {
                case string x:
                    return x.HasValue();
                case bool x:
                    return x == true;
                case DateTime x:
                    return x > DateTime.MinValue;
                case TimeSpan x:
                    return x > TimeSpan.MinValue;
                case Guid x:
                    return x != Guid.Empty;
                case IComparable x:
                    return x.CompareTo(0) != 0;
                case IEnumerable<object> x:
                    return x.Any();
                case IEnumerable x:
                    return x.GetEnumerator().MoveNext();
            }

            if (value.GetType().IsNullable(out var wrappedType))
            {
                return IsTruthy(Convert.ChangeType(value, wrappedType));
            }

            return true;
        }

        public static IDictionary<string, object> ObjectToDictionary(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return FastProperty.ObjectToDictionary(
                obj,
                key => key.Replace("_", "-").Replace("@", ""));
        }

        public static string MapPath(string path)
        {
            var root = GetApplicationRoot();

            return Combine(root, path);
        }

        public static string Combine(string root, string path)
        {
            var separator = Path.DirectorySeparatorChar;
            var result = string.Empty;
            // Windows
            if (separator == '\\')
            {
                path = path.Replace("~/", "").TrimStart('/').TrimStart('\\').Replace('/', '\\');
            }
            // Linux
            else if (separator == '/')
            {
                path = path.Replace("~/", "").TrimStart('/').TrimStart('\\').Replace('\\', '/');
            }

            result = Path.Combine(root, path);

            return result;
        }

        public static string GetApplicationRoot()
        {
            return Directory.GetCurrentDirectory();
        }

        public static void Resize(string h, int w, int he)
        {
            using (var image = Image.Load(h))
            {
                if (image.Width > w)
                {
                    image.Mutate(x => x.Resize(w, he));
                    image.Save(h);
                }
            }
        }

        public static T GetAppSetting<T>(string key)
        {
            Guard.NotEmpty(key, nameof(key));

            var setting = Configuration.GetValue<T>(key);

            return setting.Convert<T>();
        }

        public static T GetAppSetting<T>(string key, T defValue)
        {
            Guard.NotEmpty(key, nameof(key));

            var setting = Configuration.GetValue<T>(key);

            if (setting == null)
            {
                return defValue;
            }

            return setting.Convert<T>();
        }

        public static bool TryConvert<T>(object value, out T convertedValue)
        {
            return TryConvert<T>(value, CultureInfo.InvariantCulture, out convertedValue);
        }

        public static bool TryConvert<T>(object value, CultureInfo culture, out T convertedValue)
        {
            return TryAction<T>(delegate
            {
                return value.Convert<T>(culture);
            }, out convertedValue);
        }

        public static bool TryConvert(object value, Type to, out object convertedValue)
        {
            return TryConvert(value, to, CultureInfo.InvariantCulture, out convertedValue);
        }

        public static bool TryConvert(object value, Type to, CultureInfo culture, out object convertedValue)
        {
            return TryAction<object>(delegate { return value.Convert(to, culture); }, out convertedValue);
        }

        private static bool TryAction<T>(Func<T> func, out T output)
        {
            Guard.NotNull(func, nameof(func));

            try
            {
                output = func();
                return true;
            }
            catch
            {
                output = default(T);
                return false;
            }
        }
    }
}