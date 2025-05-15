using System;
using System.Collections.Concurrent;

namespace Cyclone.Persistence.Common.Utils
{
    /// <summary>
    /// 提供类型缓存的工具类
    /// </summary>
    /// <typeparam name="TValue">缓存值类型</typeparam>
    public class ConcurrentTypeCache<TValue>
    {
        private readonly ConcurrentDictionary<Type, TValue> _cache = new ConcurrentDictionary<Type, TValue>();
        private readonly Func<Type, TValue> _valueFactory;

        /// <summary>
        /// 初始化类型缓存
        /// </summary>
        /// <param name="valueFactory">值工厂</param>
        public ConcurrentTypeCache(Func<Type, TValue> valueFactory)
        {
            _valueFactory = valueFactory ?? throw new ArgumentNullException(nameof(valueFactory));
        }

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>缓存值</returns>
        public TValue GetOrAdd<T>()
        {
            return GetOrAdd(typeof(T));
        }

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>缓存值</returns>
        public TValue GetOrAdd(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return _cache.GetOrAdd(type, _valueFactory);
        }

        /// <summary>
        /// 尝试获取缓存值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">缓存值</param>
        /// <returns>是否成功获取</returns>
        public bool TryGetValue<T>(out TValue value)
        {
            return TryGetValue(typeof(T), out value);
        }

        /// <summary>
        /// 尝试获取缓存值
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">缓存值</param>
        /// <returns>是否成功获取</returns>
        public bool TryGetValue(Type type, out TValue value)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return _cache.TryGetValue(type, out value);
        }

        /// <summary>
        /// 添加或更新缓存值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">缓存值</param>
        /// <returns>新的缓存值</returns>
        public TValue AddOrUpdate<T>(TValue value)
        {
            return AddOrUpdate(typeof(T), value);
        }

        /// <summary>
        /// 添加或更新缓存值
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">缓存值</param>
        /// <returns>新的缓存值</returns>
        public TValue AddOrUpdate(Type type, TValue value)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return _cache.AddOrUpdate(type, value, (_, __) => value);
        }

        /// <summary>
        /// 尝试添加缓存值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">缓存值</param>
        /// <returns>是否成功添加</returns>
        public bool TryAdd<T>(TValue value)
        {
            return TryAdd(typeof(T), value);
        }

        /// <summary>
        /// 尝试添加缓存值
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">缓存值</param>
        /// <returns>是否成功添加</returns>
        public bool TryAdd(Type type, TValue value)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return _cache.TryAdd(type, value);
        }

        /// <summary>
        /// 尝试移除缓存值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">缓存值</param>
        /// <returns>是否成功移除</returns>
        public bool TryRemove<T>(out TValue value)
        {
            return TryRemove(typeof(T), out value);
        }

        /// <summary>
        /// 尝试移除缓存值
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">缓存值</param>
        /// <returns>是否成功移除</returns>
        public bool TryRemove(Type type, out TValue value)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return _cache.TryRemove(type, out value);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        /// <summary>
        /// 获取缓存项数量
        /// </summary>
        public int Count => _cache.Count;
    }
}