using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cyclone.Persistence.Abstractions.Repository;

namespace Cyclone.Persistence.Abstractions.UnitOfWork;

/// <summary>
/// 定义工作单元的接口
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// 获取仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>实体仓储</returns>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    /// <summary>
    /// 保存更改
    /// </summary>
    /// <returns>受影响的行数</returns>
    int SaveChanges();

    /// <summary>
    /// 异步保存更改
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="isolationLevel">事务隔离级别</param>
    /// <returns>事务对象</returns>
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// 异步开始事务
    /// </summary>
    /// <param name="isolationLevel">事务隔离级别</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>事务对象</returns>
    Task<IDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

    /// <summary>
    /// 提交事务
    /// </summary>
    void CommitTransaction();

    /// <summary>
    /// 异步提交事务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 回滚事务
    /// </summary>
    void RollbackTransaction();

    /// <summary>
    /// 异步回滚事务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取当前事务
    /// </summary>
    /// <returns>当前事务</returns>
    IDbTransaction GetCurrentTransaction();

    /// <summary>
    /// 执行原始SQL查询
    /// </summary>
    /// <param name="sql">SQL查询</param>
    /// <param name="parameters">参数</param>
    /// <returns>受影响的行数</returns>
    int ExecuteSqlCommand(string sql, params object[] parameters);

    /// <summary>
    /// 异步执行原始SQL查询
    /// </summary>
    /// <param name="sql">SQL查询</param>
    /// <param name="parameters">参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task<int> ExecuteSqlCommandAsync(string sql, object[] parameters, CancellationToken cancellationToken = default);
}