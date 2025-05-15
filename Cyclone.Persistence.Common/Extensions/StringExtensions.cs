using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cyclone.Persistence.Common.Extensions
{
    /// <summary>
    /// 提供字符串的扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 检查字符串是否为空或仅包含空白字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>是否为空或仅包含空白字符</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 检查字符串是否为空或空字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>是否为空或空字符串</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 如果字符串为 null，则返回空字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>原字符串或空字符串</returns>
        public static string NullAsEmpty(this string value)
        {
            return value ?? string.Empty;
        }

        /// <summary>
        /// 如果字符串为空或仅包含空白字符，则返回 null
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>原字符串或 null</returns>
        public static string EmptyAsNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        /// <summary>
        /// 截取字符串，超出部分用省略号代替
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="suffix">后缀</param>
        /// <returns>截取后的字符串</returns>
        public static string Truncate(this string value, int maxLength, string suffix = "...")
        {
            if (value == null)
                return null;

            if (maxLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength), "最大长度必须大于0");

            if (suffix == null)
                suffix = string.Empty;

            if (value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength - suffix.Length) + suffix;
        }

        /// <summary>
        /// 将字符串转换为标题样式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>标题样式的字符串</returns>
        public static string ToTitleCase(this string value)
        {
            if (value == null)
                return null;

            if (value.Length == 0)
                return string.Empty;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        /// <summary>
        /// 将字符串转换为驼峰命名法
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>驼峰命名法的字符串</returns>
        public static string ToCamelCase(this string value)
        {
            if (value == null)
                return null;

            if (value.Length == 0)
                return string.Empty;

            if (value.Length == 1)
                return value.ToLower();

            return char.ToLower(value[0]) + value.Substring(1);
        }

        /// <summary>
        /// 将字符串转换为帕斯卡命名法
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>帕斯卡命名法的字符串</returns>
        public static string ToPascalCase(this string value)
        {
            if (value == null)
                return null;

            if (value.Length == 0)
                return string.Empty;

            if (value.Length == 1)
                return value.ToUpper();

            return char.ToUpper(value[0]) + value.Substring(1);
        }

        /// <summary>
        /// 将字符串转换为下划线命名法
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>下划线命名法的字符串</returns>
        public static string ToSnakeCase(this string value)
        {
            if (value == null)
                return null;

            if (value.Length == 0)
                return string.Empty;

            return Regex.Replace(value, "([a-z])([A-Z])", "$1_$2").ToLower();
        }

        /// <summary>
        /// 将字符串转换为短划线命名法
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>短划线命名法的字符串</returns>
        public static string ToKebabCase(this string value)
        {
            if (value == null)
                return null;

            if (value.Length == 0)
                return string.Empty;

            return Regex.Replace(value, "([a-z])([A-Z])", "$1-$2").ToLower();
        }

        /// <summary>
        /// 移除字符串中的所有空白字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>移除空白字符后的字符串</returns>
        public static string RemoveWhitespace(this string value)
        {
            if (value == null)
                return null;

            return new string(value.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// 检查字符串是否是有效的电子邮件地址
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>是否是有效的电子邮件地址</returns>
        public static bool IsValidEmail(this string value)
        {
            if (value == null)
                return false;

            // 使用简单的正则表达式验证电子邮件地址
            return Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// 检查字符串是否是有效的 URL
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>是否是有效的 URL</returns>
        public static bool IsValidUrl(this string value)
        {
            if (value == null)
                return false;

            return Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// 检查字符串是否只包含数字
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>是否只包含数字</returns>
        public static bool IsNumeric(this string value)
        {
            if (value == null)
                return false;

            return value.All(char.IsDigit);
        }

        /// <summary>
        /// 将字符串转换为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">字符串</param>
        /// <returns>转换后的值</returns>
        public static T ConvertTo<T>(this string value)
        {
            if (value == null)
                return default;

            Type targetType = typeof(T);

            if (targetType == typeof(string))
                return (T)(object)value;

            if (targetType == typeof(int) || targetType == typeof(int?))
                return (T)(object)int.Parse(value);

            if (targetType == typeof(long) || targetType == typeof(long?))
                return (T)(object)long.Parse(value);

            if (targetType == typeof(double) || targetType == typeof(double?))
                return (T)(object)double.Parse(value);

            if (targetType == typeof(decimal) || targetType == typeof(decimal?))
                return (T)(object)decimal.Parse(value);

            if (targetType == typeof(bool) || targetType == typeof(bool?))
                return (T)(object)bool.Parse(value);

            if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                return (T)(object)DateTime.Parse(value);

            if (targetType == typeof(Guid) || targetType == typeof(Guid?))
                return (T)(object)Guid.Parse(value);

            if (targetType.IsEnum)
                return (T)Enum.Parse(targetType, value);

            return (T)Convert.ChangeType(value, targetType);
        }

        /// <summary>
        /// 安全地将字符串转换为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的值</returns>
        public static T ConvertToOrDefault<T>(this string value, T defaultValue = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                    return defaultValue;

                return ConvertTo<T>(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 重复字符串指定次数
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="count">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(this string value, int count)
        {
            if (value == null)
                return null;

            if (count <= 0)
                return string.Empty;

            var builder = new StringBuilder(value.Length * count);
            for (int i = 0; i < count; i++)
            {
                builder.Append(value);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>反转后的字符串</returns>
        public static string Reverse(this string value)
        {
            if (value == null)
                return null;

            char[] chars = value.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 获取字符串中的数字部分
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>数字部分</returns>
        public static string ExtractNumbers(this string value)
        {
            if (value == null)
                return null;

            return new string(value.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// 获取字符串中的字母部分
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>字母部分</returns>
        public static string ExtractLetters(this string value)
        {
            if (value == null)
                return null;

            return new string(value.Where(char.IsLetter).ToArray());
        }

        /// <summary>
        /// 安全地分割字符串，避免 null
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="options">分割选项</param>
        /// <returns>分割后的字符串数组</returns>
        public static string[] SafeSplit(this string value, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            if (value == null)
                return Array.Empty<string>();

            return value.Split(new[] { separator }, options);
        }

        /// <summary>
        /// 安全地分割字符串，避免 null
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="options">分割选项</param>
        /// <returns>分割后的字符串数组</returns>
        public static string[] SafeSplit(this string value, char separator, StringSplitOptions options = StringSplitOptions.None)
        {
            if (value == null)
                return Array.Empty<string>();

            return value.Split(new[] { separator }, options);
        }

        /// <summary>
        /// 将字符串转换为单词集合
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>单词集合</returns>
        public static IEnumerable<string> ToWords(this string value)
        {
            if (value == null)
                yield break;

            var matches = Regex.Matches(value, @"[\p{L}\p{Nd}]+");
            foreach (Match match in matches)
            {
                yield return match.Value;
            }
        }

        /// <summary>
        /// 获取字符串的首字母缩写
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>首字母缩写</returns>
        public static string ToInitials(this string value)
        {
            if (value == null)
                return null;

            var words = value.ToWords().ToArray();
            if (words.Length == 0)
                return string.Empty;

            var builder = new StringBuilder(words.Length);
            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    builder.Append(char.ToUpper(word[0]));
                }
            }
            return builder.ToString();
        }
    }
}