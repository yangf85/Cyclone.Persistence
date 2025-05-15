using System;
using System.Collections.Generic;
using System.Linq;
using Cyclone.Persistence.Abstractions.Query;

namespace Cyclone.Persistence.Common.Models
{
    /// <summary>
    /// 表示分页查询结果
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class PagedResult<T> : IPaginatedResult<T> where T : class
    {
        /// <summary>
        /// 获取当前页码
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// 获取页面大小
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// 获取总页数
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        /// <summary>
        /// 获取是否有上一页
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// 获取是否有下一页
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        /// <summary>
        /// 获取当前页数据
        /// </summary>
        public IEnumerable<T> Items { get; private set; }

        /// <summary>
        /// 初始化分页结果
        /// </summary>
        public PagedResult()
        {
            Items = Enumerable.Empty<T>();
        }

        /// <summary>
        /// 初始化分页结果
        /// </summary>
        /// <param name="items">当前页数据</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "页码必须大于或等于1。");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "页面大小必须大于或等于1。");

            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// 创建空的分页结果
        /// </summary>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>空的分页结果</returns>
        public static PagedResult<T> Empty(int pageNumber, int pageSize)
        {
            return new PagedResult<T>(Enumerable.Empty<T>(), 0, pageNumber, pageSize);
        }

        /// <summary>
        /// 创建空的分页结果
        /// </summary>
        /// <returns>空的分页结果</returns>
        public static PagedResult<T> Empty()
        {
            return Empty(1, 10);
        }

        /// <summary>
        /// 从集合创建分页结果
        /// </summary>
        /// <param name="source">源集合</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>分页结果</returns>
        public static PagedResult<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var enumerable = source as T[] ?? source.ToArray();
            var count = enumerable.Length;
            var items = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<T>(items, count, pageNumber, pageSize);
        }
    }
}