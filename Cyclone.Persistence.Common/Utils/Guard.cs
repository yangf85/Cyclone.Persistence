using System;
using System.Collections.Generic;
using System.Linq;

namespace Cyclone.Persistence.Common.Utils
{
    /// <summary>
    /// 提供参数检查的工具类
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// 检查参数不为 null
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentNullException">参数为 null 时抛出</exception>
        public static void NotNull(object value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// 检查字符串不为 null 或空字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentNullException">字符串为 null 时抛出</exception>
        /// <exception cref="ArgumentException">字符串为空时抛出</exception>
        public static void NotNullOrEmpty(string value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            if (value.Length == 0)
                throw new ArgumentException("字符串不能为空", paramName);
        }

        /// <summary>
        /// 检查字符串不为 null、空字符串或仅包含空白字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentNullException">字符串为 null 时抛出</exception>
        /// <exception cref="ArgumentException">字符串为空或仅包含空白字符时抛出</exception>
        public static void NotNullOrWhiteSpace(string value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("字符串不能为空或仅包含空白字符", paramName);
        }

        /// <summary>
        /// 检查集合不为 null 或空集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="value">集合</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentNullException">集合为 null 时抛出</exception>
        /// <exception cref="ArgumentException">集合为空时抛出</exception>
        public static void NotNullOrEmpty<T>(IEnumerable<T> value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            if (!value.Any())
                throw new ArgumentException("集合不能为空", paramName);
        }

        /// <summary>
        /// 检查集合不为 null，但允许为空集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="value">集合</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentNullException">集合为 null 时抛出</exception>
        public static void NotNull<T>(IEnumerable<T> value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// 检查参数在指定范围内
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">参数值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentOutOfRangeException">参数超出范围时抛出</exception>
        public static void InRange<T>(T value, T minValue, T maxValue, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
                throw new ArgumentOutOfRangeException(paramName, value, $"值必须在 {minValue} 和 {maxValue} 之间");
        }

        /// <summary>
        /// 检查参数大于指定值
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">参数值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentOutOfRangeException">参数小于或等于最小值时抛出</exception>
        public static void GreaterThan<T>(T value, T minValue, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(minValue) <= 0)
                throw new ArgumentOutOfRangeException(paramName, value, $"值必须大于 {minValue}");
        }

        /// <summary>
        /// 检查参数大于或等于指定值
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">参数值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentOutOfRangeException">参数小于最小值时抛出</exception>
        public static void GreaterThanOrEqual<T>(T value, T minValue, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(minValue) < 0)
                throw new ArgumentOutOfRangeException(paramName, value, $"值必须大于或等于 {minValue}");
        }

        /// <summary>
        /// 检查参数小于指定值
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">参数值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentOutOfRangeException">参数大于或等于最大值时抛出</exception>
        public static void LessThan<T>(T value, T maxValue, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(maxValue) >= 0)
                throw new ArgumentOutOfRangeException(paramName, value, $"值必须小于 {maxValue}");
        }

        /// <summary>
        /// 检查参数小于或等于指定值
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">参数值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentOutOfRangeException">参数大于最大值时抛出</exception>
        public static void LessThanOrEqual<T>(T value, T maxValue, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(maxValue) > 0)
                throw new ArgumentOutOfRangeException(paramName, value, $"值必须小于或等于 {maxValue}");
        }

        /// <summary>
        /// 检查参数满足指定条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">参数值</param>
        /// <param name="predicate">条件</param>
        /// <param name="paramName">参数名</param>
        /// <param name="message">错误消息</param>
        /// <exception cref="ArgumentException">参数不满足条件时抛出</exception>
        public static void Requires<T>(T value, Func<T, bool> predicate, string paramName, string message = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            if (!predicate(value))
                throw new ArgumentException(message ?? "参数不满足指定条件", paramName);
        }

        /// <summary>
        /// 检查参数是指定类型
        /// </summary>
        /// <typeparam name="T">期望的类型</typeparam>
        /// <param name="value">参数值</param>
        /// <param name="paramName">参数名</param>
        /// <exception cref="ArgumentNullException">参数为 null 时抛出</exception>
        /// <exception cref="ArgumentException">参数不是指定类型时抛出</exception>
        /// <returns>转换后的参数值</returns>
        public static T IsType<T>(object value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);

            if (!(value is T result))
                throw new ArgumentException($"参数必须是 {typeof(T).Name} 类型", paramName);

            return result;
        }

        /// <summary>
        /// 检查条件为真
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="message">错误消息</param>
        /// <exception cref="InvalidOperationException">条件为假时抛出</exception>
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
                throw new InvalidOperationException(message);
        }

        /// <summary>
        /// 检查条件为假
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="message">错误消息</param>
        /// <exception cref="InvalidOperationException">条件为真时抛出</exception>
        public static void IsFalse(bool condition, string message)
        {
            if (condition)
                throw new InvalidOperationException(message);
        }

        /// <summary>
        /// 引发异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <exception cref="Exception">始终抛出</exception>
        public static void Throw<TException>(string message) where TException : Exception
        {
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }
}