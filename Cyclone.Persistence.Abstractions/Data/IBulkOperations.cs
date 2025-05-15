using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Data;

/// <summary>
/// 定义批量操作的接口
/// </summary>
public interface IBulkOperations
{
    /// <summary>
    /// 批量插入
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="tableName">表名（可选）</param>
    /// <param name="batchSize">批次大小</param>
    /// <returns>插入的记录数</returns>
    int BulkInsert<TEntity>(IEnumerable<TEntity> entities, string tableName = null, int batchSize = 1000) where TEntity : class;

    /// <summary>
    /// 异步批量插入
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="tableName">表名（可选）</param>
    /// <param name="batchSize">批次大小</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>插入的记录数</returns>
    Task<int> BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities, string tableName = null, int batchSize = 1000, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 批量更新
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="tableName">表名（可选）</param>
    /// <param name="batchSize">批次大小</param>
    /// <returns>更新的记录数</returns>
    int BulkUpdate<TEntity>(IEnumerable<TEntity> entities, string tableName = null, int batchSize = 1000) where TEntity : class;

    /// <summary>
    /// 异步批量更新
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="tableName">表名（可选）</param>
    /// <param name="batchSize">批次大小</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>更新的记录数</returns>
    Task<int> BulkUpdateAsync<TEntity>(IEnumerable<TEntity> entities, string tableName = null, int batchSize = 1000, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="tableName">表名（可选）</param>
    /// <param name="batchSize">批次大小</param>
    /// <returns>删除的记录数</returns>
    int BulkDelete<TEntity>(IEnumerable<TEntity> entities, string tableName = null, int batchSize = 1000) where TEntity : class;

    /// <summary>
    /// 异步批量删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="tableName">表名（可选）</param>
    /// <param name="batchSize">批次大小</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>删除的记录数</returns>
    Task<int> BulkDeleteAsync<TEntity>(IEnumerable<TEntity> entities, string tableName = null, int batchSize = 1000, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// 批量合并（更新已存在的记录，插入不存在的记录）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="tableName">表名（可选）</param>
    /// <param name="batchSize">批次大小</param>
    /// <returns>受影响的记录数</returns>
    int BulkMerge<TEntity>(IEnumerable<TEntity> entities, string tableName = null, int batchSize = 1000) where TEntity : class;

    /// <summary>
    /// 异步批量合并（更新已存在的记录，插入不存在的记录）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="entities">实体集合</param>
    /// <param name="tableName">表名（可选）</param>
    /// <param name="batchSize">批次大小</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的记录数</returns>
    Task<int> BulkMergeAsync<TEntity>(IEnumerable<TEntity> entities, string tableName = null, int batchSize = 1000, CancellationToken cancellationToken = default) where TEntity : class;
}