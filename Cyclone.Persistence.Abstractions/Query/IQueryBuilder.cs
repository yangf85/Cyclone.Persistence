using System;
using System.Linq.Expressions;

namespace Cyclone.Persistence.Abstractions.Query;

/// <summary>
/// 定义查询构建器的接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IQueryBuilder<TEntity> where TEntity : class
{
    /// <summary>
    /// 添加过滤条件
    /// </summary>
    /// <param name="predicate">过滤表达式</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 添加排序条件（升序）
    /// </summary>
    /// <typeparam name="TKey">排序键类型</typeparam>
    /// <param name="keySelector">排序键选择器</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

    /// <summary>
    /// 添加排序条件（降序）
    /// </summary>
    /// <typeparam name="TKey">排序键类型</typeparam>
    /// <param name="keySelector">排序键选择器</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

    /// <summary>
    /// 添加次级排序条件（升序）
    /// </summary>
    /// <typeparam name="TKey">排序键类型</typeparam>
    /// <param name="keySelector">排序键选择器</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

    /// <summary>
    /// 添加次级排序条件（降序）
    /// </summary>
    /// <typeparam name="TKey">排序键类型</typeparam>
    /// <param name="keySelector">排序键选择器</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

    /// <summary>
    /// 设置跳过的记录数
    /// </summary>
    /// <param name="count">跳过的记录数</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> Skip(int count);

    /// <summary>
    /// 设置获取的记录数
    /// </summary>
    /// <param name="count">获取的记录数</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> Take(int count);

    /// <summary>
    /// 设置分页
    /// </summary>
    /// <param name="pageNumber">页码（从1开始）</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> Page(int pageNumber, int pageSize);

    /// <summary>
    /// 包含导航属性
    /// </summary>
    /// <typeparam name="TProperty">导航属性类型</typeparam>
    /// <param name="navigationPropertyPath">导航属性路径</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);

    /// <summary>
    /// 投影查询
    /// </summary>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="selector">选择器</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector) where TResult : class;

    /// <summary>
    /// 设置是否跟踪实体
    /// </summary>
    /// <param name="track">是否跟踪</param>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> AsTracking(bool track = true);

    /// <summary>
    /// 设置是否不跟踪实体
    /// </summary>
    /// <returns>查询构建器</returns>
    IQueryBuilder<TEntity> AsNoTracking();
}