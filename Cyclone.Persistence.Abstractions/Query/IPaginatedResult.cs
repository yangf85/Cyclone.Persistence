using System.Collections.Generic;

namespace Cyclone.Persistence.Abstractions.Query;

/// <summary>
/// 定义分页结果的接口
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public interface IPaginatedResult<T> where T : class
{
    /// <summary>
    /// 当前页码
    /// </summary>
    int PageNumber { get; }

    /// <summary>
    /// 页面大小
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// 总记录数
    /// </summary>
    int TotalCount { get; }

    /// <summary>
    /// 总页数
    /// </summary>
    int TotalPages { get; }

    /// <summary>
    /// 是否有上一页
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// 是否有下一页
    /// </summary>
    bool HasNextPage { get; }

    /// <summary>
    /// 当前页数据
    /// </summary>
    IEnumerable<T> Items { get; }
}