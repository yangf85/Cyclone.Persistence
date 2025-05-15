using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cyclone.Persistence.Common.Extensions
{
    /// <summary>
    /// 提供类型的扩展方法
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>默认值</returns>
        public static object GetDefaultValue(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }

        /// <summary>
        /// 检查类型是否是可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是可空类型</returns>
        public static bool IsNullableType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 获取可空类型的基础类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>基础类型</returns>
        public static Type GetNonNullableType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (IsNullableType(type))
                return type.GetGenericArguments()[0];

            return type;
        }

        /// <summary>
        /// 检查类型是否实现了指定接口
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>是否实现了指定接口</returns>
        public static bool ImplementsInterface(this Type type, Type interfaceType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (interfaceType == null)
                throw new ArgumentNullException(nameof(interfaceType));

            return type.GetInterfaces().Any(t => t == interfaceType ||
                (t.IsGenericType && t.GetGenericTypeDefinition() == interfaceType));
        }

        /// <summary>
        /// 检查类型是否继承自指定类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="baseType">基类类型</param>
        /// <returns>是否继承自指定类型</returns>
        public static bool InheritsFrom(this Type type, Type baseType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (baseType == null)
                throw new ArgumentNullException(nameof(baseType));

            if (baseType.IsGenericTypeDefinition && type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() == baseType ||
                      (type.BaseType != null && type.BaseType.InheritsFrom(baseType));
            }

            return baseType.IsAssignableFrom(type) && type != baseType;
        }

        /// <summary>
        /// 检查类型是否是集合类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是集合类型</returns>
        public static bool IsCollectionType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsArray ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)) ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>)) ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>)) ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>)) ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                   typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        /// 获取集合元素类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>元素类型</returns>
        public static Type GetCollectionElementType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsArray)
                return type.GetElementType();

            if (type.IsGenericType)
            {
                var genericArgs = type.GetGenericArguments();
                if (genericArgs.Length > 0)
                    return genericArgs[0];
            }

            foreach (var interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType)
                {
                    var genericDefinition = interfaceType.GetGenericTypeDefinition();
                    if (genericDefinition == typeof(IEnumerable<>) ||
                        genericDefinition == typeof(ICollection<>) ||
                        genericDefinition == typeof(IList<>))
                    {
                        return interfaceType.GetGenericArguments()[0];
                    }
                }
            }

            return typeof(object);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">构造函数参数</param>
        /// <returns>实例</returns>
        public static object CreateInstance(this Type type, params object[] args)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// 获取可读属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>可读属性集合</returns>
        public static IEnumerable<PropertyInfo> GetReadableProperties(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead);
        }

        /// <summary>
        /// 获取可写属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>可写属性集合</returns>
        public static IEnumerable<PropertyInfo> GetWritableProperties(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite);
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性信息</returns>
        public static PropertyInfo GetProperty(this Type type, string propertyName)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            return type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(this Type type, object obj, string propertyName)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException($"属性 {propertyName} 不存在", nameof(propertyName));

            return property.GetValue(obj);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetPropertyValue(this Type type, object obj, string propertyName, object value)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException($"属性 {propertyName} 不存在", nameof(propertyName));
            if (!property.CanWrite)
                throw new ArgumentException($"属性 {propertyName} 不可写", nameof(propertyName));

            property.SetValue(obj, value);
        }

        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性</returns>
        public static TAttribute GetAttribute<TAttribute>(this Type type, bool inherit = false) where TAttribute : Attribute
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetCustomAttributes(typeof(TAttribute), inherit).FirstOrDefault() as TAttribute;
        }

        /// <summary>
        /// 获取特性集合
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>特性集合</returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type type, bool inherit = false) where TAttribute : Attribute
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }

        /// <summary>
        /// 检查类型是否具有特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="inherit">是否继承</param>
        /// <returns>是否具有特性</returns>
        public static bool HasAttribute<TAttribute>(this Type type, bool inherit = false) where TAttribute : Attribute
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetCustomAttributes(typeof(TAttribute), inherit).Any();
        }

        /// <summary>
        /// 获取友好的类型名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>友好的类型名称</returns>
        public static string GetFriendlyName(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments()
                    .Select(t => GetFriendlyName(t))
                    .ToArray();

                var typeDefinition = type.Name;
                var index = typeDefinition.IndexOf('`');
                if (index > 0)
                    typeDefinition = typeDefinition.Substring(0, index);

                return $"{typeDefinition}<{string.Join(", ", genericArguments)}>";
            }

            if (type.IsArray)
            {
                var rank = type.GetArrayRank();
                var elementType = type.GetElementType();
                var dimensions = string.Concat(Enumerable.Repeat(",", rank - 1));
                return $"{GetFriendlyName(elementType)}[{dimensions}]";
            }

            return type.Name;
        }

        /// <summary>
        /// 获取类型的所有实现类
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="assembly">程序集</param>
        /// <returns>实现类集合</returns>
        public static IEnumerable<Type> GetImplementations(this Type type, Assembly assembly = null)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (assembly == null)
                assembly = type.Assembly;

            return assembly.GetTypes()
                .Where(t => t != type && !t.IsInterface && !t.IsAbstract && type.IsAssignableFrom(t));
        }

        /// <summary>
        /// 检查类型是否是数值类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是数值类型</returns>
        public static bool IsNumericType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var nonNullableType = GetNonNullableType(type);

            return nonNullableType == typeof(byte) ||
                   nonNullableType == typeof(sbyte) ||
                   nonNullableType == typeof(short) ||
                   nonNullableType == typeof(ushort) ||
                   nonNullableType == typeof(int) ||
                   nonNullableType == typeof(uint) ||
                   nonNullableType == typeof(long) ||
                   nonNullableType == typeof(ulong) ||
                   nonNullableType == typeof(float) ||
                   nonNullableType == typeof(double) ||
                   nonNullableType == typeof(decimal);
        }

        /// <summary>
        /// 检查类型是否是整数类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是整数类型</returns>
        public static bool IsIntegerType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var nonNullableType = GetNonNullableType(type);

            return nonNullableType == typeof(byte) ||
                   nonNullableType == typeof(sbyte) ||
                   nonNullableType == typeof(short) ||
                   nonNullableType == typeof(ushort) ||
                   nonNullableType == typeof(int) ||
                   nonNullableType == typeof(uint) ||
                   nonNullableType == typeof(long) ||
                   nonNullableType == typeof(ulong);
        }

        /// <summary>
        /// 检查类型是否是浮点数类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是浮点数类型</returns>
        public static bool IsFloatingPointType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var nonNullableType = GetNonNullableType(type);

            return nonNullableType == typeof(float) ||
                   nonNullableType == typeof(double) ||
                   nonNullableType == typeof(decimal);
        }
    }
}