using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Connection;

/// <summary>
/// 定义数据库事务的接口
/// </summary>
public interface IDbTransaction : IDisposable
{
    /// <summary>
    /// 获取事务的隔离级别
    /// </summary>
    IsolationLevel IsolationLevel { get; }

    /// <summary>
    /// 获取事务的连接
    /// </summary>
    IDbConnection Connection { get; }

    /// <summary>
    /// 提交事务
    /// </summary>
    void Commit();

    /// <summary>
    /// 异步提交事务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 回滚事务
    /// </summary>
    void Rollback();

    /// <summary>
    /// 异步回滚事务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 创建保存点
    /// </summary>
    /// <param name="savepointName">保存点名称</param>
    void SavePoint(string savepointName);

    /// <summary>
    /// 异步创建保存点
    /// </summary>
    /// <param name="savepointName">保存点名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task SavePointAsync(string savepointName, CancellationToken cancellationToken = default);

    /// <summary>
    /// 回滚到保存点
    /// </summary>
    /// <param name="savepointName">保存点名称</param>
    void RollbackToSavePoint(string savepointName);

    /// <summary>
    /// 异步回滚到保存点
    /// </summary>
    /// <param name="savepointName">保存点名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task RollbackToSavePointAsync(string savepointName, CancellationToken cancellationToken = default);

    /// <summary>
    /// 释放保存点
    /// </summary>
    /// <param name="savepointName">保存点名称</param>
    void ReleaseSavePoint(string savepointName);

    /// <summary>
    /// 异步释放保存点
    /// </summary>
    /// <param name="savepointName">保存点名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task ReleaseSavePointAsync(string savepointName, CancellationToken cancellationToken = default);
}