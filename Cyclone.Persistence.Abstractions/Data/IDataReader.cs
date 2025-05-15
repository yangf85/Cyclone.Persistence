using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Data;

/// <summary>
/// 定义数据读取器的接口
/// </summary>
public interface IDataReader : IDisposable
{
    /// <summary>
    /// 获取字段数量
    /// </summary>
    int FieldCount { get; }

    /// <summary>
    /// 关闭读取器
    /// </summary>
    void Close();

    /// <summary>
    /// 获取指定列名的值
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>列值</returns>
    object GetValue(string name);

    /// <summary>
    /// 获取指定列索引的值
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>列值</returns>
    object GetValue(int ordinal);

    /// <summary>
    /// 获取指定列名的字符串值
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>字符串值</returns>
    string GetString(string name);

    /// <summary>
    /// 获取指定列索引的字符串值
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>字符串值</returns>
    string GetString(int ordinal);

    /// <summary>
    /// 获取指定列名的整数值
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>整数值</returns>
    int GetInt32(string name);

    /// <summary>
    /// 获取指定列索引的整数值
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>整数值</returns>
    int GetInt32(int ordinal);

    /// <summary>
    /// 获取指定列名的长整数值
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>长整数值</returns>
    long GetInt64(string name);

    /// <summary>
    /// 获取指定列索引的长整数值
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>长整数值</returns>
    long GetInt64(int ordinal);

    /// <summary>
    /// 获取指定列名的浮点值
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>浮点值</returns>
    double GetDouble(string name);

    /// <summary>
    /// 获取指定列索引的浮点值
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>浮点值</returns>
    double GetDouble(int ordinal);

    /// <summary>
    /// 获取指定列名的布尔值
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>布尔值</returns>
    bool GetBoolean(string name);

    /// <summary>
    /// 获取指定列索引的布尔值
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>布尔值</returns>
    bool GetBoolean(int ordinal);

    /// <summary>
    /// 获取指定列名的日期时间值
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>日期时间值</returns>
    DateTime GetDateTime(string name);

    /// <summary>
    /// 获取指定列索引的日期时间值
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>日期时间值</returns>
    DateTime GetDateTime(int ordinal);

    /// <summary>
    /// 获取指定列名的 GUID 值
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>GUID 值</returns>
    Guid GetGuid(string name);

    /// <summary>
    /// 获取指定列索引的 GUID 值
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>GUID 值</returns>
    Guid GetGuid(int ordinal);

    /// <summary>
    /// 获取指定列名的强类型值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="name">列名</param>
    /// <returns>强类型值</returns>
    T GetFieldValue<T>(string name);

    /// <summary>
    /// 获取指定列索引的强类型值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="ordinal">列索引</param>
    /// <returns>强类型值</returns>
    T GetFieldValue<T>(int ordinal);

    /// <summary>
    /// 检查指定列是否为 DBNull
    /// </summary>
    /// <param name="name">列名</param>
    /// <returns>是否为 DBNull</returns>
    bool IsDBNull(string name);

    /// <summary>
    /// 检查指定列是否为 DBNull
    /// </summary>
    /// <param name="ordinal">列索引</param>
    /// <returns>是否为 DBNull</returns>
    bool IsDBNull(int ordinal);

    /// <summary>
    /// 读取下一条记录
    /// </summary>
    /// <returns>是否成功读取</returns>
    bool Read();

    /// <summary>
    /// 异步读取下一条记录
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>是否成功读取</returns>
    Task<bool> ReadAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>实体集合</returns>
    IEnumerable<T> GetData<T>() where T : class, new();

    /// <summary>
    /// 异步获取所有数据
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>实体集合</returns>
    Task<IEnumerable<T>> GetDataAsync<T>(CancellationToken cancellationToken = default) where T : class, new();
}