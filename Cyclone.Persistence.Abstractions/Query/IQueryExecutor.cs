using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Query;

/// <summary>
/// 定义查询执行器的接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IQueryExecutor<TEntity> where TEntity : class
{
    /// <summary>
    /// 获取单个实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>实体对象，如果没有找到则返回null</returns>
    TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 异步获取单个实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>实体对象，如果没有找到则返回null</returns>
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取第一个实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>实体对象，如果没有找到则返回null</returns>
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 异步获取第一个实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>实体对象，如果没有找到则返回null</returns>
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <returns>实体集合</returns>
    IEnumerable<TEntity> ToList();

    /// <summary>
    /// 异步获取所有实体
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>实体集合</returns>
    Task<IEnumerable<TEntity>> ToListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取数组
    /// </summary>
    /// <returns>实体数组</returns>
    TEntity[] ToArray();

    /// <summary>
    /// 异步获取数组
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>实体数组</returns>
    Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取记录数
    /// </summary>
    /// <returns>记录数</returns>
    int Count();

    /// <summary>
    /// 异步获取记录数
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>记录数</returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 是否存在满足条件的记录
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>是否存在</returns>
    bool Any(Expression<Func<TEntity, bool>> predicate = null);

    /// <summary>
    /// 异步检查是否存在满足条件的记录
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>是否存在</returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取指定列的最大值
    /// </summary>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="selector">选择器</param>
    /// <returns>最大值</returns>
    TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector);

    /// <summary>
    /// 异步获取指定列的最大值
    /// </summary>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="selector">选择器</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>最大值</returns>
    Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取指定列的最小值
    /// </summary>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="selector">选择器</param>
    /// <returns>最小值</returns>
    TResult Min<TResult>(Expression<Func<TEntity, TResult>> selector);

    /// <summary>
    /// 异步获取指定列的最小值
    /// </summary>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="selector">选择器</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>最小值</returns>
    Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取指定列的平均值
    /// </summary>
    /// <param name="selector">选择器</param>
    /// <returns>平均值</returns>
    double Average(Expression<Func<TEntity, int>> selector);

    /// <summary>
    /// 异步获取指定列的平均值
    /// </summary>
    /// <param name="selector">选择器</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>平均值</returns>
    Task<double> AverageAsync(Expression<Func<TEntity, int>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取指定列的总和
    /// </summary>
    /// <param name="selector">选择器</param>
    /// <returns>总和</returns>
    int Sum(Expression<Func<TEntity, int>> selector);

    /// <summary>
    /// 异步获取指定列的总和
    /// </summary>
    /// <param name="selector">选择器</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>总和</returns>
    Task<int> SumAsync(Expression<Func<TEntity, int>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageNumber">页码（从1开始）</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns>分页结果</returns>
    IPaginatedResult<TEntity> ToPaginatedResult(int pageNumber, int pageSize);

    /// <summary>
    /// 异步分页查询
    /// </summary>
    /// <param name="pageNumber">页码（从1开始）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>分页结果</returns>
    Task<IPaginatedResult<TEntity>> ToPaginatedResultAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}