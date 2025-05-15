using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Data;

/// <summary>
/// 定义数据库命令的接口
/// </summary>
public interface IDbCommand : IDisposable
{
    /// <summary>
    /// 获取或设置命令文本
    /// </summary>
    string CommandText { get; set; }

    /// <summary>
    /// 获取或设置命令类型
    /// </summary>
    CommandType CommandType { get; set; }

    /// <summary>
    /// 获取或设置命令超时（秒）
    /// </summary>
    int CommandTimeout { get; set; }

    /// <summary>
    /// 添加参数
    /// </summary>
    /// <param name="name">参数名</param>
    /// <param name="value">参数值</param>
    /// <param name="dbType">数据库类型</param>
    /// <param name="direction">参数方向</param>
    /// <returns>数据库参数</returns>
    IDbDataParameter AddParameter(string name, object value, DbType? dbType = null, ParameterDirection direction = ParameterDirection.Input);

    /// <summary>
    /// 添加多个参数
    /// </summary>
    /// <param name="parameters">参数字典</param>
    void AddParameters(IDictionary<string, object> parameters);

    /// <summary>
    /// 执行非查询命令
    /// </summary>
    /// <returns>受影响的行数</returns>
    int ExecuteNonQuery();

    /// <summary>
    /// 异步执行非查询命令
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行查询，返回第一个结果
    /// </summary>
    /// <returns>查询结果</returns>
    object ExecuteScalar();

    /// <summary>
    /// 异步执行查询，返回第一个结果
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>查询结果</returns>
    Task<object> ExecuteScalarAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行查询，返回数据读取器
    /// </summary>
    /// <returns>数据读取器</returns>
    IDataReader ExecuteReader();

    /// <summary>
    /// 异步执行查询，返回数据读取器
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>数据读取器</returns>
    Task<IDataReader> ExecuteReaderAsync(CancellationToken cancellationToken = default);
}