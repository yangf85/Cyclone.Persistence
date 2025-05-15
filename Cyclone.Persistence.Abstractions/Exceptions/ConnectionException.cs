using System;
using System.Runtime.Serialization;

namespace Cyclone.Persistence.Abstractions.Exceptions;

/// <summary>
/// 数据库连接异常
/// </summary>
[Serializable]
public class ConnectionException : CyclonePersistenceException
{
    /// <summary>
    /// 获取连接字符串
    /// </summary>
    public string ConnectionString { get; }

    /// <summary>
    /// 初始化异常
    /// </summary>
    public ConnectionException() : base()
    {
    }

    /// <summary>
    /// 使用指定的错误消息初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    public ConnectionException(string message) : base(message)
    {
    }

    /// <summary>
    /// 使用指定的错误消息和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">内部异常</param>
    public ConnectionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// 使用指定的错误消息和连接字符串初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="connectionString">连接字符串</param>
    public ConnectionException(string message, string connectionString) : base(message)
    {
        ConnectionString = connectionString;
    }

    /// <summary>
    /// 使用指定的错误消息、连接字符串和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="innerException">内部异常</param>
    public ConnectionException(string message, string connectionString, Exception innerException) : base(message, innerException)
    {
        ConnectionString = connectionString;
    }

    /// <summary>
    /// 使用序列化数据初始化异常
    /// </summary>
    /// <param name="info">序列化信息</param>
    /// <param name="context">流上下文</param>
    protected ConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ConnectionString = info.GetString("ConnectionString");
    }

    /// <summary>
    /// 获取对象数据
    /// </summary>
    /// <param name="info">序列化信息</param>
    /// <param name="context">流上下文</param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("ConnectionString", ConnectionString);
    }
}