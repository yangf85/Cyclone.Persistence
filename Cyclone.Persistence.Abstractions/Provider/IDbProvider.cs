using System;
using Cyclone.Persistence.Abstractions.Connection;

namespace Cyclone.Persistence.Abstractions.Provider;

/// <summary>
/// 定义数据库提供程序的接口
/// </summary>
public interface IDbProvider
{
    /// <summary>
    /// 获取提供程序名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 获取提供程序类型
    /// </summary>
    DbProviderType ProviderType { get; }

    /// <summary>
    /// 是否支持事务
    /// </summary>
    bool SupportsTransaction { get; }

    /// <summary>
    /// 是否支持架构创建
    /// </summary>
    bool SupportsSchemaCreation { get; }

    /// <summary>
    /// 创建数据库连接
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <returns>数据库连接</returns>
    IDbConnection CreateConnection(string connectionString);

    /// <summary>
    /// 检查数据库是否存在
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <returns>是否存在</returns>
    bool DatabaseExists(string connectionString);

    /// <summary>
    /// 创建数据库
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <returns>是否成功创建</returns>
    bool CreateDatabase(string connectionString);

    /// <summary>
    /// 删除数据库
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <returns>是否成功删除</returns>
    bool DropDatabase(string connectionString);
}