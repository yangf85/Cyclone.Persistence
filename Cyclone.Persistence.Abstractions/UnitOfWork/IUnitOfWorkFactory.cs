using System;

namespace Cyclone.Persistence.Abstractions.UnitOfWork;

/// <summary>
/// 定义工作单元工厂的接口
/// </summary>
public interface IUnitOfWorkFactory
{
    /// <summary>
    /// 创建工作单元
    /// </summary>
    /// <returns>工作单元</returns>
    IUnitOfWork CreateUnitOfWork();

    /// <summary>
    /// 创建工作单元并指定连接字符串
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <returns>工作单元</returns>
    IUnitOfWork CreateUnitOfWork(string connectionString);

    /// <summary>
    /// 创建工作单元并指定超时时间
    /// </summary>
    /// <param name="timeout">超时时间</param>
    /// <returns>工作单元</returns>
    IUnitOfWork CreateUnitOfWork(TimeSpan timeout);

    /// <summary>
    /// 创建工作单元并指定连接字符串和超时时间
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="timeout">超时时间</param>
    /// <returns>工作单元</returns>
    IUnitOfWork CreateUnitOfWork(string connectionString, TimeSpan timeout);
}