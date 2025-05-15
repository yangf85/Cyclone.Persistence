namespace Cyclone.Persistence.Abstractions.Provider;

/// <summary>
/// 数据库提供程序类型
/// </summary>
public enum DbProviderType
{
    /// <summary>
    /// SQLite 数据库
    /// </summary>
    SQLite,

    /// <summary>
    /// SQL Server 数据库
    /// </summary>
    SqlServer,

    /// <summary>
    /// PostgreSQL 数据库
    /// </summary>
    PostgreSQL,

    /// <summary>
    /// LiteDB 文档数据库
    /// </summary>
    LiteDB,

    /// <summary>
    /// 内存数据库 (用于测试)
    /// </summary>
    InMemory,

    /// <summary>
    /// 其他数据库类型
    /// </summary>
    Other
}