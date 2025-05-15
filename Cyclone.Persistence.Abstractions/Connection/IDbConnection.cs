using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Connection;

/// <summary>
/// 定义数据库连接的接口
/// </summary>
public interface IDbConnection : IDisposable
{
    /// <summary>
    /// 获取连接字符串
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// 获取连接状态
    /// </summary>
    ConnectionState State { get; }

    /// <summary>
    /// 打开连接
    /// </summary>
    void Open();

    /// <summary>
    /// 异步打开连接
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task OpenAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 关闭连接
    /// </summary>
    void Close();

    /// <summary>
    /// 创建命令
    /// </summary>
    /// <returns>数据库命令</returns>
    IDbCommand CreateCommand();

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="isolationLevel">事务隔离级别</param>
    /// <returns>数据库事务</returns>
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// 异步开始事务
    /// </summary>
    /// <param name="isolationLevel">事务隔离级别</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>数据库事务</returns>
    Task<IDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取或设置命令超时（秒）
    /// </summary>
    int CommandTimeout { get; set; }
}