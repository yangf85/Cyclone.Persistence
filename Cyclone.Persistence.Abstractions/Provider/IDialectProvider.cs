using System;
using System.Collections.Generic;
using System.Data;

namespace Cyclone.Persistence.Abstractions.Provider;

/// <summary>
/// 定义数据库方言提供程序的接口
/// </summary>
public interface IDialectProvider
{
    /// <summary>
    /// 获取提供程序类型
    /// </summary>
    DbProviderType ProviderType { get; }

    /// <summary>
    /// 获取标识符引用字符（开始）
    /// </summary>
    char OpenQuote { get; }

    /// <summary>
    /// 获取标识符引用字符（结束）
    /// </summary>
    char CloseQuote { get; }

    /// <summary>
    /// 获取参数前缀
    /// </summary>
    string ParameterPrefix { get; }

    /// <summary>
    /// 引用标识符
    /// </summary>
    /// <param name="identifier">标识符</param>
    /// <returns>引用后的标识符</returns>
    string QuoteIdentifier(string identifier);

    /// <summary>
    /// 创建参数名称
    /// </summary>
    /// <param name="name">参数名</param>
    /// <returns>格式化后的参数名</returns>
    string CreateParameterName(string name);

    /// <summary>
    /// 创建参数
    /// </summary>
    /// <param name="name">参数名</param>
    /// <param name="value">参数值</param>
    /// <param name="dbType">数据库类型</param>
    /// <param name="direction">参数方向</param>
    /// <returns>数据库参数</returns>
    IDbDataParameter CreateParameter(string name, object value, DbType? dbType = null, ParameterDirection direction = ParameterDirection.Input);

    /// <summary>
    /// 获取 .NET 类型对应的数据库类型
    /// </summary>
    /// <param name="type">.NET 类型</param>
    /// <returns>数据库类型</returns>
    DbType GetDbType(Type type);

    /// <summary>
    /// 获取最大支持的参数数量
    /// </summary>
    int GetMaxParameters();

    /// <summary>
    /// 生成分页 SQL
    /// </summary>
    /// <param name="sql">原始 SQL</param>
    /// <param name="skip">跳过的记录数</param>
    /// <param name="take">获取的记录数</param>
    /// <param name="parameters">参数集合</param>
    /// <returns>分页 SQL</returns>
    string GeneratePagedSql(string sql, int skip, int take, IDictionary<string, object> parameters);

    /// <summary>
    /// 生成插入 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="columns">列名集合</param>
    /// <param name="returnIdentity">是否返回标识</param>
    /// <returns>插入 SQL</returns>
    string GenerateInsertSql(string tableName, IEnumerable<string> columns, bool returnIdentity = true);

    /// <summary>
    /// 生成批量插入 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="columns">列名集合</param>
    /// <param name="rowCount">行数</param>
    /// <returns>批量插入 SQL</returns>
    string GenerateBulkInsertSql(string tableName, IEnumerable<string> columns, int rowCount);

    /// <summary>
    /// 生成更新 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="columns">列名集合</param>
    /// <param name="keyColumns">主键列名集合</param>
    /// <returns>更新 SQL</returns>
    string GenerateUpdateSql(string tableName, IEnumerable<string> columns, IEnumerable<string> keyColumns);

    /// <summary>
    /// 生成删除 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="keyColumns">主键列名集合</param>
    /// <returns>删除 SQL</returns>
    string GenerateDeleteSql(string tableName, IEnumerable<string> keyColumns);

    /// <summary>
    /// 生成按条件删除 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="whereClause">条件子句</param>
    /// <returns>按条件删除 SQL</returns>
    string GenerateDeleteWhereSql(string tableName, string whereClause);

    /// <summary>
    /// 生成合并 SQL (UPSERT)
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="columns">列名集合</param>
    /// <param name="keyColumns">主键列名集合</param>
    /// <returns>合并 SQL</returns>
    string GenerateMergeSql(string tableName, IEnumerable<string> columns, IEnumerable<string> keyColumns);

    /// <summary>
    /// 生成表是否存在的 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>判断表是否存在的 SQL</returns>
    string GenerateTableExistsSql(string tableName);

    /// <summary>
    /// 生成创建表的 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="columns">列定义</param>
    /// <param name="primaryKeys">主键列名集合</param>
    /// <returns>创建表的 SQL</returns>
    string GenerateCreateTableSql(string tableName, IDictionary<string, string> columns, IEnumerable<string> primaryKeys);

    /// <summary>
    /// 生成删除表的 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>删除表的 SQL</returns>
    string GenerateDropTableSql(string tableName);

    /// <summary>
    /// 生成创建索引的 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="indexName">索引名</param>
    /// <param name="columns">索引列名集合</param>
    /// <param name="isUnique">是否唯一索引</param>
    /// <returns>创建索引的 SQL</returns>
    string GenerateCreateIndexSql(string tableName, string indexName, IEnumerable<string> columns, bool isUnique);

    /// <summary>
    /// 生成删除索引的 SQL
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="indexName">索引名</param>
    /// <returns>删除索引的 SQL</returns>
    string GenerateDropIndexSql(string tableName, string indexName);
}