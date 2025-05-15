using System;

namespace Cyclone.Persistence.Abstractions.Connection;

/// <summary>
/// 定义数据库连接工厂的接口
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>
    /// 创建数据库连接
    /// </summary>
    /// <returns>数据库连接</returns>
    IDbConnection CreateConnection();

    /// <summary>
    /// 使用指定的连接字符串创建数据库连接
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <returns>数据库连接</returns>
    IDbConnection CreateConnection(string connectionString);

    /// <summary>
    /// 使用指定的连接超时创建数据库连接
    /// </summary>
    /// <param name="timeout">连接超时</param>
    /// <returns>数据库连接</returns>
    IDbConnection CreateConnection(TimeSpan timeout);

    /// <summary>
    /// 使用指定的连接字符串和超时创建数据库连接
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="timeout">连接超时</param>
    /// <returns>数据库连接</returns>
    IDbConnection CreateConnection(string connectionString, TimeSpan timeout);
}