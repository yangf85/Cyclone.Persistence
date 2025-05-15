using System;
using System.Collections.Generic;
using System.Linq;
using Cyclone.Persistence.Common.Models;

namespace Cyclone.Persistence.Common.Extensions
{
    /// <summary>
    /// 提供 IEnumerable 的扩展方法
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 将集合分页
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>分页结果</returns>
        public static PagedResult<T> ToPagedResult<T>(this IEnumerable<T> source, int pageNumber, int pageSize) where T : class
        {
            return PagedResult<T>.Create(source, pageNumber, pageSize);
        }

        /// <summary>
        /// 将集合转换为非空集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <returns>非空集合</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// 对集合的每个元素执行操作
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="action">要执行的操作</param>
        /// <returns>源集合</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }

        /// <summary>
        /// 将集合拆分为指定大小的块
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="batchSize">批次大小</param>
        /// <returns>批次集合</returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (batchSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(batchSize), "批次大小必须大于0");

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return GetBatch(enumerator, batchSize);
                }
            }
        }

        private static IEnumerable<T> GetBatch<T>(IEnumerator<T> enumerator, int batchSize)
        {
            do
            {
                yield return enumerator.Current;
            } while (--batchSize > 0 && enumerator.MoveNext());
        }

        /// <summary>
        /// 按指定条件将集合分组为两个集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="predicate">条件</param>
        /// <returns>满足条件和不满足条件的两个集合</returns>
        public static (IEnumerable<T> Matched, IEnumerable<T> Unmatched) Split<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var matched = new List<T>();
            var unmatched = new List<T>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    matched.Add(item);
                }
                else
                {
                    unmatched.Add(item);
                }
            }

            return (matched, unmatched);
        }

        /// <summary>
        /// 如果集合不为空，则执行指定操作
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="action">要执行的操作</param>
        /// <returns>源集合</returns>
        public static IEnumerable<T> IfNotEmpty<T>(this IEnumerable<T> source, Action<IEnumerable<T>> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (source.Any())
            {
                action(source);
            }

            return source;
        }

        /// <summary>
        /// 如果集合为空，则返回备用集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="fallback">备用集合</param>
        /// <returns>源集合或备用集合</returns>
        public static IEnumerable<T> IfEmpty<T>(this IEnumerable<T> source, IEnumerable<T> fallback)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (fallback == null)
                throw new ArgumentNullException(nameof(fallback));

            return source.Any() ? source : fallback;
        }

        /// <summary>
        /// 如果集合为空，则执行指定函数返回备用集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="fallbackFunc">备用集合函数</param>
        /// <returns>源集合或备用集合</returns>
        public static IEnumerable<T> IfEmpty<T>(this IEnumerable<T> source, Func<IEnumerable<T>> fallbackFunc)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (fallbackFunc == null)
                throw new ArgumentNullException(nameof(fallbackFunc));

            return source.Any() ? source : fallbackFunc();
        }

        /// <summary>
        /// 返回集合或空集合，避免 null
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <returns>源集合或空集合</returns>
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// 将集合转换为可观察集合
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="onAdd">添加元素时的处理函数</param>
        /// <param name="onRemove">移除元素时的处理函数</param>
        /// <param name="onClear">清空集合时的处理函数</param>
        /// <returns>可观察集合</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source,
            Action<T> onAdd = null, Action<T> onRemove = null, Action onClear = null)
        {
            return new ObservableCollection<T>(source, onAdd, onRemove, onClear);
        }

        /// <summary>
        /// 可观察集合类
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        public class ObservableCollection<T> : ICollection<T>
        {
            private readonly ICollection<T> _collection;
            private readonly Action<T> _onAdd;
            private readonly Action<T> _onRemove;
            private readonly Action _onClear;

            /// <summary>
            /// 初始化可观察集合
            /// </summary>
            /// <param name="source">源集合</param>
            /// <param name="onAdd">添加元素时的处理函数</param>
            /// <param name="onRemove">移除元素时的处理函数</param>
            /// <param name="onClear">清空集合时的处理函数</param>
            public ObservableCollection(IEnumerable<T> source, Action<T> onAdd = null, Action<T> onRemove = null, Action onClear = null)
            {
                _collection = new List<T>(source ?? Enumerable.Empty<T>());
                _onAdd = onAdd;
                _onRemove = onRemove;
                _onClear = onClear;
            }

            /// <summary>
            /// 获取集合中的元素数量
            /// </summary>
            public int Count => _collection.Count;

            /// <summary>
            /// 获取集合是否为只读
            /// </summary>
            public bool IsReadOnly => _collection.IsReadOnly;

            /// <summary>
            /// 添加元素
            /// </summary>
            /// <param name="item">要添加的元素</param>
            public void Add(T item)
            {
                _collection.Add(item);
                _onAdd?.Invoke(item);
            }

            /// <summary>
            /// 清空集合
            /// </summary>
            public void Clear()
            {
                _collection.Clear();
                _onClear?.Invoke();
            }

            /// <summary>
            /// 检查集合是否包含指定元素
            /// </summary>
            /// <param name="item">要检查的元素</param>
            /// <returns>是否包含</returns>
            public bool Contains(T item)
            {
                return _collection.Contains(item);
            }

            /// <summary>
            /// 将集合复制到数组
            /// </summary>
            /// <param name="array">目标数组</param>
            /// <param name="arrayIndex">开始索引</param>
            public void CopyTo(T[] array, int arrayIndex)
            {
                _collection.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// 获取枚举器
            /// </summary>
            /// <returns>枚举器</returns>
            public IEnumerator<T> GetEnumerator()
            {
                return _collection.GetEnumerator();
            }

            /// <summary>
            /// 移除指定元素
            /// </summary>
            /// <param name="item">要移除的元素</param>
            /// <returns>是否成功移除</returns>
            public bool Remove(T item)
            {
                var result = _collection.Remove(item);
                if (result)
                {
                    _onRemove?.Invoke(item);
                }
                return result;
            }

            /// <summary>
            /// 获取枚举器
            /// </summary>
            /// <returns>枚举器</returns>
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}