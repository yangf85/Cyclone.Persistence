using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Data;

/// <summary>
/// 定义数据写入器的接口
/// </summary>
public interface IDataWriter : IDisposable
{
    /// <summary>
    /// 添加实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entity">实体对象</param>
    /// <returns>添加后的实体对象</returns>
    TEntity Add<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// 异步添加实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entity">实体对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>添加后的实体对象</returns>
    Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 批量添加实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <returns>添加的记录数</returns>
    int AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    /// <summary>
    /// 异步批量添加实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>添加的记录数</returns>
    Task<int> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entity">实体对象</param>
    /// <returns>受影响的行数</returns>
    int Update<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// 异步更新实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entity">实体对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task<int> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 批量更新实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <returns>受影响的行数</returns>
    int UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    /// <summary>
    /// 异步批量更新实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task<int> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entity">实体对象</param>
    /// <returns>受影响的行数</returns>
    int Delete<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// 异步删除实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entity">实体对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task<int> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 批量删除实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <returns>受影响的行数</returns>
    int DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    /// <summary>
    /// 异步批量删除实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task<int> DeleteRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 执行 SQL 命令
    /// </summary>
    /// <param name="sql">SQL 命令</param>
    /// <param name="parameters">参数</param>
    /// <returns>受影响的行数</returns>
    int ExecuteNonQuery(string sql, object parameters = null);

    /// <summary>
    /// 异步执行 SQL 命令
    /// </summary>
    /// <param name="sql">SQL 命令</param>
    /// <param name="parameters">参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task<int> ExecuteNonQueryAsync(string sql, object parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行 SQL 命令并返回第一个结果
    /// </summary>
    /// <param name="sql">SQL 命令</param>
    /// <param name="parameters">参数</param>
    /// <returns>第一个结果</returns>
    object ExecuteScalar(string sql, object parameters = null);

    /// <summary>
    /// 异步执行 SQL 命令并返回第一个结果
    /// </summary>
    /// <param name="sql">SQL 命令</param>
    /// <param name="parameters">参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>第一个结果</returns>
    Task<object> ExecuteScalarAsync(string sql, object parameters = null, CancellationToken cancellationToken = default);
}